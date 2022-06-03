namespace LABAuto
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.outPutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configuartionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serialPortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tCPIPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serverPortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.outPutToolStripMenuItem,
            this.configuartionToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1469, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // loginToolStripMenuItem
            // 
            this.loginToolStripMenuItem.Name = "loginToolStripMenuItem";
            this.loginToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.loginToolStripMenuItem.Text = "Login";
            this.loginToolStripMenuItem.Click += new System.EventHandler(this.loginToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // outPutToolStripMenuItem
            // 
            this.outPutToolStripMenuItem.Name = "outPutToolStripMenuItem";
            this.outPutToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.outPutToolStripMenuItem.Text = "OutPut";
            this.outPutToolStripMenuItem.Click += new System.EventHandler(this.outPutToolStripMenuItem_Click);
            // 
            // configuartionToolStripMenuItem
            // 
            this.configuartionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serialPortToolStripMenuItem,
            this.tCPIPToolStripMenuItem,
            this.serverPortToolStripMenuItem});
            this.configuartionToolStripMenuItem.Name = "configuartionToolStripMenuItem";
            this.configuartionToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.configuartionToolStripMenuItem.Text = "Configuration";
            // 
            // serialPortToolStripMenuItem
            // 
            this.serialPortToolStripMenuItem.Name = "serialPortToolStripMenuItem";
            this.serialPortToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.serialPortToolStripMenuItem.Text = "SerialPort";
            this.serialPortToolStripMenuItem.Click += new System.EventHandler(this.serialPortToolStripMenuItem_Click);
            // 
            // tCPIPToolStripMenuItem
            // 
            this.tCPIPToolStripMenuItem.Name = "tCPIPToolStripMenuItem";
            this.tCPIPToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.tCPIPToolStripMenuItem.Text = "TCPIP";
            this.tCPIPToolStripMenuItem.Click += new System.EventHandler(this.tCPIPToolStripMenuItem_Click);
            // 
            // serverPortToolStripMenuItem
            // 
            this.serverPortToolStripMenuItem.Name = "serverPortToolStripMenuItem";
            this.serverPortToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.serverPortToolStripMenuItem.Text = "ServerPort";
            this.serverPortToolStripMenuItem.Click += new System.EventHandler(this.serverPortToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1469, 652);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Ezovion MI";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem outPutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configuartionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serialPortToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tCPIPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serverPortToolStripMenuItem;
    }
}

