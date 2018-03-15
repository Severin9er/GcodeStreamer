#define DEBUG

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Threading;
using WindowsFormsApplication2;
using System.Net;

namespace GcodeStreamer
{
    public partial class GcodeStreamer : Form
    {
        #region structs
        
        struct tool
        {
            public int nr;
            public string size;
        };

        struct position
        {
            public double x;
            public double y;
            public double z;
        };

        #endregion

        #region constants

        string[] noReplyCmds =
        {
            "" + (char)0x18,
            //"?",
            "!",
            "~",
            "" + (char)0x84,
            "" + (char)0x85,
            "" + (char)0x90,
            "" + (char)0x91,
            "" + (char)0x92,
            "" + (char)0x93,
            "" + (char)0x94,
            "" + (char)0x95,
            "" + (char)0x96,
            "" + (char)0x97,
            "" + (char)0x9A,
            "" + (char)0x9B,
            "" + (char)0x9C,
            "" + (char)0x9D
        };
        string[] unrecognizedOKOrError =
        {
            "\nk",
            "\no"
        };

        string[] immediateCmds =
        {
            "!",
            "~",
            ""+(char)0x90,
            ""+(char)0x91,
            ""+(char)0x92,
            ""+(char)0x93,
            ""+(char)0x94,
            ""+(char)0x9A,
            ""+(char)0x9B,
            ""+(char)0x9C,
            ""+(char)0x9D,
            ""+(char)0x18,
            ""+(char)0x85
    };
        string feedHold = "!";
        string feedResume = "~";
        string softReset = "" + (char)0x18;
        string requestState = "?";
        string jogCancel = "" + (char)0x85;

        #endregion

        #region properties


        /*string portName = "COM8";
        int baudrate = 115200;
        int maxFeed = 300;
        double maxStep = 0.1;
        int refreshPosInterval = 100;
        string decAccuracy = "0.000000";
        char decSplitChar = ',';
        int maxSpindleRPM = 1000;
        double pcbDimX = 0.0;
        double pcbDimY = 0.0;*/
        int stdFeedError22 = 50;

        #endregion

        #region delegates and events

        delegate void dEnableButton(Button btn, bool enable);
        delegate void dSetProgressbarValue(ProgressBar pb, int value);
        delegate void dPrintText(TextBox tb, string text);
        delegate void dSetText(TextBox tb, string text);
        delegate void dEnableJoystick(bool enable);
        ManualResetEvent mreStreaming;
        ManualResetEvent mrePortCommunication;

        #endregion

        #region variables

        Thread streamingThread;
        Thread jogLoop;
        Thread refreshPosThread;
        System.Windows.Forms.Timer refreshPosTimer;
        //System.Timers.Timer refreshPosTimer;
        StreamReader file;
        SerialPort port;
        LinkedList<tool> usedTools;
        double zHighWhenIdle = 0.0;
        int currentToolNr;
        position toolChangePos;
        Graphics g;
        Graphics gDrawing;
        Rectangle pcbBounds;
        int lastFeed;
        int lastSpindle;
        volatile bool joystickloop;
        volatile string jogString;
        WFSettings settings;
        int spindleSpeed;
        Point lineStart = new Point();
        Point lineEnd = new Point();
        position gStartPos, gEndPos;

        #endregion

        public GcodeStreamer()
        {
            InitializeComponent();
            g = this.pJoystick.CreateGraphics();
            gDrawing = pDrawing.CreateGraphics();
            pcbBounds = pDrawing.ClientRectangle;
            pcbBounds.X += 20;
            pcbBounds.Y += 20;
            pcbBounds.Height -= 40;
            pcbBounds.Width -= 40;
            mreStreaming = new ManualResetEvent(true);
            mrePortCommunication = new ManualResetEvent(true);
            refreshPosTimer = new System.Windows.Forms.Timer();
            refreshPosTimer.Enabled = true;
            toolChangePos = new position();
            usedTools = new LinkedList<tool>();
            currentToolNr = -1;
            lastFeed = 100;
            lastSpindle = 100;
            settings = new WFSettings();
            if(File.Exists("settings.txt"))
            {
                try
                {
                    settings.readFile();
                }
                catch
                {
                    settings.restoreDefault();
                    settings.saveFile();
                }
            }
            else
            {
                settings.restoreDefault();
                settings.saveFile();
            }
            updateMaxSpindleRPM();
            spindleSpeed = WFSettings.maxSpindleRPM;
            tbarSpindleRPM.Value = spindleSpeed;
            tbSpindleRPM.Text = tbarSpindleRPM.Value.ToString();
            port = new SerialPort(WFSettings.comport, WFSettings.baudrate);
            try
            {
                port.Open();
            }
            catch
            {
                //System.Console.WriteLine(e.StackTrace);
                //Error-Message
            }
            refreshPosThread = new Thread(refreshPos);
            refreshPosThread.Start();
        }

