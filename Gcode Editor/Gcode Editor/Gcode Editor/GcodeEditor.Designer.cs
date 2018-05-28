namespace Gcode_Editor
{
    partial class GcodeEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbFileName = new System.Windows.Forms.TextBox();
            this.tbOpenFile = new System.Windows.Forms.Button();
            this.tbXMin = new System.Windows.Forms.TextBox();
            this.tbXMax = new System.Windows.Forms.TextBox();
            this.tbYMin = new System.Windows.Forms.TextBox();
            this.tbYMax = new System.Windows.Forms.TextBox();
            this.btnConvert = new System.Windows.Forms.Button();
            this.lblMin = new System.Windows.Forms.Label();
            this.lblMax = new System.Windows.Forms.Label();
            this.lblX = new System.Windows.Forms.Label();
            this.lblY = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbFileName
            // 
            this.tbFileName.Enabled = false;
            this.tbFileName.Location = new System.Drawing.Point(12, 12);
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.Size = new System.Drawing.Size(403, 22);
            this.tbFileName.TabIndex = 0;
            // 
            // tbOpenFile
            // 
            this.tbOpenFile.Location = new System.Drawing.Point(421, 9);
            this.tbOpenFile.Name = "tbOpenFile";
            this.tbOpenFile.Size = new System.Drawing.Size(75, 29);
            this.tbOpenFile.TabIndex = 1;
            this.tbOpenFile.Text = "Open";
            this.tbOpenFile.UseVisualStyleBackColor = true;
            this.tbOpenFile.Click += new System.EventHandler(this.tbOpenFile_Click);
            // 
            // tbXMin
            // 
            this.tbXMin.Location = new System.Drawing.Point(34, 98);
            this.tbXMin.Name = "tbXMin";
            this.tbXMin.Size = new System.Drawing.Size(53, 22);
            this.tbXMin.TabIndex = 2;
            this.tbXMin.Text = "0";
            this.tbXMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbXMin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbKeyPress);
            this.tbXMin.Leave += new System.EventHandler(this.tbLeave);
            // 
            // tbXMax
            // 
            this.tbXMax.Location = new System.Drawing.Point(93, 98);
            this.tbXMax.Name = "tbXMax";
            this.tbXMax.Size = new System.Drawing.Size(53, 22);
            this.tbXMax.TabIndex = 3;
            this.tbXMax.Text = "100";
            this.tbXMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbXMax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbKeyPress);
            this.tbXMax.Leave += new System.EventHandler(this.tbLeave);
            // 
            // tbYMin
            // 
            this.tbYMin.Location = new System.Drawing.Point(34, 126);
            this.tbYMin.Name = "tbYMin";
            this.tbYMin.Size = new System.Drawing.Size(53, 22);
            this.tbYMin.TabIndex = 4;
            this.tbYMin.Text = "0";
            this.tbYMin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbYMin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbKeyPress);
            this.tbYMin.Leave += new System.EventHandler(this.tbLeave);
            // 
            // tbYMax
            // 
            this.tbYMax.Location = new System.Drawing.Point(93, 126);
            this.tbYMax.Name = "tbYMax";
            this.tbYMax.Size = new System.Drawing.Size(53, 22);
            this.tbYMax.TabIndex = 5;
            this.tbYMax.Text = "70";
            this.tbYMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbYMax.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbKeyPress);
            this.tbYMax.Leave += new System.EventHandler(this.tbLeave);
            // 
            // btnConvert
            // 
            this.btnConvert.Enabled = false;
            this.btnConvert.Location = new System.Drawing.Point(203, 123);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(75, 29);
            this.btnConvert.TabIndex = 6;
            this.btnConvert.Text = "Convert";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // lblMin
            // 
            this.lblMin.AutoSize = true;
            this.lblMin.Location = new System.Drawing.Point(46, 78);
            this.lblMin.Name = "lblMin";
            this.lblMin.Size = new System.Drawing.Size(30, 17);
            this.lblMin.TabIndex = 7;
            this.lblMin.Text = "Min";
            // 
            // lblMax
            // 
            this.lblMax.AutoSize = true;
            this.lblMax.Location = new System.Drawing.Point(105, 78);
            this.lblMax.Name = "lblMax";
            this.lblMax.Size = new System.Drawing.Size(33, 17);
            this.lblMax.TabIndex = 8;
            this.lblMax.Text = "Max";
            // 
            // lblX
            // 
            this.lblX.AutoSize = true;
            this.lblX.Location = new System.Drawing.Point(9, 101);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(17, 17);
            this.lblX.TabIndex = 9;
            this.lblX.Text = "X";
            // 
            // lblY
            // 
            this.lblY.AutoSize = true;
            this.lblY.Location = new System.Drawing.Point(9, 129);
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(17, 17);
            this.lblY.TabIndex = 10;
            this.lblY.Text = "Y";
            // 
            // GcodeEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblY);
            this.Controls.Add(this.lblX);
            this.Controls.Add(this.lblMax);
            this.Controls.Add(this.lblMin);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.tbYMax);
            this.Controls.Add(this.tbYMin);
            this.Controls.Add(this.tbXMax);
            this.Controls.Add(this.tbXMin);
            this.Controls.Add(this.tbOpenFile);
            this.Controls.Add(this.tbFileName);
            this.Name = "GcodeEditor";
            this.Text = "Gcode Editor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbFileName;
        private System.Windows.Forms.Button tbOpenFile;
        private System.Windows.Forms.TextBox tbXMin;
        private System.Windows.Forms.TextBox tbXMax;
        private System.Windows.Forms.TextBox tbYMin;
        private System.Windows.Forms.TextBox tbYMax;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.Label lblMin;
        private System.Windows.Forms.Label lblMax;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.Label lblY;
    }
}

