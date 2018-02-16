namespace WindowsFormsApplication2
{
    partial class WFSettings
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
            this.tlpSettings = new System.Windows.Forms.TableLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblRefreshInterval = new System.Windows.Forms.Label();
            this.lblDecAccuracy = new System.Windows.Forms.Label();
            this.lblMaxJoyStep = new System.Windows.Forms.Label();
            this.cbDecSplitter = new System.Windows.Forms.ComboBox();
            this.lblMaxJoyFeed = new System.Windows.Forms.Label();
            this.lblDecSplit = new System.Windows.Forms.Label();
            this.lblBaudrate = new System.Windows.Forms.Label();
            this.lblComport = new System.Windows.Forms.Label();
            this.cbComport = new System.Windows.Forms.ComboBox();
            this.cbBaudrate = new System.Windows.Forms.ComboBox();
            this.tbMaxJoyFeed = new System.Windows.Forms.TextBox();
            this.tbMaxJoyStep = new System.Windows.Forms.TextBox();
            this.tbDecAccuracy = new System.Windows.Forms.TextBox();
            this.tbRefreshInterval = new System.Windows.Forms.TextBox();
            this.lblBaudrateUnit = new System.Windows.Forms.Label();
            this.lblFeedUnit = new System.Windows.Forms.Label();
            this.lblJoyStepUnit = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRestore = new System.Windows.Forms.Button();
            this.tlpSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpSettings
            // 
            this.tlpSettings.ColumnCount = 4;
            this.tlpSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 57.0122F));
            this.tlpSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.9878F));
            this.tlpSettings.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 88F));
            this.tlpSettings.Controls.Add(this.btnSave, 2, 9);
            this.tlpSettings.Controls.Add(this.lblRefreshInterval, 1, 7);
            this.tlpSettings.Controls.Add(this.lblDecAccuracy, 1, 6);
            this.tlpSettings.Controls.Add(this.lblMaxJoyStep, 1, 5);
            this.tlpSettings.Controls.Add(this.cbDecSplitter, 2, 3);
            this.tlpSettings.Controls.Add(this.lblMaxJoyFeed, 1, 4);
            this.tlpSettings.Controls.Add(this.lblDecSplit, 1, 3);
            this.tlpSettings.Controls.Add(this.lblBaudrate, 1, 2);
            this.tlpSettings.Controls.Add(this.lblComport, 1, 1);
            this.tlpSettings.Controls.Add(this.cbComport, 2, 1);
            this.tlpSettings.Controls.Add(this.cbBaudrate, 2, 2);
            this.tlpSettings.Controls.Add(this.tbMaxJoyFeed, 2, 4);
            this.tlpSettings.Controls.Add(this.tbMaxJoyStep, 2, 5);
            this.tlpSettings.Controls.Add(this.tbDecAccuracy, 2, 6);
            this.tlpSettings.Controls.Add(this.tbRefreshInterval, 2, 7);
            this.tlpSettings.Controls.Add(this.lblBaudrateUnit, 3, 2);
            this.tlpSettings.Controls.Add(this.lblFeedUnit, 3, 4);
            this.tlpSettings.Controls.Add(this.lblJoyStepUnit, 3, 5);
            this.tlpSettings.Controls.Add(this.label1, 3, 7);
            this.tlpSettings.Controls.Add(this.btnRestore, 1, 9);
            this.tlpSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpSettings.Location = new System.Drawing.Point(0, 0);
            this.tlpSettings.Name = "tlpSettings";
            this.tlpSettings.RowCount = 11;
            this.tlpSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlpSettings.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSettings.Size = new System.Drawing.Size(436, 519);
            this.tlpSettings.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Location = new System.Drawing.Point(210, 263);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(134, 44);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblRefreshInterval
            // 
            this.lblRefreshInterval.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRefreshInterval.AutoSize = true;
            this.lblRefreshInterval.Location = new System.Drawing.Point(42, 200);
            this.lblRefreshInterval.Name = "lblRefreshInterval";
            this.lblRefreshInterval.Size = new System.Drawing.Size(162, 30);
            this.lblRefreshInterval.TabIndex = 12;
            this.lblRefreshInterval.Text = "Position Refresh Interval";
            this.lblRefreshInterval.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDecAccuracy
            // 
            this.lblDecAccuracy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDecAccuracy.AutoSize = true;
            this.lblDecAccuracy.Location = new System.Drawing.Point(84, 170);
            this.lblDecAccuracy.Name = "lblDecAccuracy";
            this.lblDecAccuracy.Size = new System.Drawing.Size(120, 30);
            this.lblDecAccuracy.TabIndex = 10;
            this.lblDecAccuracy.Text = "Decimal Accuracy";
            this.lblDecAccuracy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMaxJoyStep
            // 
            this.lblMaxJoyStep.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMaxJoyStep.AutoSize = true;
            this.lblMaxJoyStep.Location = new System.Drawing.Point(80, 140);
            this.lblMaxJoyStep.Name = "lblMaxJoyStep";
            this.lblMaxJoyStep.Size = new System.Drawing.Size(124, 30);
            this.lblMaxJoyStep.TabIndex = 8;
            this.lblMaxJoyStep.Text = "Max. Joystick Step";
            this.lblMaxJoyStep.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbDecSplitter
            // 
            this.cbDecSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbDecSplitter.FormattingEnabled = true;
            this.cbDecSplitter.Items.AddRange(new object[] {
            ", (Comma)",
            ". (Point)"});
            this.cbDecSplitter.Location = new System.Drawing.Point(210, 83);
            this.cbDecSplitter.Name = "cbDecSplitter";
            this.cbDecSplitter.Size = new System.Drawing.Size(134, 24);
            this.cbDecSplitter.TabIndex = 6;
            this.cbDecSplitter.SelectedIndexChanged += new System.EventHandler(this.cbDecSplitter_SelectedIndexChanged);
            // 
            // lblMaxJoyFeed
            // 
            this.lblMaxJoyFeed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMaxJoyFeed.AutoSize = true;
            this.lblMaxJoyFeed.Location = new System.Drawing.Point(77, 110);
            this.lblMaxJoyFeed.Name = "lblMaxJoyFeed";
            this.lblMaxJoyFeed.Size = new System.Drawing.Size(127, 30);
            this.lblMaxJoyFeed.TabIndex = 5;
            this.lblMaxJoyFeed.Text = "Max. Joystick Feed";
            this.lblMaxJoyFeed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDecSplit
            // 
            this.lblDecSplit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDecSplit.AutoSize = true;
            this.lblDecSplit.Location = new System.Drawing.Point(98, 80);
            this.lblDecSplit.Name = "lblDecSplit";
            this.lblDecSplit.Size = new System.Drawing.Size(106, 30);
            this.lblDecSplit.TabIndex = 4;
            this.lblDecSplit.Text = "Decimal Splitter";
            this.lblDecSplit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBaudrate
            // 
            this.lblBaudrate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBaudrate.AutoSize = true;
            this.lblBaudrate.Location = new System.Drawing.Point(138, 50);
            this.lblBaudrate.Name = "lblBaudrate";
            this.lblBaudrate.Size = new System.Drawing.Size(66, 30);
            this.lblBaudrate.TabIndex = 2;
            this.lblBaudrate.Text = "Baudrate";
            this.lblBaudrate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblComport
            // 
            this.lblComport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblComport.AutoSize = true;
            this.lblComport.Location = new System.Drawing.Point(134, 20);
            this.lblComport.Name = "lblComport";
            this.lblComport.Size = new System.Drawing.Size(70, 30);
            this.lblComport.TabIndex = 0;
            this.lblComport.Text = "COM-Port";
            this.lblComport.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbComport
            // 
            this.cbComport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbComport.FormattingEnabled = true;
            this.cbComport.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4"});
            this.cbComport.Location = new System.Drawing.Point(210, 23);
            this.cbComport.Name = "cbComport";
            this.cbComport.Size = new System.Drawing.Size(134, 24);
            this.cbComport.TabIndex = 1;
            this.cbComport.SelectedIndexChanged += new System.EventHandler(this.cbComport_SelectedIndexChanged);
            // 
            // cbBaudrate
            // 
            this.cbBaudrate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbBaudrate.FormattingEnabled = true;
            this.cbBaudrate.Items.AddRange(new object[] {
            "2400",
            "4800",
            "9600",
            "14400",
            "19200",
            "38400",
            "57600",
            "115200",
            "230400",
            "250000"});
            this.cbBaudrate.Location = new System.Drawing.Point(210, 53);
            this.cbBaudrate.Name = "cbBaudrate";
            this.cbBaudrate.Size = new System.Drawing.Size(134, 24);
            this.cbBaudrate.TabIndex = 3;
            this.cbBaudrate.SelectedIndexChanged += new System.EventHandler(this.cbBaudrate_SelectedIndexChanged);
            // 
            // tbMaxJoyFeed
            // 
            this.tbMaxJoyFeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbMaxJoyFeed.Location = new System.Drawing.Point(210, 113);
            this.tbMaxJoyFeed.Name = "tbMaxJoyFeed";
            this.tbMaxJoyFeed.Size = new System.Drawing.Size(134, 22);
            this.tbMaxJoyFeed.TabIndex = 7;
            this.tbMaxJoyFeed.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbInt_KeyPress);
            this.tbMaxJoyFeed.Leave += new System.EventHandler(this.tbInt_Leave);
            // 
            // tbMaxJoyStep
            // 
            this.tbMaxJoyStep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbMaxJoyStep.Location = new System.Drawing.Point(210, 143);
            this.tbMaxJoyStep.Name = "tbMaxJoyStep";
            this.tbMaxJoyStep.Size = new System.Drawing.Size(134, 22);
            this.tbMaxJoyStep.TabIndex = 9;
            this.tbMaxJoyStep.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbMaxJoyStep_KeyPress);
            this.tbMaxJoyStep.Leave += new System.EventHandler(this.tbMaxJoyStep_Leave);
            // 
            // tbDecAccuracy
            // 
            this.tbDecAccuracy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbDecAccuracy.Location = new System.Drawing.Point(210, 173);
            this.tbDecAccuracy.Name = "tbDecAccuracy";
            this.tbDecAccuracy.Size = new System.Drawing.Size(134, 22);
            this.tbDecAccuracy.TabIndex = 11;
            this.tbDecAccuracy.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbDecAccuracy_KeyPress);
            this.tbDecAccuracy.Leave += new System.EventHandler(this.tbDecAccuracy_Leave);
            // 
            // tbRefreshInterval
            // 
            this.tbRefreshInterval.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbRefreshInterval.Location = new System.Drawing.Point(210, 203);
            this.tbRefreshInterval.Name = "tbRefreshInterval";
            this.tbRefreshInterval.Size = new System.Drawing.Size(134, 22);
            this.tbRefreshInterval.TabIndex = 13;
            this.tbRefreshInterval.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbInt_KeyPress);
            this.tbRefreshInterval.Leave += new System.EventHandler(this.tbInt_Leave);
            // 
            // lblBaudrateUnit
            // 
            this.lblBaudrateUnit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblBaudrateUnit.AutoSize = true;
            this.lblBaudrateUnit.Location = new System.Drawing.Point(350, 50);
            this.lblBaudrateUnit.Name = "lblBaudrateUnit";
            this.lblBaudrateUnit.Size = new System.Drawing.Size(41, 30);
            this.lblBaudrateUnit.TabIndex = 14;
            this.lblBaudrateUnit.Text = "Baud";
            this.lblBaudrateUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblFeedUnit
            // 
            this.lblFeedUnit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblFeedUnit.AutoSize = true;
            this.lblFeedUnit.Location = new System.Drawing.Point(350, 110);
            this.lblFeedUnit.Name = "lblFeedUnit";
            this.lblFeedUnit.Size = new System.Drawing.Size(56, 30);
            this.lblFeedUnit.TabIndex = 15;
            this.lblFeedUnit.Text = "mm/min";
            this.lblFeedUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblJoyStepUnit
            // 
            this.lblJoyStepUnit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lblJoyStepUnit.AutoSize = true;
            this.lblJoyStepUnit.Location = new System.Drawing.Point(350, 140);
            this.lblJoyStepUnit.Name = "lblJoyStepUnit";
            this.lblJoyStepUnit.Size = new System.Drawing.Size(30, 30);
            this.lblJoyStepUnit.TabIndex = 16;
            this.lblJoyStepUnit.Text = "mm";
            this.lblJoyStepUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(350, 200);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 30);
            this.label1.TabIndex = 17;
            this.label1.Text = "ms";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnRestore
            // 
            this.btnRestore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRestore.Location = new System.Drawing.Point(23, 263);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(181, 44);
            this.btnRestore.TabIndex = 18;
            this.btnRestore.Text = "Restore Default";
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // WFSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 519);
            this.Controls.Add(this.tlpSettings);
            this.Name = "WFSettings";
            this.Text = "Settings";
            this.tlpSettings.ResumeLayout(false);
            this.tlpSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpSettings;
        private System.Windows.Forms.Label lblComport;
        private System.Windows.Forms.ComboBox cbComport;
        private System.Windows.Forms.Label lblBaudrate;
        private System.Windows.Forms.ComboBox cbBaudrate;
        private System.Windows.Forms.ComboBox cbDecSplitter;
        private System.Windows.Forms.Label lblMaxJoyFeed;
        private System.Windows.Forms.Label lblDecSplit;
        private System.Windows.Forms.Label lblMaxJoyStep;
        private System.Windows.Forms.TextBox tbMaxJoyFeed;
        private System.Windows.Forms.TextBox tbMaxJoyStep;
        private System.Windows.Forms.Label lblDecAccuracy;
        private System.Windows.Forms.TextBox tbDecAccuracy;
        private System.Windows.Forms.Label lblRefreshInterval;
        private System.Windows.Forms.TextBox tbRefreshInterval;
        private System.Windows.Forms.Label lblBaudrateUnit;
        private System.Windows.Forms.Label lblFeedUnit;
        private System.Windows.Forms.Label lblJoyStepUnit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnRestore;
    }
}