        private void streaming()
        {
            try
            {
                if (file == null)
                    return;
                if (!file.BaseStream.CanRead)
                    return;
                gStartPos.x = 0;
                gStartPos.y = 0;
                gStartPos.z = 0;
                while (!file.EndOfStream)
                {

                    string nextLine = file.ReadLine();

                    int status = (int)(100 * file.BaseStream.Position / file.BaseStream.Length);
                    mreStreaming.WaitOne();
                    setProgressbarValue(pbStreamBar, status);

                    //Remove spaces at the beginning
                    while (nextLine.StartsWith(" "))
                    {
                        nextLine = nextLine.Remove(0, 1);
                    }
                    if (nextLine.Contains(";"))
                    {
                        nextLine = nextLine.Substring(0, nextLine.IndexOf(';'));
                    }
                    /*if (nextLine.Contains("("))
                    {
                        bracket(nextLine); //bracket() was already called before in drawGcode
                        nextLine = "";
                    }*/
                    if (nextLine.Contains("T") && nextLine.Contains("M06"))  //Tool change
                    {
                        drawGcodeLine(nextLine, Color.Blue);
                        toolChange(nextLine);
                        nextLine = "";
                    }
                    if(nextLine.Contains("M03") && !nextLine.Contains("S"))
                    {
                        nextLine += " S" + spindleSpeed;
                    }
                    if (nextLine != "")
                    {
                        while (nextLine.LastIndexOf(' ') == nextLine.Length - 1)
                        {
                            nextLine = nextLine.Remove(nextLine.Length - 1, 1);
                        }
                        mreStreaming.WaitOne();
                        drawGcodeLine(nextLine, Color.Blue);
                        send(nextLine);
                        //Thread.Sleep(10);
                    }

                }
                while (tbZPos.Text != zHighWhenIdle.ToString("0.000")) ;
                SendNotification("Milling is done", "The CNC has finished milling");
            }
            catch
            {
                //MessageBox.Show(e.StackTrace);
                mreStreaming.Set();
                mrePortCommunication.Set();
            }
            finally
            {
                file.BaseStream.Close();
                file.Close();
                file = new StreamReader(new FileStream(tbFileName.Text, FileMode.Open));
                file.BaseStream.Position = 0;
                file.BaseStream.Flush();
                setProgressbarValue(pbStreamBar, 0);
                enableButton(btnStart, true);
                enableButton(btnStop, false);
                enableButton(btnPause, false);
                enableButton(btnSendCommand, true);
                enableJoystick(true);
            }
        }

        private void toolChange(string line)
        {
            while (line.First() != 'T')
            {
                line = line.Remove(0, 1);
            }
            line = line.Remove(0, 1);
            string[] s = line.Split(' ');
            int nr = int.Parse(s[0]);
            if(nr != currentToolNr) //If tool is not already mounted
            {
                //position backPos = getPosition();
                //backPos.z = toolChangePos.z;
                enableButton(btnSendCommand, true);
                enableJoystick(true);
                moveToPos(toolChangePos);
                if (WFSettings.firstNotification)
                {
                    SendNotification("Toolchange Soon", "T" + nr.ToString("00") + ": " + usedTools.ToArray()[nr - 1].size);
                }
                //send("M05"); //Stop Spindle //not needed, because it is included in the Gcode
                while (tbXPos.Text != toolChangePos.x.ToString("0.000") || tbYPos.Text != toolChangePos.y.ToString("0.000") || tbZPos.Text != toolChangePos.z.ToString("0.000")) ;
                if (WFSettings.lastNotification)
                {
                    SendNotification("Toolchange Now", "T" + nr.ToString("00") + ": " + usedTools.ToArray()[nr - 1].size);
                }
                MessageBox.Show("Change Tool to T" + nr.ToString() + ":\r\n\r\nSize: " + usedTools.ToArray()[nr - 1].size+"\r\n\r\nDO NOT TIGHTEN TOOL TOO FIRMLY YET", "Change Tool", MessageBoxButtons.OK);
                currentToolNr = nr; //Set current tool to the new tool
                //moveToPos(backPos); //Go back to previous x and y position (z in safe height)
                //send("M03");  //Start Spindle again // not needed because already included in Gcode
                enableButton(btnSendCommand, false);
                enableJoystick(false);
            }
            else
            {
                enableButton(btnSendCommand, true);
                enableJoystick(true);
                while (tbZPos.Text != 0.ToString("0.000"));
                MessageBox.Show("Tighten the Tool now firmly and make sure, it has contact to the surface (Z=0).", "Change Tool", MessageBoxButtons.OK);
                enableButton(btnSendCommand, false);
                enableJoystick(false);
            }
        }

        private void updateMaxSpindleRPM()
        {
            tbarSpindleRPM.Maximum = WFSettings.maxSpindleRPM;
            tbarSpindleRPM.TickFrequency = WFSettings.maxSpindleRPM / 20;
        }

