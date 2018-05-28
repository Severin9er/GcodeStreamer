using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Gcode_Editor
{
    public partial class GcodeEditor : Form
    {
        double xMin, yMin, xMax, yMax;
        StreamReader inFile;
        StreamWriter outFile;
        public GcodeEditor()
        {
            InitializeComponent();
            xMin = double.Parse(tbXMin.Text);
            yMin = double.Parse(tbYMin.Text);
            xMax = double.Parse(tbXMax.Text);
            yMax = double.Parse(tbYMax.Text);
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            inFile = new StreamReader(new FileStream(tbFileName.Text,FileMode.Open));
            outFile = new StreamWriter(new FileStream(tbFileName.Text.Insert(tbFileName.Text.LastIndexOf('.'), "_out"),FileMode.Create));

            string line = inFile.ReadLine();
            string writeLine = "";
            while(line != null)
            {
                writeLine = line;
                line = line.Replace(" ", "");  //Remove all whitespaces
                if(line.StartsWith("M03") && !line.Contains("S"))
                {
                    writeLine = "M03 S1000";
                }
                if(line.StartsWith("G0"))  //G00 or G01 code
                {
                    double xTarget, yTarget;
                    getXY(line, out xTarget, out yTarget);
                    if(xTarget < xMin || xTarget > xMax || yTarget < yMin || yTarget > yMax)
                    {
                        writeLine = "";
                    }
                }
                if (writeLine != "")
                {
                    outFile.WriteLine(writeLine);
                }
                line = inFile.ReadLine();
            }
            inFile.Close();
            outFile.Close();
            MessageBox.Show("Conversion complete");
        }  

        private void getXY(string line, out double x, out double y)
        {
            if(line.Contains('X'))
            {
                string xArg = line.Substring(line.IndexOf('X'));
                xArg = xArg.Replace("X", "");
                for(int i = 0; i < xArg.Length; i++)
                {
                    if(char.IsLetter(xArg.ElementAt(i)))
                    {
                        xArg = xArg.Substring(0, i);
                    }
                }
                x = double.Parse(xArg);
            }
            else
            {
                x = double.NaN;
            }
            if (line.Contains('Y'))
            {
                string yArg = line.Substring(line.IndexOf('Y'));
                yArg = yArg.Replace("Y", "");
                foreach (char c in yArg)
                {
                    if (char.IsLetter(c))
                    {
                        yArg = yArg.Substring(0, yArg.IndexOf(c));
                    }
                }
                y = double.Parse(yArg);
            }
            else
            {
                y = double.NaN;
            }
        }

        private void tbOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            if(!string.IsNullOrEmpty(ofd.FileName))
            {
                tbFileName.Text = ofd.FileName;
                btnConvert.Enabled = true;
            }
        }

        private void tbKeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',' && !Char.IsControl(e.KeyChar) && e.KeyChar != '-')
            {
                e.Handled = true;
                return;
            }
            if ((e.KeyChar == '.' || e.KeyChar == ',') && (tb.SelectionStart == 0 || tb.Text.Contains(",") || tb.Text.Contains(".")))
            {
                e.Handled = true;
            }
            if(e.KeyChar == '-' && (tb.Text.Contains('-') || tb.SelectionStart != 0))
            {
                e.Handled = true;
            }
        }

        private void tbLeave(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            double value = 0;
            
            if (!string.IsNullOrEmpty(tb.Text))
            {
                tb.Text.Replace('.', ',');
                if (double.TryParse(tb.Text, out value))
                {
                    if (tb.Equals(tbXMin))
                    {
                        xMin = value;
                    }
                    else if (tb.Equals(tbXMax))
                    {
                        xMax = value;
                    }
                    else if (tb.Equals(tbYMin))
                    {
                        yMin = value;
                    }
                    else if (tb.Equals(tbYMax))
                    {
                        yMax = value;
                    }
                }
            }
            tbXMin.Text = xMin.ToString();
            tbYMin.Text = yMin.ToString();
            tbXMax.Text = xMax.ToString();
            tbYMax.Text = yMax.ToString();
        }
    }
}
