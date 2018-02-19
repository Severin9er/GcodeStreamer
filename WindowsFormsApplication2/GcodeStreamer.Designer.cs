namespace GcodeStreamer
{
    partial class GcodeStreamer
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
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.tbFileName = new System.Windows.Forms.TextBox();
            this.tbConsole = new System.Windows.Forms.TextBox();
            this.tbCommands = new System.Windows.Forms.TextBox();
            this.btnSendCommand = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.pbStreamBar = new System.Windows.Forms.ProgressBar();
            this.pJoystick = new System.Windows.Forms.Panel();
            this.tbarFeed = new System.Windows.Forms.TrackBar();
            this.tbFeed = new System.Windows.Forms.TextBox();
            this.lblFeedP = new System.Windows.Forms.Label();
            this.lblFeed = new System.Windows.Forms.Label();
            this.tbarJoystick = new System.Windows.Forms.TrackBar();
            this.tbXPos = new System.Windows.Forms.TextBox();
            this.tbYPos = new System.Windows.Forms.TextBox();
            this.tbZPos = new System.Windows.Forms.TextBox();
            this.lblXPos = new System.Windows.Forms.Label();
            this.lblYPos = new System.Windows.Forms.Label();
            this.lblZPos = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsmiSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.pDrawing = new System.Windows.Forms.Panel();
            this.lblSpindleOv = new System.Windows.Forms.Label();
            this.lblSpindleOvP = new System.Windows.Forms.Label();
            this.tbSpindleOv = new System.Windows.Forms.TextBox();
            this.tbarSpindleOv = new System.Windows.Forms.TrackBar();
            this.lblSpindleRPM = new System.Windows.Forms.Label();
            this.tbSpindleRPM = new System.Windows.Forms.TextBox();
            this.tbarSpindleRPM = new System.Windows.Forms.TrackBar();
            this.lblSpindleRPMUnit = new System.Windows.Forms.Label();
            this.btnSpindleOn = new System.Windows.Forms.Button();
            this.btnSpindleOff = new System.Windows.Forms.Button();
            this.lblSpindleOnOff = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tbarFeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbarJoystick)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbarSpindleOv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbarSpindleRPM)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(454, 26);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(61, 33);
            this.btnOpenFile.TabIndex = 0;
            this.btnOpenFile.Text = "Open";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // tbFileName
            // 
            this.tbFileName.Enabled = false;
            this.tbFileName.Location = new System.Drawing.Point(12, 31);
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.Size = new System.Drawing.Size(437, 22);
            this.tbFileName.TabIndex = 1;
            // 
            // tbConsole
            // 
            this.tbConsole.Location = new System.Drawing.Point(13, 106);
            this.tbConsole.Multiline = true;
            this.tbConsole.Name = "tbConsole";
            this.tbConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbConsole.Size = new System.Drawing.Size(502, 343);
            this.tbConsole.TabIndex = 2;
            // 
            // tbCommands
            // 
            this.tbCommands.Location = new System.Drawing.Point(13, 478);
            this.tbCommands.Name = "tbCommands";
            this.tbCommands.Size = new System.Drawing.Size(436, 22);
            this.tbCommands.TabIndex = 3;
            this.tbCommands.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbCommands_KeyPress);
            // 
            // btnSendCommand
            // 
            this.btnSendCommand.Location = new System.Drawing.Point(455, 471);
            this.btnSendCommand.Name = "btnSendCommand";
            this.btnSendCommand.Size = new System.Drawing.Size(60, 36);
            this.btnSendCommand.TabIndex = 4;
            this.btnSendCommand.Text = "Send";
            this.btnSendCommand.UseVisualStyleBackColor = true;
            this.btnSendCommand.Click += new System.EventHandler(this.btnSendCommand_Click);
            // 
            // btnStart
            // 
            this.btnStart.Enabled = false;
            this.btnStart.Location = new System.Drawing.Point(13, 63);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(85, 30);
            this.btnStart.TabIndex = 5;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnPause
            // 
            this.btnPause.Enabled = false;
            this.btnPause.Location = new System.Drawing.Point(104, 63);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(85, 30);
            this.btnPause.TabIndex = 6;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(195, 63);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(85, 30);
            this.btnStop.TabIndex = 7;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // pbStreamBar
            // 
            this.pbStreamBar.Location = new System.Drawing.Point(293, 65);
            this.pbStreamBar.Name = "pbStreamBar";
            this.pbStreamBar.Size = new System.Drawing.Size(223, 27);
            this.pbStreamBar.TabIndex = 8;
            // 
            // pJoystick
            // 
            this.pJoystick.BackColor = System.Drawing.SystemColors.Control;
            this.pJoystick.Location = new System.Drawing.Point(579, 301);
            this.pJoystick.Name = "pJoystick";
            this.pJoystick.Size = new System.Drawing.Size(200, 200);
            this.pJoystick.TabIndex = 15;
            this.pJoystick.Paint += new System.Windows.Forms.PaintEventHandler(this.pJoystick_Paint);
            this.pJoystick.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pJoystick_MouseMove);
            this.pJoystick.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Joystick_MouseUp);
            // 
            // tbarFeed
            // 
            this.tbarFeed.Location = new System.Drawing.Point(633, 225);
            this.tbarFeed.Maximum = 200;
            this.tbarFeed.Minimum = 11;
            this.tbarFeed.Name = "tbarFeed";
            this.tbarFeed.Size = new System.Drawing.Size(230, 56);
            this.tbarFeed.TabIndex = 16;
            this.tbarFeed.TickFrequency = 10;
            this.tbarFeed.Value = 100;
            this.tbarFeed.Scroll += new System.EventHandler(this.tbarFeed_Scroll);
            this.tbarFeed.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tbarFeed_MouseUp);
            // 
            // tbFeed
            // 
            this.tbFeed.Location = new System.Drawing.Point(869, 225);
            this.tbFeed.Name = "tbFeed";
            this.tbFeed.Size = new System.Drawing.Size(61, 22);
            this.tbFeed.TabIndex = 19;
            this.tbFeed.Text = "100";
            this.tbFeed.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbFeed_KeyPress);
            // 
            // lblFeedP
            // 
            this.lblFeedP.AutoSize = true;
            this.lblFeedP.Location = new System.Drawing.Point(936, 225);
            this.lblFeedP.Name = "lblFeedP";
            this.lblFeedP.Size = new System.Drawing.Size(20, 17);
            this.lblFeedP.TabIndex = 22;
            this.lblFeedP.Text = "%";
            // 
            // lblFeed
            // 
            this.lblFeed.AutoSize = true;
            this.lblFeed.Location = new System.Drawing.Point(580, 230);
            this.lblFeed.Name = "lblFeed";
            this.lblFeed.Size = new System.Drawing.Size(44, 17);
            this.lblFeed.TabIndex = 25;
            this.lblFeed.Text = "Feed:";
            // 
            // tbarJoystick
            // 
            this.tbarJoystick.Location = new System.Drawing.Point(810, 301);
            this.tbarJoystick.Maximum = 100;
            this.tbarJoystick.Minimum = -100;
            this.tbarJoystick.Name = "tbarJoystick";
            this.tbarJoystick.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tbarJoystick.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tbarJoystick.Size = new System.Drawing.Size(56, 199);
            this.tbarJoystick.TabIndex = 26;
            this.tbarJoystick.TickFrequency = 10;
            this.tbarJoystick.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tbarJoystick.Scroll += new System.EventHandler(this.tbarJoystick_Scroll);
            this.tbarJoystick.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Joystick_MouseUp);
            // 
            // tbXPos
            // 
            this.tbXPos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbXPos.Enabled = false;
            this.tbXPos.Location = new System.Drawing.Point(579, 31);
            this.tbXPos.Name = "tbXPos";
            this.tbXPos.Size = new System.Drawing.Size(55, 22);
            this.tbXPos.TabIndex = 27;
            // 
            // tbYPos
            // 
            this.tbYPos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbYPos.Enabled = false;
            this.tbYPos.Location = new System.Drawing.Point(667, 31);
            this.tbYPos.Name = "tbYPos";
            this.tbYPos.Size = new System.Drawing.Size(55, 22);
            this.tbYPos.TabIndex = 28;
            // 
            // tbZPos
            // 
            this.tbZPos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbZPos.Enabled = false;
            this.tbZPos.Location = new System.Drawing.Point(755, 31);
            this.tbZPos.Name = "tbZPos";
            this.tbZPos.Size = new System.Drawing.Size(55, 22);
            this.tbZPos.TabIndex = 29;
            // 
            // lblXPos
            // 
            this.lblXPos.AutoSize = true;
            this.lblXPos.Location = new System.Drawing.Point(552, 34);
            this.lblXPos.Name = "lblXPos";
            this.lblXPos.Size = new System.Drawing.Size(21, 17);
            this.lblXPos.TabIndex = 30;
            this.lblXPos.Text = "X:";
            this.lblXPos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblYPos
            // 
            this.lblYPos.AutoSize = true;
            this.lblYPos.Location = new System.Drawing.Point(640, 34);
            this.lblYPos.Name = "lblYPos";
            this.lblYPos.Size = new System.Drawing.Size(21, 17);
            this.lblYPos.TabIndex = 31;
            this.lblYPos.Text = "Y:";
            this.lblYPos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblZPos
            // 
            this.lblZPos.AutoSize = true;
            this.lblZPos.Location = new System.Drawing.Point(728, 34);
            this.lblZPos.Name = "lblZPos";
            this.lblZPos.Size = new System.Drawing.Size(21, 17);
            this.lblZPos.TabIndex = 32;
            this.lblZPos.Text = "Z:";
            this.lblZPos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSettings});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1093, 28);
            this.menuStrip1.TabIndex = 33;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsmiSettings
            // 
            this.tsmiSettings.Name = "tsmiSettings";
            this.tsmiSettings.Size = new System.Drawing.Size(74, 24);
            this.tsmiSettings.Text = "Settings";
            this.tsmiSettings.Click += new System.EventHandler(this.tsmiSettings_Click);
            // 
            // pDrawing
            // 
            this.pDrawing.BackColor = System.Drawing.Color.DarkGray;
            this.pDrawing.Location = new System.Drawing.Point(13, 563);
            this.pDrawing.Name = "pDrawing";
            this.pDrawing.Size = new System.Drawing.Size(766, 403);
            this.pDrawing.TabIndex = 34;
            // 
            // lblSpindleOv
            // 
            this.lblSpindleOv.AutoSize = true;
            this.lblSpindleOv.Location = new System.Drawing.Point(553, 168);
            this.lblSpindleOv.Name = "lblSpindleOv";
            this.lblSpindleOv.Size = new System.Drawing.Size(81, 17);
            this.lblSpindleOv.TabIndex = 38;
            this.lblSpindleOv.Text = "Spindle Ov:";
            // 
            // lblSpindleOvP
            // 
            this.lblSpindleOvP.AutoSize = true;
            this.lblSpindleOvP.Location = new System.Drawing.Point(936, 163);
            this.lblSpindleOvP.Name = "lblSpindleOvP";
            this.lblSpindleOvP.Size = new System.Drawing.Size(20, 17);
            this.lblSpindleOvP.TabIndex = 37;
            this.lblSpindleOvP.Text = "%";
            // 
            // tbSpindleOv
            // 
            this.tbSpindleOv.Location = new System.Drawing.Point(869, 163);
            this.tbSpindleOv.Name = "tbSpindleOv";
            this.tbSpindleOv.Size = new System.Drawing.Size(61, 22);
            this.tbSpindleOv.TabIndex = 36;
            this.tbSpindleOv.Text = "100";
            this.tbSpindleOv.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbSpindleOv_KeyPress);
            // 
            // tbarSpindleOv
            // 
            this.tbarSpindleOv.Location = new System.Drawing.Point(633, 163);
            this.tbarSpindleOv.Maximum = 200;
            this.tbarSpindleOv.Minimum = 11;
            this.tbarSpindleOv.Name = "tbarSpindleOv";
            this.tbarSpindleOv.Size = new System.Drawing.Size(230, 56);
            this.tbarSpindleOv.TabIndex = 35;
            this.tbarSpindleOv.TickFrequency = 10;
            this.tbarSpindleOv.Value = 100;
            this.tbarSpindleOv.Scroll += new System.EventHandler(this.tbarSpindleOv_Scroll);
            this.tbarSpindleOv.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tbarSpindleOv_MouseUp);
            // 
            // lblSpindleRPM
            // 
            this.lblSpindleRPM.AutoSize = true;
            this.lblSpindleRPM.Location = new System.Drawing.Point(530, 106);
            this.lblSpindleRPM.Name = "lblSpindleRPM";
            this.lblSpindleRPM.Size = new System.Drawing.Size(104, 17);
            this.lblSpindleRPM.TabIndex = 41;
            this.lblSpindleRPM.Text = "Spindle Speed:";
            // 
            // tbSpindleRPM
            // 
            this.tbSpindleRPM.Location = new System.Drawing.Point(869, 101);
            this.tbSpindleRPM.Name = "tbSpindleRPM";
            this.tbSpindleRPM.Size = new System.Drawing.Size(61, 22);
            this.tbSpindleRPM.TabIndex = 40;
            this.tbSpindleRPM.Text = "100";
            this.tbSpindleRPM.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbSpindleRPM_KeyPress);
            // 
            // tbarSpindleRPM
            // 
            this.tbarSpindleRPM.Location = new System.Drawing.Point(633, 101);
            this.tbarSpindleRPM.Maximum = 200;
            this.tbarSpindleRPM.Name = "tbarSpindleRPM";
            this.tbarSpindleRPM.Size = new System.Drawing.Size(230, 56);
            this.tbarSpindleRPM.TabIndex = 39;
            this.tbarSpindleRPM.TickFrequency = 10;
            this.tbarSpindleRPM.Value = 100;
            this.tbarSpindleRPM.Scroll += new System.EventHandler(this.tbarSpindleRPM_Scroll);
            this.tbarSpindleRPM.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tbarSpindleRPM_MouseUp);
            // 
            // lblSpindleRPMUnit
            // 
            this.lblSpindleRPMUnit.AutoSize = true;
            this.lblSpindleRPMUnit.Location = new System.Drawing.Point(936, 104);
            this.lblSpindleRPMUnit.Name = "lblSpindleRPMUnit";
            this.lblSpindleRPMUnit.Size = new System.Drawing.Size(38, 17);
            this.lblSpindleRPMUnit.TabIndex = 42;
            this.lblSpindleRPMUnit.Text = "RPM";
            // 
            // btnSpindleOn
            // 
            this.btnSpindleOn.Location = new System.Drawing.Point(980, 96);
            this.btnSpindleOn.Name = "btnSpindleOn";
            this.btnSpindleOn.Size = new System.Drawing.Size(43, 33);
            this.btnSpindleOn.TabIndex = 43;
            this.btnSpindleOn.Text = "On";
            this.btnSpindleOn.UseVisualStyleBackColor = true;
            this.btnSpindleOn.Click += new System.EventHandler(this.btnSpindleOn_Click);
            // 
            // btnSpindleOff
            // 
            this.btnSpindleOff.Location = new System.Drawing.Point(1029, 96);
            this.btnSpindleOff.Name = "btnSpindleOff";
            this.btnSpindleOff.Size = new System.Drawing.Size(43, 33);
            this.btnSpindleOff.TabIndex = 44;
            this.btnSpindleOff.Text = "Off";
            this.btnSpindleOff.UseVisualStyleBackColor = true;
            this.btnSpindleOff.Click += new System.EventHandler(this.btnSpindleOff_Click);
            // 
            // lblSpindleOnOff
            // 
            this.lblSpindleOnOff.AutoSize = true;
            this.lblSpindleOnOff.Location = new System.Drawing.Point(977, 70);
            this.lblSpindleOnOff.Name = "lblSpindleOnOff";
            this.lblSpindleOnOff.Size = new System.Drawing.Size(101, 17);
            this.lblSpindleOnOff.TabIndex = 45;
            this.lblSpindleOnOff.Text = "Spindle On/Off";
            // 
            // GcodeStreamer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1093, 978);
            this.Controls.Add(this.lblSpindleOnOff);
            this.Controls.Add(this.btnSpindleOff);
            this.Controls.Add(this.btnSpindleOn);
            this.Controls.Add(this.lblSpindleRPMUnit);
            this.Controls.Add(this.lblSpindleRPM);
            this.Controls.Add(this.tbSpindleRPM);
            this.Controls.Add(this.tbarSpindleRPM);
            this.Controls.Add(this.lblSpindleOv);
            this.Controls.Add(this.lblSpindleOvP);
            this.Controls.Add(this.tbSpindleOv);
            this.Controls.Add(this.tbarSpindleOv);
            this.Controls.Add(this.pDrawing);
            this.Controls.Add(this.lblZPos);
            this.Controls.Add(this.lblYPos);
            this.Controls.Add(this.lblXPos);
            this.Controls.Add(this.tbZPos);
            this.Controls.Add(this.tbYPos);
            this.Controls.Add(this.tbXPos);
            this.Controls.Add(this.tbarJoystick);
            this.Controls.Add(this.lblFeed);
            this.Controls.Add(this.lblFeedP);
            this.Controls.Add(this.tbFeed);
            this.Controls.Add(this.tbarFeed);
            this.Controls.Add(this.pJoystick);
            this.Controls.Add(this.pbStreamBar);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.btnSendCommand);
            this.Controls.Add(this.tbCommands);
            this.Controls.Add(this.tbConsole);
            this.Controls.Add(this.tbFileName);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GcodeStreamer";
            this.Text = "Gcode Streamer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.onClose);
            ((System.ComponentModel.ISupportInitialize)(this.tbarFeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbarJoystick)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbarSpindleOv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbarSpindleRPM)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.TextBox tbFileName;
        private System.Windows.Forms.TextBox tbConsole;
        private System.Windows.Forms.TextBox tbCommands;
        private System.Windows.Forms.Button btnSendCommand;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.ProgressBar pbStreamBar;
        private System.Windows.Forms.Panel pJoystick;
        private System.Windows.Forms.TrackBar tbarFeed;
        private System.Windows.Forms.TextBox tbFeed;
        private System.Windows.Forms.Label lblFeedP;
        private System.Windows.Forms.Label lblFeed;
        private System.Windows.Forms.TrackBar tbarJoystick;
        private System.Windows.Forms.TextBox tbXPos;
        private System.Windows.Forms.TextBox tbYPos;
        private System.Windows.Forms.TextBox tbZPos;
        private System.Windows.Forms.Label lblXPos;
        private System.Windows.Forms.Label lblYPos;
        private System.Windows.Forms.Label lblZPos;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiSettings;
        private System.Windows.Forms.Panel pDrawing;
        private System.Windows.Forms.Label lblSpindleOv;
        private System.Windows.Forms.Label lblSpindleOvP;
        private System.Windows.Forms.TextBox tbSpindleOv;
        private System.Windows.Forms.TrackBar tbarSpindleOv;
        private System.Windows.Forms.Label lblSpindleRPM;
        private System.Windows.Forms.TextBox tbSpindleRPM;
        private System.Windows.Forms.TrackBar tbarSpindleRPM;
        private System.Windows.Forms.Label lblSpindleRPMUnit;
        private System.Windows.Forms.Button btnSpindleOn;
        private System.Windows.Forms.Button btnSpindleOff;
        private System.Windows.Forms.Label lblSpindleOnOff;
    }
}