        #region communication with cnc

        private void moveToPos(position p)
        {
            send("G00 Z" + p.z.ToString(WFSettings.decAccuracy).Replace(',','.'));  //Move Z first
            send("G00 X" + p.x.ToString(WFSettings.decAccuracy).Replace(',', '.') + " Y" + p.y.ToString(WFSettings.decAccuracy).Replace(',', '.'));   //Move X,Y
        }

        private string send(string cmd)
        {
            string reply = "";
            if (cmd == "")
                return reply; //no command to send
            bool needReply = checkCmdForReply(cmd);
            printText(tbConsole, cmd);
            if (!port.IsOpen)
            {
                Thread.Sleep(10);
                return reply; //port is not open -> error message
            }
            port.NewLine = "\r";
            if (immediateCmds.Contains(cmd))
            {
                port.Write(cmd);
                return reply;
            }
            mrePortCommunication.WaitOne();
            mrePortCommunication.Reset();
            if(!needReply)
            {
                if (cmd.Length == 1)
                {
                    Char c = cmd.First();
                    Byte[] b = { (Byte)c };
                    port.Write(b, 0, 1);
                }
                else
                {
                    port.Write(cmd);
                }
            }
            if(needReply)
            {
                port.WriteLine(cmd);
                
                string line = port.ReadLine();
                while(!line.Contains("ok") && !line.Contains("error") && !unrecognizedOKOrError.Contains(line))
                {
                    printText(tbConsole,line);
                    reply += line;
                    line = port.ReadLine();
                }
                if(line.Contains("error"))
                {
                    printText(tbConsole, line);
                    if(line.Contains("error:22"))
                    {
                        mrePortCommunication.Set();
                        send(cmd + " F" + stdFeedError22.ToString());
                    }
                }
            }
            mrePortCommunication.Set();
            return reply;
        }

        private string sendHidden(string cmd)
        {
            string reply = "";
            if (cmd == "")
                return reply; //no command to send
            bool needReply = checkCmdForReply(cmd);
            if (!port.IsOpen)
            {
                Thread.Sleep(10);
                return reply; //port is not open -> error message
            }
            port.NewLine = "\r";
            if (immediateCmds.Contains(cmd))
            {
                port.Write(cmd);
                return reply;
            }
            mrePortCommunication.WaitOne();
            mrePortCommunication.Reset();
            if (!needReply)
            {
                if(cmd.Length == 1 && cmd != "!" && cmd != "~")
                {
                    Char c = cmd.First();
                    Byte[] b = { (Byte)c };
                    port.Write(b, 0, 1);
                }
                else
                {
                    port.WriteLine(cmd);
                }
            }
            if (needReply)
            {
                try
                {
                    port.WriteLine(cmd);
                    string line = port.ReadLine();
                    while (!line.Contains("ok") && !line.Contains("error") && !unrecognizedOKOrError.Contains(line))
                    {
                        reply += line;
                        line = port.ReadLine();
                    }
                }
                catch
                {

                }
                
            }
            mrePortCommunication.Set();
            return reply;
        }

        private position getPosition()
        {
            position pos = new position();
            pos.x = double.NaN;
            pos.y = double.NaN;
            pos.z = double.NaN;
            string reply = sendHidden(requestState);
            if (reply != "")
            {
                if (reply.Contains("WPos:") || reply.Contains("MPos:"))
                {
                    string[] s = reply.Split('|');
                    for (int i = 0; i < s.Count(); i++)
                    {
                        if (s[i].StartsWith("MPos:") || s[i].StartsWith("WPos:"))
                        {
                            reply = s[i];
                            i = s.Count();
                        }
                    }
                    reply = reply.Remove(0, 5);
                    s = reply.Split(',');
                    for (int i = 0; i < s.Count(); i++)
                    {
                        s[i] = s[i].Replace('.', WFSettings.decSplitChar);
                    }
                    try
                    {
                        double.TryParse(s[0], out pos.x);
                        double.TryParse(s[1], out pos.y);
                        double.TryParse(s[2], out pos.z);
                    }
                    catch(Exception e)
                    {
                        MessageBox.Show(e.Message, e.StackTrace);
                        pos.x = double.NaN;
                        pos.y = double.NaN;
                        pos.z = double.NaN;
                    }
                }
            }
            return pos;
        }

        private void refreshPos()
        {
            try {
                position pos;
                while (true)
                {
                    pos = getPosition();
                    if (!double.IsNaN(pos.x) && !double.IsNaN(pos.y) && !double.IsNaN(pos.z))
                    {
                        setText(tbXPos, pos.x.ToString("0.000"));
                        setText(tbYPos, pos.y.ToString("0.000"));
                        setText(tbZPos, pos.z.ToString("0.000"));
                    }
                    Thread.Sleep(WFSettings.refreshPosInterval);
                }
            }
            catch
            {
                mrePortCommunication.Set();
            }
        }

