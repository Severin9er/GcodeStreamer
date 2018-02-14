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
            ((System.ComponentModel.ISupportInitialize)(this.tbarFeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbarJoystick)).BeginInit();
            this.SuspendLayout();
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.onClose);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(460, 18);
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
            this.tbFileName.Location = new System.Drawing.Point(18, 23);
            this.tbFileName.Name = "tbFileName";
            this.tbFileName.Size = new System.Drawing.Size(437, 22);
            this.tbFileName.TabIndex = 1;
            // 
            // tbConsole
            // 
            this.tbConsole.Location = new System.Drawing.Point(19, 98);
            this.tbConsole.Multiline = true;
            this.tbConsole.Name = "tbConsole";
            this.tbConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbConsole.Size = new System.Drawing.Size(502, 343);
            this.tbConsole.TabIndex = 2;
            // 
            // tbCommands
            // 
            this.tbCommands.Location = new System.Drawing.Point(19, 470);
            this.tbCommands.Name = "tbCommands";
            this.tbCommands.Size = new System.Drawing.Size(436, 22);
            this.tbCommands.TabIndex = 3;
            this.tbCommands.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbCommands_KeyPress);
            // 
            // btnSendCommand
            // 
            this.btnSendCommand.Location = new System.Drawing.Point(461, 463);
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
            this.btnStart.Location = new System.Drawing.Point(19, 55);
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
            this.btnPause.Location = new System.Drawing.Point(110, 55);
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
            this.btnStop.Location = new System.Drawing.Point(201, 55);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(85, 30);
            this.btnStop.TabIndex = 7;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // pbStreamBar
            // 
            this.pbStreamBar.Location = new System.Drawing.Point(299, 57);
            this.pbStreamBar.Name = "pbStreamBar";
            this.pbStreamBar.Size = new System.Drawing.Size(223, 27);
            this.pbStreamBar.TabIndex = 8;
            // 
            // pJoystick
            // 
            this.pJoystick.BackColor = System.Drawing.SystemColors.Control;
            this.pJoystick.Location = new System.Drawing.Point(585, 293);
            this.pJoystick.Name = "pJoystick";
            this.pJoystick.Size = new System.Drawing.Size(200, 200);
            this.pJoystick.TabIndex = 15;
            this.pJoystick.Paint += new System.Windows.Forms.PaintEventHandler(this.pJoystick_Paint);
            this.pJoystick.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pJoystick_MouseMove);
            this.pJoystick.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Joystick_MouseUp);
            // 
            // tbarFeed
            // 
            this.tbarFeed.Location = new System.Drawing.Point(580, 158);
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
            this.tbFeed.Location = new System.Drawing.Point(816, 158);
            this.tbFeed.Name = "tbFeed";
            this.tbFeed.Size = new System.Drawing.Size(61, 22);
            this.tbFeed.TabIndex = 19;
            this.tbFeed.Text = "100";
            this.tbFeed.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbFeed_KeyPress);
            // 
            // lblFeedP
            // 
            this.lblFeedP.AutoSize = true;
            this.lblFeedP.Location = new System.Drawing.Point(883, 158);
            this.lblFeedP.Name = "lblFeedP";
            this.lblFeedP.Size = new System.Drawing.Size(20, 17);
            this.lblFeedP.TabIndex = 22;
            this.lblFeedP.Text = "%";
            // 
            // lblFeed
            // 
            this.lblFeed.AutoSize = true;
            this.lblFeed.Location = new System.Drawing.Point(527, 163);
            this.lblFeed.Name = "lblFeed";
            this.lblFeed.Size = new System.Drawing.Size(44, 17);
            this.lblFeed.TabIndex = 25;
            this.lblFeed.Text = "Feed:";
            // 
            // tbarJoystick
            // 
            this.tbarJoystick.Location = new System.Drawing.Point(816, 293);
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
            this.tbXPos.Location = new System.Drawing.Point(585, 23);
            this.tbXPos.Name = "tbXPos";
            this.tbXPos.Size = new System.Drawing.Size(55, 22);
            this.tbXPos.TabIndex = 27;
            // 
            // tbYPos
            // 
            this.tbYPos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbYPos.Enabled = false;
            this.tbYPos.Location = new System.Drawing.Point(673, 23);
            this.tbYPos.Name = "tbYPos";
            this.tbYPos.Size = new System.Drawing.Size(55, 22);
            this.tbYPos.TabIndex = 28;
            // 
            // tbZPos
            // 
            this.tbZPos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbZPos.Enabled = false;
            this.tbZPos.Location = new System.Drawing.Point(761, 23);
            this.tbZPos.Name = "tbZPos";
            this.tbZPos.Size = new System.Drawing.Size(55, 22);
            this.tbZPos.TabIndex = 29;
            // 
            // lblXPos
            // 
            this.lblXPos.AutoSize = true;
            this.lblXPos.Location = new System.Drawing.Point(558, 26);
            this.lblXPos.Name = "lblXPos";
            this.lblXPos.Size = new System.Drawing.Size(21, 17);
            this.lblXPos.TabIndex = 30;
            this.lblXPos.Text = "X:";
            this.lblXPos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblYPos
            // 
            this.lblYPos.AutoSize = true;
            this.lblYPos.Location = new System.Drawing.Point(646, 26);
            this.lblYPos.Name = "lblYPos";
            this.lblYPos.Size = new System.Drawing.Size(21, 17);
            this.lblYPos.TabIndex = 31;
            this.lblYPos.Text = "Y:";
            this.lblYPos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblZPos
            // 
            this.lblZPos.AutoSize = true;
            this.lblZPos.Location = new System.Drawing.Point(734, 26);
            this.lblZPos.Name = "lblZPos";
            this.lblZPos.Size = new System.Drawing.Size(21, 17);
            this.lblZPos.TabIndex = 32;
            this.lblZPos.Text = "Z:";
            this.lblZPos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GcodeStreamer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 551);
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
            this.Name = "GcodeStreamer";
            this.Text = "Gcode Streamer";
            ((System.ComponentModel.ISupportInitialize)(this.tbarFeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbarJoystick)).EndInit();
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
    }
}

