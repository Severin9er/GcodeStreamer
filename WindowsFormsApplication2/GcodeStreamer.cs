﻿#define DEBUG

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
            "?",
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
            "" + (char)0x9A
        };

        const string decAccuracy = "0.000000";

        const char decSplitChar = ',';

        #endregion

        #region properties

        string feedHold = "!";
        string feedResume = "~";
        string softReset = "" + (char)0x18;
        string requestState = "?";
        string jogCancel = "" + (char)0x85;
        string portName = "COM8";
        int baudrate = 115200;
        int maxFeed = 300;
        double maxStep = 0.1;
        int refreshPosInterval = 100;

        #endregion

        #region delegates and events

        delegate void dEnableButton(Button btn, bool enable);
        delegate void dSetProgressbarValue(ProgressBar pb, int value);
        delegate void dPrintText(TextBox tb, string text);
        delegate void dSetText(TextBox tb, string text);
        delegate void dEnableJoystick(bool enable);
        ManualResetEvent mre;

        #endregion

        #region variables

        Thread streamingThread;
        Thread jogLoop;
        Thread refreshPosThread;
        StreamReader file;
        SerialPort port;
        LinkedList<tool> usedTools;
        int currentToolNr;
        position toolChangePos;
        Graphics g;
        int lastFeed;
        volatile bool joystickloop;
        volatile string jogString;

        #endregion

        public GcodeStreamer()
        {
            InitializeComponent();
            g = this.pJoystick.CreateGraphics();
            mre = new ManualResetEvent(true);
            port = new SerialPort(portName, baudrate);
            toolChangePos = new position();
            usedTools = new LinkedList<tool>();
            currentToolNr = -1;
            lastFeed = 100;
            try {
                port.Open();
            }
            catch(Exception e) {
                //Error-Message
            }
            refreshPosThread = new Thread(refreshPos);
            refreshPosThread.Start();
        }

        private void streaming()
        {
            if (file == null)
                return;
            if (!file.BaseStream.CanRead)
                return;
            while(!file.EndOfStream)
            {
                
                string nextLine = file.ReadLine();
                
                int status = (int)(100*file.BaseStream.Position / file.BaseStream.Length);
                mre.WaitOne();
                setProgressbarValue(pbStreamBar, status);

                //Remove spaces at the beginning
                while(nextLine.StartsWith(" "))
                {
                    nextLine = nextLine.Remove(0, 1);
                }
                if(nextLine.Contains(";"))
                {
                    nextLine = nextLine.Substring(0,nextLine.IndexOf(';'));
                }
                if(nextLine.Contains("("))
                {
                    bracket(nextLine);
                    nextLine = "";
                }
                if(nextLine.Contains("T") && nextLine.Contains("M06"))  //Tool change
                {
                    toolChange(nextLine);
                    nextLine = "";
                }
                if (nextLine != "")
                {
                    while(nextLine.LastIndexOf(' ') == nextLine.Length-1)
                    {
                        nextLine = nextLine.Remove(nextLine.Length - 1, 1);
                    }
                    mre.WaitOne();
                    send(nextLine);
                    //Thread.Sleep(10);
                }
                
            }
            //finally

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
                position backPos = getPosition();
                backPos.z = toolChangePos.z;
                enableButton(btnSendCommand, true);
                enableJoystick(true);
                moveToPos(toolChangePos);
                //send("M05"); //Stop Spindle //not needed, because it is included in the Gcode
                MessageBox.Show("Change Tool to T" + nr.ToString() + ":\r\n\r\nSize: " + usedTools.ToArray()[nr - 1].size+"\r\n\r\nDO NOT TIGHTEN TOOL TOO FIRMLY YET", "Change Tool", MessageBoxButtons.OK);
                currentToolNr = nr; //Set current tool to the new tool
                moveToPos(backPos); //Go back to previous x and y position (z in safe height)
                //send("M03");  //Start Spindle again // not needed because already included in Gcode
                enableButton(btnSendCommand, false);
                enableJoystick(false);
            }
            else
            {
                enableButton(btnSendCommand, true);
                enableJoystick(true);
                MessageBox.Show("Tighten the Tool now firmly and make sure, it has contact to the surface (Z=0).", "Change Tool", MessageBoxButtons.OK);
                enableButton(btnSendCommand, false);
                enableJoystick(false);
            }
        }

        private void moveToPos(position p)
        {
            send("G01 Z" + p.z.ToString(decAccuracy));  //Move Z first
            send("G01 X" + p.x.ToString(decAccuracy) + " Y" + p.y.ToString(decAccuracy));   //Move X,Y
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
            if(!needReply)
            {
                port.Write(cmd);
            }
            if(needReply)
            {
                port.WriteLine(cmd);
                reply = port.ReadLine();
                if (reply != "ok")
                {
                    tbConsole.AppendText(reply+"\r\n");
                }
            }
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
            if (!needReply)
            {
                port.Write(cmd);
            }
            if (needReply)
            {
                port.WriteLine(cmd);
                reply = port.ReadLine(); //wait on reply
                if (reply != "ok")
                {
                    //error message
                }
            }
            return reply;
        }

        private position getPosition()
        {
            position pos = new position();
            pos.x = 0.0;
            pos.y = 0.0;
            pos.z = 0.0;
            string reply = sendHidden(requestState);
            if (reply != "")
            {
                string[] s = reply.Split('|');
                for (int i = 0; i < s.Count(); i++)
                {
                    if (s[i].StartsWith("MPos:"))
                    {
                        reply = s[i];
                        i = s.Count();
                    }
                }
                reply = reply.Remove(0, 5);
                s = reply.Split(',');
                for (int i = 0; i < s.Count(); i++)
                {
                    s[i] = s[i].Replace('.', decSplitChar);
                }
                pos.x = double.Parse(s[0]);
                pos.y = double.Parse(s[1]);
                pos.z = double.Parse(s[2]);
            }
            return pos;
        }

        private void refreshPos()
        {
            position pos;
            while(true)
            {
                pos = getPosition();
                setText(tbXPos, pos.x.ToString("0.000"));
                setText(tbYPos, pos.y.ToString("0.000"));
                setText(tbZPos, pos.z.ToString("0.000"));
                Thread.Sleep(refreshPosInterval);
            }
        }

        private void bracket(string line)
        {
            if(line.Contains("tool change at")) //Parse Tool Change Position out of file
            {
                while(!char.IsDigit(line.First<char>()))    //Remove part before first number
                {
                    line = line.Remove(0, 1);
                }
                string[] s = line.Split(' ');
                for (int i = 0; i < s.Count(); i++)
                {
                    s[i]=s[i].Replace('.', decSplitChar);
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
                        send("" + (char)0x91);    //increase by 10
                    }
                    for (int i = 0; i < (val - lastFeed) % 10; i++)
                    {
                        send("" + (char)0x93);    //Increase by 1
                    }
                }
                else
                {
                    for (int i = 0; i < (lastFeed - val) / 10; i++)
                    {
                        send("" + (char)0x92);    //decrease by 10
                    }
                    for (int i = 0; i < (lastFeed - val) % 10; i++)
                    {
                        send("" + (char)0x94);    //decrease by 1
                    }
                }
                lastFeed = val;
            }
            
        }

        private void joystick(int x, int y)
        {
            int feed = (int)Math.Sqrt(x * x + y * y);
            int max = pJoystick.Width / 2 - 10;
            double xStep = maxStep * x / max;
            double yStep = maxStep * y / max;
            feed = feed * maxFeed;
            feed /= max;
            jogString = "$J=G91 X" + xStep.ToString(decAccuracy) + "Y" + yStep.ToString(decAccuracy) + "F" + feed;
        }

        private void joystickLoopStart()
        {
            jogLoop = new Thread(joystickLoop);
            jogLoop.Start();
        }

        private void joystickLoop()
        {
            while(joystickloop)
            {
                send(jogString);
            }
        }

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
            if(jogLoop != null)
            {
                jogLoop.Abort();
                jogLoop = null;
                send(jogCancel);
            }
            if(streamingThread != null && streamingThread.ThreadState == ThreadState.Running)
            {
                EventArgs ev = new EventArgs();
                btnStop_Click((object)btnStop, ev);
            }
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
            file = new StreamReader(openDialog.OpenFile());
            btnStart.Enabled = true;
            usedTools = new LinkedList<tool>();
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
            streamingThread.Abort();
            send(softReset);  //Send SoftReset to stop CNC and forget last command
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
                mre.Reset();
                send(feedHold);
                btnSendCommand.Enabled = true;
                enableJoystick(true);
            }
            else
            {
                btnPause.Text = "Pause";
                mre.Set();
                send(feedResume);
                btnSendCommand.Enabled = false;
                enableJoystick(false);
            }
            
        }

        private void pJoystick_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                g.Dispose();
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
            send(jogCancel);
        }

        private void tbarJoystick_Scroll(object sender, EventArgs e)
        {
            int feed = tbarJoystick.Value * maxFeed / tbarJoystick.Maximum;
            if (feed < 0)
            {
                jogString = "$J=G91 Z-" + maxStep.ToString(decAccuracy) + " F" + (feed*-1).ToString();
            }
            else
            {
                jogString = "$J=G91 Z" + maxStep.ToString(decAccuracy) + " F" + feed.ToString();
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
        

        #endregion
    }
}

