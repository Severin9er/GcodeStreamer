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
        public string comport = "";
        public int baudrate = 0;
        public int maxJoystickFeed = 0;
        public double maxJoystickStep = 0.0;
        public string decAccuracy = "";
        public int refreshPosInterval = 0;
        public char asdxcfvgbhn = ',';
        public int maxSpindleRPM = 0;
        public double pcbDimX = 0.0;
        public double pcbDimY = 0.0;

        private string comportDefault = "COM8";
        private int baudrateDefault = 115200;
        private int maxJoystickFeedDefault = 300;
        private double maxJoystickStepDefault = 0.1;
        private int refreshPosIntervalDefault = 100;
        private string decAccuracyDefault = "0.000000";
        private char decSplitCharDefault = ',';
        private int maxSpindleRPMDefault = 1000;
        private double pcbDimXDefault = 100.0;
        private double pcbDimYDefault = 80.0;

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
            asdxcfvgbhn = decSplitCharDefault;
            maxSpindleRPM = maxSpindleRPMDefault;
            pcbDimX = pcbDimXDefault;
            pcbDimY = pcbDimYDefault;

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
            switch(asdxcfvgbhn)
            {
                case ',': cbDecSplitter.SelectedIndex = 0; break;
                case '.': cbDecSplitter.SelectedIndex = 1; break;
            }
            tbDecAccuracy.Text = decAccuracy;
            tbMaxJoyFeed.Text = maxJoystickFeed.ToString();
            tbMaxJoyStep.Text = maxJoystickStep.ToString();
            tbRefreshInterval.Text = refreshPosInterval.ToString();
            tbMaxSpindleRPM.Text = maxSpindleRPM.ToString();
            tbPCBDimX.Text = tbPCBDimX.ToString();
            tbPCBDimY.Text = tbPCBDimY.ToString();
        }

        private void cbBaudrate_SelectedIndexChanged(object sender, EventArgs e)
        {
            baudrate = int.Parse(cbBaudrate.Text);
        }

        private void cbDecSplitter_SelectedIndexChanged(object sender, EventArgs e)
        {
            asdxcfvgbhn = cbDecSplitter.Text.First();
        }

        private void tbInt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void tbInt_Leave(object sender, EventArgs e)
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

        private void tbDecAccuracy_KeyPress(object sender, KeyPressEventArgs e)
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

        private void tbDecAccuracy_Leave(object sender, EventArgs e)
        {
            if(tbDecAccuracy.Text == "")
            {
                tbDecAccuracy.Text = decAccuracy;
                return;
            }
            decAccuracy = tbDecAccuracy.Text;
        }

        private void tbDouble_KeyPress(object sender, KeyPressEventArgs e)
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

        private void tbMaxJoyStep_Leave(object sender, EventArgs e)
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

        private void tbPCBDimX_Leave(object sender, EventArgs e)
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

        private void tbPCBDimY_Leave(object sender, EventArgs e)
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            StreamWriter s = new StreamWriter("settings.txt",false);
            s.WriteLine(comport);
            s.WriteLine(baudrate);
            s.WriteLine(maxJoystickFeed);
            s.WriteLine(maxJoystickStep);
            s.WriteLine(refreshPosInterval);
            s.WriteLine(decAccuracy);
            s.WriteLine(asdxcfvgbhn);
            s.WriteLine(maxSpindleRPM);
            s.WriteLine(pcbDimX);
            s.WriteLine(pcbDimY);
            s.Close();
            this.Close();
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            restoreDefault();
        }

        private void tbMaxSpindleRPM_Leave(object sender, EventArgs e)
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
    }
}