        private void bracket(string line, ref StreamReader sr)
        {
            if(line.Contains("tool change at")) //Parse Tool Change Position out of file
            {
                usedTools.Clear();
                while(!char.IsDigit(line.First<char>()))    //Remove part before first number
                {
                    line = line.Remove(0, 1);
                }
                string[] s = line.Split(' ');
                for (int i = 0; i < s.Count(); i++)
                {
                    s[i]=s[i].Replace('.', WFSettings.decSplitChar);
                }
                toolChangePos.x = double.Parse(s[0]);
                toolChangePos.y = double.Parse(s[1]);
                toolChangePos.z = double.Parse(s[2]);
                return;
            }
            if(line.Contains("T") && line.Contains("mm"))   //Parse Tools (requries that no other comment in brackets contains "T" and "mm")
            {
                while (line.First<char>() != 'T')    //Remove part before T
                {
                    line = line.Remove(0, 1);
                }
                string[] s = line.Split(' ');
                s[0] = s[0].Remove(0, 1);   //Remove T
                tool t = new tool();
                t.nr = int.Parse(s[0]);
                t.size = s[1];
                for(int i = 2; t.size == "" && i < s.Count(); i++)
                {
                    t.size = s[i];
                }
                usedTools.AddLast(t);
                //Ignore the rest of the line
                return;
            }
            if(line.Contains("High") && line.Contains("Up") && line.Contains("Down") && line.Contains("Drill"))
            {
                line = sr.ReadLine();
                line = line.Replace("(", "");
                line = line.Replace(")", "");
                string s = line.Split(' ')[0].Replace('.',WFSettings.decSplitChar);
                zHighWhenIdle = double.Parse(s);
            }
        }

        private bool checkCmdForReply(string cmd)
        {
            for(int i = 0; i < noReplyCmds.Length; i++)
            {
                if (cmd == noReplyCmds[i])
                    return false;
            }
            return true;
        }

        private void feedOverride(int val)
        {
            if(val > 10 && val <= 200 && val != lastFeed)
            {
                if(val > lastFeed)  //increasing
                {
                    for (int i = 0; i < (val - lastFeed) / 10; i++)
                    {
                        sendHidden("" + (char)0x91);    //increase by 10
                        Thread.Sleep(10);
                    }
                    for (int i = 0; i < (val - lastFeed) % 10; i++)
                    {
                        sendHidden("" + (char)0x93);    //Increase by 1
                        Thread.Sleep(10);
                    }
                }
                else
                {
                    for (int i = 0; i < (lastFeed - val) / 10; i++)
                    {
                        sendHidden("" + (char)0x92);    //decrease by 10
                        Thread.Sleep(10);
                    }
                    for (int i = 0; i < (lastFeed - val) % 10; i++)
                    {
                        sendHidden("" + (char)0x94);    //decrease by 1
                        Thread.Sleep(10);
                    }
                }
                lastFeed = val;
            }
            
        }

        private void spindleOverride(int val)
        {
            if (val > 10 && val <= 200 && val != lastSpindle)
            {
                if (val > lastSpindle)  //increasing
                {
                    for (int i = 0; i < (val - lastSpindle) / 10; i++)
                    {
                        sendHidden("" + (char)0x9A);    //increase by 10
                        Thread.Sleep(10);
                    }
                    for (int i = 0; i < (val - lastSpindle) % 10; i++)
                    {
                        sendHidden("" + (char)0x9C);    //Increase by 1
                        Thread.Sleep(10);
                    }
                }
                else
                {
                    for (int i = 0; i < (lastSpindle - val) / 10; i++)
                    {
                        sendHidden("" + (char)0x9B);    //decrease by 10
                        Thread.Sleep(10);
                    }
                    for (int i = 0; i < (lastSpindle - val) % 10; i++)
                    {
                        sendHidden("" + (char)0x9D);    //decrease by 1
                        Thread.Sleep(10);
                    }
                }
                lastSpindle = val;
            }

        }

        private void joystick(int x, int y)
        {
            int feed = (int)Math.Sqrt(x * x + y * y);
            int max = pJoystick.Width / 2 - 10;
            double xStep = WFSettings.maxJoystickStep * x / max;
            double yStep = WFSettings.maxJoystickStep * -y / max;
            feed = feed * WFSettings.maxJoystickFeed;
            feed /= max;
            jogString = "$J=G91 X" + xStep.ToString(WFSettings.decAccuracy) + "Y" + yStep.ToString(WFSettings.decAccuracy) + "F" + feed;
        }

        private void joystickLoopStart()
        {
            jogLoop = new Thread(joystickLoop);
            jogLoop.Start();
        }

        private void joystickLoop()
        {
            try {
                while (joystickloop)
                {
                    jogString = jogString.Replace(',', '.');
                    send(jogString);
                }
            }
            catch
            {
                mrePortCommunication.Set();
            }
        }

