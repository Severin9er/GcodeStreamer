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

namespace WindowsFormsApplication2
{
    public partial class WFSettings : Form
    {
        public static string comport = "";
        public static int baudrate = 0;
        public static int maxJoystickFeed = 0;
        public static double maxJoystickStep = 0.0;
        public static string decAccuracy = "";
        public static int refreshPosInterval = 0;
        public static char decSplitChar = ',';
        public static int maxSpindleRPM = 0;
        public static double pcbDimX = 0.0;
        public static double pcbDimY = 0.0;
        public static bool firstNotification = true;
        public static bool lastNotification = true;
        public static bool finishedNotification = true;
        public static bool errorNotification = true;

        private const string comportDefault = "COM8";
        private const int baudrateDefault = 115200;
        private const int maxJoystickFeedDefault = 300;
        private const double maxJoystickStepDefault = 0.1;
        private const int refreshPosIntervalDefault = 100;
        private const string decAccuracyDefault = "0.000000";
        private const char decSplitCharDefault = ',';
        private const int maxSpindleRPMDefault = 1000;
        private const double pcbDimXDefault = 100.0;
        private const double pcbDimYDefault = 80.0;
        private const bool firstNotificationDefault = true;
        private const bool lastNotificationDefault = true;
        private const bool finishedNotificationDefault = true;
        private const bool errorNotificationDefault = true;

        public WFSettings()
        {
            InitializeComponent();
        }

        public void restoreDefault()
        {
            comport = comportDefault;
            baudrate = baudrateDefault;
            maxJoystickFeed = maxJoystickFeedDefault;
            maxJoystickStep = maxJoystickStepDefault;
            decAccuracy = decAccuracyDefault;
            refreshPosInterval = refreshPosIntervalDefault;
            decSplitChar = decSplitCharDefault;
            maxSpindleRPM = maxSpindleRPMDefault;
            pcbDimX = pcbDimXDefault;
            pcbDimY = pcbDimYDefault;
            firstNotification = firstNotificationDefault;
            lastNotification = lastNotificationDefault;
            finishedNotification = finishedNotificationDefault;
            errorNotification = errorNotificationDefault;

            initSettings();
        }

        private void refreshPortList()
        {
            while(cbComport.Items.Count > 0)
            {
                cbComport.Items.RemoveAt(0);
            }
            string[] portNameList = SerialPort.GetPortNames();
            for (int i = 0; i < portNameList.Count(); i++)
            {
                cbComport.Items.Add(portNameList[i]);
            }
        }

        private void cbComport_SelectedIndexChanged(object sender, EventArgs e)
        {
            comport = cbComport.Text;
        }

        public void initSettings()
        {
            cbComport.Text = comport;
            cbBaudrate.Text = baudrate.ToString();
            refreshPortList();
            switch(decSplitChar)
            {
                case ',': cbDecSplitter.SelectedIndex = 0; break;
                case '.': cbDecSplitter.SelectedIndex = 1; break;
            }
            tbDecAccuracy.Text = decAccuracy;
            tbMaxJoyFeed.Text = maxJoystickFeed.ToString();
            tbMaxJoyStep.Text = maxJoystickStep.ToString();
            tbRefreshInterval.Text = refreshPosInterval.ToString();
            tbMaxSpindleRPM.Text = maxSpindleRPM.ToString();
            tbPCBDimX.Text = pcbDimX.ToString();
            tbPCBDimY.Text = pcbDimY.ToString();
            cbFirstNotification.Checked = firstNotification;
            cbLastNotification.Checked = lastNotification;
            cbNotificationFinished.Checked = finishedNotification;
            cbNotificationError.Checked = errorNotification;
        }

        public  void saveFile()
        {
            StreamWriter s = new StreamWriter("settings.txt", false);
            s.WriteLine(comport);
            s.WriteLine(baudrate);
            s.WriteLine(maxJoystickFeed);
            s.WriteLine(maxJoystickStep);
            s.WriteLine(refreshPosInterval);
            s.WriteLine(decAccuracy);
            s.WriteLine(decSplitChar);
            s.WriteLine(maxSpindleRPM);
            s.WriteLine(pcbDimX);
            s.WriteLine(pcbDimY);
            s.WriteLine(firstNotification);
            s.WriteLine(lastNotification);
            s.WriteLine(finishedNotification);
            s.WriteLine(errorNotification);
            s.Close();
        }

        public  void readFile()
        {
            StreamReader s = new StreamReader("settings.txt");
            try
            {
                comport = s.ReadLine();
                baudrate = int.Parse(s.ReadLine());
                maxJoystickFeed = int.Parse(s.ReadLine());
                maxJoystickStep = double.Parse(s.ReadLine());
                refreshPosInterval = int.Parse(s.ReadLine());
                decAccuracy = s.ReadLine();
                decSplitChar = char.Parse(s.ReadLine());
                maxSpindleRPM = int.Parse(s.ReadLine());
                pcbDimX = double.Parse(s.ReadLine());
                pcbDimY = double.Parse(s.ReadLine());
                firstNotification = bool.Parse(s.ReadLine());
                lastNotification = bool.Parse(s.ReadLine());
                finishedNotification = bool.Parse(s.ReadLine());
                errorNotification = bool.Parse(s.ReadLine());
            }
            finally
            {
                s.Close();
            }
        }

