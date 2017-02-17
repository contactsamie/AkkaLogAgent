namespace AkkaLogAgent.WinForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ErrorNotificationTxt = new System.Windows.Forms.ToolStripStatusLabel();
            this.InformationTxt = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusTxt = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.selectPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FolderPathTxt = new System.Windows.Forms.ToolStripTextBox();
            this.fileNameregexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RegexTxt = new System.Windows.Forms.ToolStripTextBox();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DisplayTxt = new System.Windows.Forms.RichTextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.indicatorPannel = new System.Windows.Forms.Panel();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.indicatorPannel.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ErrorNotificationTxt,
            this.InformationTxt,
            this.StatusTxt});
            this.statusStrip1.Location = new System.Drawing.Point(0, 436);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(712, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // ErrorNotificationTxt
            // 
            this.ErrorNotificationTxt.ForeColor = System.Drawing.Color.Red;
            this.ErrorNotificationTxt.Name = "ErrorNotificationTxt";
            this.ErrorNotificationTxt.Size = new System.Drawing.Size(0, 17);
            // 
            // InformationTxt
            // 
            this.InformationTxt.Name = "InformationTxt";
            this.InformationTxt.Size = new System.Drawing.Size(0, 17);
            // 
            // StatusTxt
            // 
            this.StatusTxt.Name = "StatusTxt";
            this.StatusTxt.Size = new System.Drawing.Size(0, 17);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectPathToolStripMenuItem,
            this.FolderPathTxt,
            this.fileNameregexToolStripMenuItem,
            this.RegexTxt,
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(712, 27);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // selectPathToolStripMenuItem
            // 
            this.selectPathToolStripMenuItem.Name = "selectPathToolStripMenuItem";
            this.selectPathToolStripMenuItem.Size = new System.Drawing.Size(77, 23);
            this.selectPathToolStripMenuItem.Text = "Select Path";
            this.selectPathToolStripMenuItem.Click += new System.EventHandler(this.selectPathToolStripMenuItem_Click);
            // 
            // FolderPathTxt
            // 
            this.FolderPathTxt.Name = "FolderPathTxt";
            this.FolderPathTxt.Size = new System.Drawing.Size(250, 23);
            // 
            // fileNameregexToolStripMenuItem
            // 
            this.fileNameregexToolStripMenuItem.Name = "fileNameregexToolStripMenuItem";
            this.fileNameregexToolStripMenuItem.Size = new System.Drawing.Size(48, 23);
            this.fileNameregexToolStripMenuItem.Text = "Filter:";
            // 
            // RegexTxt
            // 
            this.RegexTxt.Name = "RegexTxt";
            this.RegexTxt.Size = new System.Drawing.Size(200, 23);
            this.RegexTxt.Text = "InventoryService*.log";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.debugModeToolStripMenuItem,
            this.startToolStripMenuItem1});
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(43, 23);
            this.startToolStripMenuItem.Text = "Start";
            // 
            // debugModeToolStripMenuItem
            // 
            this.debugModeToolStripMenuItem.Name = "debugModeToolStripMenuItem";
            this.debugModeToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.debugModeToolStripMenuItem.Text = "Debug Mode";
            this.debugModeToolStripMenuItem.Click += new System.EventHandler(this.debugModeToolStripMenuItem_Click);
            // 
            // startToolStripMenuItem1
            // 
            this.startToolStripMenuItem1.Name = "startToolStripMenuItem1";
            this.startToolStripMenuItem1.Size = new System.Drawing.Size(203, 22);
            this.startToolStripMenuItem1.Text = "Start(will execute restart)";
            this.startToolStripMenuItem1.Click += new System.EventHandler(this.startToolStripMenuItem1_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(43, 23);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Visible = false;
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // DisplayTxt
            // 
            this.DisplayTxt.BackColor = System.Drawing.Color.Black;
            this.DisplayTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DisplayTxt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.DisplayTxt.Location = new System.Drawing.Point(0, 0);
            this.DisplayTxt.Name = "DisplayTxt";
            this.DisplayTxt.Size = new System.Drawing.Size(712, 222);
            this.DisplayTxt.TabIndex = 2;
            this.DisplayTxt.Text = "";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 27);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.DisplayTxt);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(712, 409);
            this.splitContainer1.SplitterDistance = 222;
            this.splitContainer1.TabIndex = 3;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.richTextBox1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.indicatorPannel);
            this.splitContainer2.Size = new System.Drawing.Size(712, 183);
            this.splitContainer2.SplitterDistance = 298;
            this.splitContainer2.TabIndex = 1;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.Black;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(298, 183);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // indicatorPannel
            // 
            this.indicatorPannel.Controls.Add(this.richTextBox2);
            this.indicatorPannel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.indicatorPannel.Location = new System.Drawing.Point(0, 0);
            this.indicatorPannel.Name = "indicatorPannel";
            this.indicatorPannel.Size = new System.Drawing.Size(410, 183);
            this.indicatorPannel.TabIndex = 0;
            // 
            // richTextBox2
            // 
            this.richTextBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox2.Location = new System.Drawing.Point(17, 19);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(381, 148);
            this.richTextBox2.TabIndex = 0;
            this.richTextBox2.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 458);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Akka Log Agent";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.indicatorPannel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem selectPathToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox FolderPathTxt;
        private System.Windows.Forms.ToolStripMenuItem fileNameregexToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox RegexTxt;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.RichTextBox DisplayTxt;
        private System.Windows.Forms.ToolStripStatusLabel ErrorNotificationTxt;
        private System.Windows.Forms.ToolStripStatusLabel InformationTxt;
        private System.Windows.Forms.ToolStripStatusLabel StatusTxt;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel indicatorPannel;
        private System.Windows.Forms.ToolStripMenuItem debugModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem1;
        private System.Windows.Forms.RichTextBox richTextBox2;
    }
}