        #endregion

        #region gcode drawing

        public void drawGcodeFile()
        {
            gDrawing.Clear(pDrawing.BackColor);
            gStartPos.x = 0;
            gStartPos.y = 0;
            gStartPos.z = 0;
            gDrawing.DrawRectangle(new Pen(Color.Black), pcbBounds);
            StreamReader fileReader = new StreamReader(tbFileName.Text);
            StreamWriter fileWriter = new StreamWriter("tmpFile.txt");
            bool markForNotification = false;
            while (!fileReader.EndOfStream)
            {
                string line = fileReader.ReadLine();
                if(line.Contains("("))
                {
                    bracket(line, ref fileReader);
                    fileWriter.WriteLine(line);
                }
                else
                {
                    if (drawGcodeLine(line, Color.Black))
                    {
                        fileWriter.WriteLine(line);
                    }
                    else
                    {
                        //Consume whole block of moves out of bounds
                        if(line.StartsWith("G00"))
                        {
                            line = fileReader.ReadLine();
                            while(!line.StartsWith("G00"))
                            {
                                line = fileReader.ReadLine();
                            }
                        }
                        markForNotification = true;
                    }
                }
            }
            fileReader.Close();
            fileWriter.Close();
            if(markForNotification)
            {
                DialogResult dialogResult;
                dialogResult = MessageBox.Show("Some GCode lines were detected, which are outside of the configured PCB-Dimensions.\n" +
                    "Would you like to delete them from the file?\n\n" +
                    "NOTE: If you don't delete the lines, the CNC might crash!", "Invalid moves detected", MessageBoxButtons.YesNo);
                if(dialogResult == DialogResult.Yes)
                {
                    file.Close();
                    File.Copy("tmpFile.txt", tbFileName.Text, true);
                    file = new StreamReader(new FileStream(tbFileName.Text, FileMode.Open));
                    file.BaseStream.Flush();
                    file.BaseStream.Position = 0;
                }
            }
            File.Delete("tmpFile.txt");
        }

        private bool drawGcodeLine(string line, Color c)
        {
            bool success = true;
            double xMin, yMin, xMax, yMax;
            xMin = 0;
            yMin = 0;
            xMax = WFSettings.pcbDimX;
            yMax = WFSettings.pcbDimY;
            Pen p = new Pen(c);
            string[] s = line.Split(' ');
            if (s[0] == "G01" || s[0] == "G00")
            {
                gEndPos = gStartPos;
                for (int i = 1; i < s.Length; i++)
                {
                    s[i] = s[i].Replace('.', WFSettings.decSplitChar);
                    if (s[i].Length > 0)
                    {
                        if (s[i].First() == 'X')
                        {
                            gEndPos.x = double.Parse(s[i].Remove(0, 1));
                        }
                        else if (s[i].First() == 'Y')
                        {
                            gEndPos.y = double.Parse(s[i].Remove(0, 1));
                        }
                        else if (s[i].First() == 'Z')
                        {
                            gEndPos.z = double.Parse(s[i].Remove(0, 1));
                        }
                    }
                }
                if (gEndPos.Equals(toolChangePos))
                {
                    FontFamily ff = new FontFamily(System.Drawing.Text.GenericFontFamilies.Monospace);
                    Font f = new Font(ff, 12);
                    lineStart.X = (int)map(gStartPos.x, xMin, xMax, pcbBounds.X, pcbBounds.Width + pcbBounds.X);
                    lineStart.Y = (int)map(gStartPos.y, yMin, yMax, pcbBounds.Height + pcbBounds.Y, pcbBounds.Y);
                    gDrawing.DrawString("T", f, p.Brush, lineStart.X, lineStart.Y);
                }
                else if (gEndPos.z == gStartPos.z)
                {
                    if (gEndPos.x > xMax || gEndPos.x < xMin || gEndPos.y > yMax || gEndPos.y < yMin)
                    {
                        tbConsole.AppendText(line + "\n");
                        success = false;
                    }
                    else
                    {
                        lineStart.X = (int)map(gStartPos.x, xMin, xMax, pcbBounds.X, pcbBounds.Width + pcbBounds.X);
                        lineStart.Y = (int)map(gStartPos.y, yMin, yMax, pcbBounds.Height + pcbBounds.Y, pcbBounds.Y);
                        lineEnd.X = (int)map(gEndPos.x, xMin, xMax, pcbBounds.X, pcbBounds.Width + pcbBounds.X);
                        lineEnd.Y = (int)map(gEndPos.y, yMin, yMax, pcbBounds.Height + pcbBounds.Y, pcbBounds.Y);
                        gDrawing.DrawLine(p, lineStart, lineEnd);
                        gStartPos = gEndPos;
                    }
                }
                else
                {
                    lineStart.X = (int)map(gStartPos.x, xMin, xMax, pcbBounds.X, pcbBounds.Width + pcbBounds.X);
                    lineStart.Y = (int)map(gStartPos.y, yMin, yMax, pcbBounds.Height + pcbBounds.Y, pcbBounds.Y);
                    gDrawing.DrawEllipse(p, lineStart.X - 5, lineStart.Y - 5, 10, 10);
                    gStartPos = gEndPos;
                }

            }
            else if (s[0] == "M06")
            {
                FontFamily ff = new FontFamily(System.Drawing.Text.GenericFontFamilies.Monospace);
                Font f = new Font(ff, 12);
                lineStart.X = (int)map(gStartPos.x, xMin, xMax, pcbBounds.X, pcbBounds.Width + pcbBounds.X);
                lineStart.Y = (int)map(gStartPos.y, yMin, yMax, pcbBounds.Height + pcbBounds.Y, pcbBounds.Y);
                gDrawing.DrawString(s[1], f, p.Brush, lineStart.X, lineStart.Y);
            }
            return success;
        }