        private  void cbBaudrate_SelectedIndexChanged(object sender, EventArgs e)
        {
            baudrate = int.Parse(cbBaudrate.Text);
        }

        private  void cbDecSplitter_SelectedIndexChanged(object sender, EventArgs e)
        {
            decSplitChar = cbDecSplitter.Text.First();
        }

        private  void tbInt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private  void tbInt_Leave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if(tb.Text == "")
            {
                if (sender == tbMaxJoyFeed)
                {
                    tbMaxJoyFeed.Text = maxJoystickFeed.ToString();
                }
                else if (sender == tbRefreshInterval)
                {
                    tbRefreshInterval.Text = refreshPosInterval.ToString();
                }
            }
            else
            {
                if (sender == tbMaxJoyFeed)
                {
                    maxJoystickFeed = int.Parse(tbMaxJoyFeed.Text);
                }
                else if (sender == tbRefreshInterval)
                {
                    refreshPosInterval = int.Parse(tbRefreshInterval.Text);
                }
            }
            
        }

        private  void tbDecAccuracy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == ',')
            {
                e.KeyChar = '.';
            }
            if(e.KeyChar != '0' && e.KeyChar != '.' && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                return;
            }
            if(e.KeyChar == '.' && (tbDecAccuracy.SelectionStart == 0 || tbDecAccuracy.Text.Contains(".")))
            {
                e.Handled = true;
            }
        }

        private  void tbDecAccuracy_Leave(object sender, EventArgs e)
        {
            if(tbDecAccuracy.Text == "")
            {
                tbDecAccuracy.Text = decAccuracy;
                return;
            }
            decAccuracy = tbDecAccuracy.Text;
        }

        private  void tbDouble_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',' && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                return;
            }
            if ((e.KeyChar == '.' || e.KeyChar == ',') && (tb.SelectionStart == 0 || tb.Text.Contains(",") || tb.Text.Contains(".")))
            {
                e.Handled = true;
            }
        }

        private  void tbMaxJoyStep_Leave(object sender, EventArgs e)
        {
            if(tbMaxJoyStep.Text == "")
            {
                tbMaxJoyStep.Text = maxJoystickStep.ToString();
                return;
            }
            if (!double.TryParse(tbMaxJoyStep.Text, out maxJoystickStep))
            {
                if(tbMaxJoyStep.Text.Contains(","))
                {
                    tbMaxJoyStep.Text.Replace(',', '.');
                }
                else
                {
                    tbMaxJoyStep.Text.Replace('.', ',');
                }
                maxJoystickStep = double.Parse(tbMaxJoyStep.Text);
            }
            tbMaxJoyStep.Text = maxJoystickStep.ToString();
        }

        private  void tbPCBDimX_Leave(object sender, EventArgs e)
        {
            if (tbPCBDimX.Text == "")
            {
                tbPCBDimX.Text = pcbDimX.ToString();
                return;
            }
            if (!double.TryParse(tbPCBDimX.Text, out pcbDimX))
            {
                if (tbPCBDimX.Text.Contains(","))
                {
                    tbPCBDimX.Text.Replace(',', '.');
                }
                else
                {
                    tbPCBDimX.Text.Replace('.', ',');
                }
                pcbDimX = double.Parse(tbPCBDimX.Text);
            }
            tbPCBDimX.Text = pcbDimX.ToString();
        }

        private  void tbPCBDimY_Leave(object sender, EventArgs e)
        {
            if (tbPCBDimY.Text == "")
            {
                tbPCBDimY.Text = pcbDimY.ToString();
                return;
            }
            if (!double.TryParse(tbPCBDimY.Text, out pcbDimY))
            {
                if (tbPCBDimY.Text.Contains(","))
                {
                    tbPCBDimY.Text.Replace(',', '.');
                }
                else
                {
                    tbPCBDimY.Text.Replace('.', ',');
                }
                pcbDimY = double.Parse(tbPCBDimY.Text);
            }
            tbPCBDimY.Text = pcbDimY.ToString();
        }

        private  void btnSave_Click(object sender, EventArgs e)
        {
            saveFile();
            this.Close();
        }

        private  void btnRestore_Click(object sender, EventArgs e)
        {
            restoreDefault();
        }

        private  void tbMaxSpindleRPM_Leave(object sender, EventArgs e)
        {
            if (tbMaxSpindleRPM.Text != "")
            {
                maxSpindleRPM = int.Parse(tbMaxSpindleRPM.Text);
            }
            else
            {
                tbMaxSpindleRPM.Text = maxSpindleRPM.ToString();
            }
        }

        private  void cbLastNotification_CheckedChanged(object sender, EventArgs e)
        {
            lastNotification = cbLastNotification.Checked;
        }

        private  void cbFirstNotification_CheckedChanged(object sender, EventArgs e)
        {
            firstNotification = cbFirstNotification.Checked;
        }

        private void cbNotificationFinished_CheckedChanged(object sender, EventArgs e)
        {
            finishedNotification = cbNotificationFinished.Checked;
        }

        private void cbNotificationError_CheckedChanged(object sender, EventArgs e)
        {
            errorNotification = cbNotificationError.Checked;
        }
    }
}