        public double map(double x, double in_min, double in_max, double out_min, double out_max)
        {
            return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
        }

        #endregion

        #region invoke

        public void setProgressbarValue(ProgressBar pb, int value)
        {
            if(pb.InvokeRequired)
            {
                dSetProgressbarValue d = new dSetProgressbarValue(setProgressbarValue);
                pb.Invoke(d, new object[] { pb, value });
            }
            else
            {
                pb.Value = value;
            }
        }

        public void printText(TextBox tb, string text)
        {
            if (tb.InvokeRequired)
            {
                dPrintText d = new dPrintText(printText);
                tb.Invoke(d, new object[] { tb, text });
            }
            else
            {
                tb.AppendText(text + "\r\n");
            }
        }

        public void setText(TextBox tb, string text)
        {
            if (tb.InvokeRequired)
            {
                dSetText d = new dSetText(setText);
                tb.Invoke(d, new object[] { tb, text });
            }
            else
            {
                tb.Text = text;
            }
        }

        public void enableButton(Button btn, bool enable)
        {
            if (btn.InvokeRequired)
            {
                dEnableButton d = new dEnableButton(enableButton);
                btn.Invoke(d, new object[] { btn, enable });
            }
            else
            {
                btn.Enabled = enable;
            }
        }

        public void enableJoystick(bool enable)
        {
            if(pJoystick.InvokeRequired)
            {
                dEnableJoystick d = new dEnableJoystick(enableJoystick);
                pJoystick.Invoke(d, new object[] { enable });
            }
            else
            {
                pJoystick.Enabled = enable;
            }

            if (tbarJoystick.InvokeRequired)
            {
                dEnableJoystick d = new dEnableJoystick(enableJoystick);
                tbarJoystick.Invoke(d, new object[] { enable });
            }
            else
            {
                tbarJoystick.Enabled = enable;
            }
        }

        #endregion

        #region Events

        private void onClose(object sender, FormClosingEventArgs e)
        {
            refreshPosThread.Abort();
            if (jogLoop != null)
            {
                port.Close();
                jogLoop.Abort();
                jogLoop = null;
                port.Open();
                sendHidden(jogCancel);
            }
            if(streamingThread != null && streamingThread.ThreadState == ThreadState.Running)
            {
                EventArgs ev = new EventArgs();
                btnStop_Click((object)btnStop, ev);
            }
            port.Close();
        }

        private void btnSendCommand_Click(object sender, EventArgs e)
        {
            string cmd = tbCommands.Text;
            tbCommands.Text = "";
            send(cmd);
        }

        private void tbCommands_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                e.Handled = true;
                btnSendCommand.PerformClick();
            }
            else
            {
                e.Handled = false;
            }
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            if (streamingThread != null)
                streamingThread.Abort();
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Tap-File (*.tap)|*.tap";
            openDialog.ShowDialog();
            if (openDialog.FileName == "")
                return;
            tbFileName.Text = openDialog.FileName;
            if (file != null)
            {
                file.Close();
            }
            file = new StreamReader(openDialog.OpenFile());
            btnStart.Enabled = true;
            usedTools = new LinkedList<tool>();
            drawGcodeFile();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            setProgressbarValue(pbStreamBar, 0);
            streamingThread = new Thread(streaming);
            streamingThread.Start();
            btnStop.Enabled = true;
            btnPause.Enabled = true;
            btnStart.Enabled = false;
            btnSendCommand.Enabled = false;
            enableJoystick(false);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            port.Close();
            streamingThread.Abort();
            try
            {
                port.Open();
            }
            catch
            {

            }
            send(feedHold); //Stop Motion
            send(softReset);  //Send SoftReset to stop CNC and forget last commands
            send(feedResume);   //enable Motion again
            //Close file and reopen it
            file.BaseStream.Close();
            file.Close();
            file = new StreamReader(new FileStream(tbFileName.Text, FileMode.Open));
            file.BaseStream.Position = 0;
            file.BaseStream.Flush();
            setProgressbarValue(pbStreamBar, 0);
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            btnPause.Enabled = false;
            enableButton(btnSendCommand, true);
            enableJoystick(true);
            if (btnPause.Text == "Unpause")
            {
                EventArgs en = new EventArgs();
                btnPause_Click(btnPause, en);
            }
            while (pbStreamBar.Value != 0)
                setProgressbarValue(pbStreamBar, 0);
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if(btnPause.Text == "Pause")
            {
                btnPause.Text = "Unpause";
                mreStreaming.Reset();
                send(feedHold);
                btnSendCommand.Enabled = true;
                enableJoystick(true);
            }
            else
            {
                btnPause.Text = "Pause";
                mreStreaming.Set();
                send(feedResume);
                btnSendCommand.Enabled = false;
                enableJoystick(false);
            }
            
        }

        private void pJoystick_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                g = pJoystick.CreateGraphics();
                Pen p = new Pen(Color.Red, 5);
                g.FillEllipse(p.Brush, pJoystick.ClientRectangle);
                p = new Pen(Color.Blue, 5);
                int x = e.Location.X - pJoystick.ClientRectangle.Size.Width / 2;
                int y = e.Location.Y - pJoystick.ClientRectangle.Size.Height / 2;
                int radius = pJoystick.ClientRectangle.Size.Width / 2;
                double len = Math.Sqrt(x * x + y * y);
                if (len < radius - 10)
                {
                    g.FillEllipse(p.Brush, e.Location.X - 10, e.Location.Y - 10, 20, 20);
                    joystick(x, y);
                }
                else
                {
                    double xHelp = x / len;
                    double yHelp = y / len;
                    xHelp *= radius - 10;
                    yHelp *= radius - 10;
                    x = (int)xHelp + pJoystick.ClientRectangle.Size.Width / 2;
                    y = (int)yHelp + pJoystick.ClientRectangle.Size.Height / 2;
                    g.FillEllipse(p.Brush, x - 10, y - 10, 20, 20);
                    joystick((int)xHelp, (int)yHelp);
                }
                if (jogLoop == null)
                {
                    joystickloop = true;
                    joystickLoopStart();
                }
            }
        }

        private void Joystick_MouseUp(object sender, MouseEventArgs e)
        {
            if(sender == pJoystick)
            {
                g.Clear(pJoystick.BackColor);
                pJoystick_Paint(sender, (EventArgs)e);
            }
            else if(sender == tbarJoystick)
            {
                tbarJoystick.Value = 0;
            }
            joystickloop = false;
            jogLoop.Abort();
            while (jogLoop.ThreadState != ThreadState.Aborted);
            jogLoop = null;
            sendHidden(jogCancel);
            mrePortCommunication.WaitOne();
            mrePortCommunication.Reset();
            mrePortCommunication.Set();
        }

        private void tbarJoystick_Scroll(object sender, EventArgs e)
        {
            int feed = tbarJoystick.Value * WFSettings.maxJoystickFeed / tbarJoystick.Maximum;
            double joystickStep = WFSettings.maxJoystickStep;
            if (feed < 0)
            {
                jogString = "$J=G91 Z-" + joystickStep.ToString(WFSettings.decAccuracy) + " F" + (feed*-1).ToString();
            }
            else
            {
                jogString = "$J=G91 Z" + joystickStep.ToString(WFSettings.decAccuracy) + " F" + feed.ToString();
            }
            if (jogLoop == null)
            {
                joystickloop = true;
                joystickLoopStart();
            }
        }

        private void pJoystick_Paint(object sender, EventArgs e)
        {
            Pen p = new Pen(Color.Red, 5);
            Graphics g = this.pJoystick.CreateGraphics();
            //g.DrawEllipse(p, pJoystick.ClientRectangle);
            g.FillEllipse(p.Brush, pJoystick.ClientRectangle);
            p = new Pen(Color.Blue, 5);
            g.FillEllipse(p.Brush, pJoystick.Width/2-10, pJoystick.Height/2-10, 20, 20);
        }

        private void tbarFeed_Scroll(object sender, EventArgs e)
        {
            tbFeed.Text = tbarFeed.Value.ToString();
        }

        private void tbarFeed_MouseUp(object sender, MouseEventArgs e)
        {
            feedOverride(tbarFeed.Value);
        }

        private void tbFeed_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if(e.KeyChar == (char)13)   //enter
            {
                int value = int.Parse(tb.Text);
                if(value < 11)
                {
                    value = 11;
                }
                else if(value > 200)
                {
                    value = 200;
                }
                tbarFeed.Value = value;
                tb.Text = value.ToString();
                feedOverride(value);
                return;
            }
            if(e.KeyChar == ' ')
            {
                e.Handled = true;
                return;
            }
            if (!char.IsControl(e.KeyChar))
            {
                string text = tbFeed.Text;
                text = text.Insert(tbFeed.SelectionStart, "" + e.KeyChar);
                int value;
                if (!int.TryParse(text, out value))
                {
                    e.Handled = true;
                }
            }
        }

        private void tbarSpindleOv_Scroll(object sender, EventArgs e)
        {
            tbSpindleOv.Text = tbarSpindleOv.Value.ToString();
        }

        private void tbarSpindleOv_MouseUp(object sender, MouseEventArgs e)
        {
            spindleOverride(tbarSpindleOv.Value);
        }

        private void tbSpindleOv_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (e.KeyChar == (char)13)   //enter
            {
                int value = int.Parse(tb.Text);
                if (value < 11)
                {
                    value = 11;
                }
                else if (value > 200)
                {
                    value = 200;
                }
                tbarSpindleOv.Value = value;
                tb.Text = value.ToString();
                spindleOverride(value);
                return;
            }
            if (e.KeyChar == ' ')
            {
                e.Handled = true;
                return;
            }
            if (!char.IsControl(e.KeyChar))
            {
                string text = tbSpindleOv.Text;
                text = text.Insert(tbSpindleOv.SelectionStart, "" + e.KeyChar);
                int value;
                if (!int.TryParse(text, out value))
                {
                    e.Handled = true;
                }
            }
        }

        private void tbarSpindleRPM_Scroll(object sender, EventArgs e)
        {
            tbSpindleRPM.Text = tbarSpindleRPM.Value.ToString();
        }

        private void tbarSpindleRPM_MouseUp(object sender, MouseEventArgs e)
        {
            spindleSpeed = tbarSpindleRPM.Value;
        }

        private void tbSpindleRPM_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (e.KeyChar == (char)13)   //enter
            {
                int value = int.Parse(tb.Text);
                if (value < 0)
                {
                    value = 0;
                }
                else if (value > tbarSpindleRPM.Maximum)
                {
                    value = tbarSpindleRPM.Maximum;
                }
                tbarSpindleRPM.Value = value;
                tb.Text = value.ToString();
                spindleSpeed = value;
                return;
            }
            if (e.KeyChar == ' ')
            {
                e.Handled = true;
                return;
            }
            if (!char.IsControl(e.KeyChar))
            {
                string text = tbSpindleRPM.Text;
                text = text.Insert(tbSpindleOv.SelectionStart, "" + e.KeyChar);
                int value;
                if (!int.TryParse(text, out value))
                {
                    e.Handled = true;
                }
            }
        }

        private void tsmiSettings_Click(object sender, EventArgs e)
        {
            refreshPosThread.Abort();
            port.Close();
            settings = new WFSettings();
            settings.initSettings();
            settings.Show();
            settings.FormClosed += new FormClosedEventHandler(Settings_Close);
        }

        private void Settings_Close(object sender, FormClosedEventArgs e)
        {
            WFSettings s = (WFSettings)sender;
            updateMaxSpindleRPM();
            port = new SerialPort(WFSettings.comport, WFSettings.baudrate);
            try
            {
                port.Open();
            }
            catch
            {
                //Nope
            }
            s.FormClosed -= new FormClosedEventHandler(Settings_Close);
            mreStreaming.Set();
            mrePortCommunication.Set();
            refreshPosThread = new Thread(refreshPos);
            refreshPosThread.Start();
        }

        private void btnSpindleOn_Click(object sender, EventArgs e)
        {
            send("M03 S" + spindleSpeed.ToString());
        }

        private void btnSpindleOff_Click(object sender, EventArgs e)
        {
            send("M05");
        }

        private void btnUnlockCNC_Click(object sender, EventArgs e)
        {
            sendHidden(softReset);
            send("$X");
        }

        #endregion

        #region Firebase Push Messages

        public static String SendNotification(String Title, String Message)
        {
            var result = "-1";
            var webAddr = "https://fcm.googleapis.com/fcm/send";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Add(HttpRequestHeader.Authorization, "key=AAAAlOhZ22g:APA91bH3S3df2pqroy8zlzXrCtqjTHMUJMAVUsXVKWot9l3hgPuzZuylzFfHOQrT3cJPZH_fi2cxqz2vOEvTGXaQD3OuwL_ily7qCh250Tj9MYHU48HPNTLcig2Z3UTb7DzEBh0uB3dA");
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string strNJson = @"{
                    ""to"": ""/topics/ServiceNow"",
                    ""notification"": 
                    {
                        ""title"": ""TITLE"",
                        ""text"": ""NOTIFICATIONTEXT"",
                        ""sound"":""default""
                    }
                }";
                strNJson = strNJson.Replace("TITLE", Title);
                strNJson = strNJson.Replace("NOTIFICATIONTEXT", Message);
                streamWriter.Write(strNJson);
                streamWriter.Flush();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            return result;
        }

        #endregion
    }
}

