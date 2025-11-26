
namespace YouTubeDownloader
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            label1 = new Label();
            textBox1 = new TextBox();
            rtbConsole = new RichTextBox();
            btnStart = new Button();
            rbWMV = new RadioButton();
            rbMKV = new RadioButton();
            rbWebM = new RadioButton();
            rbMp4 = new RadioButton();
            rbOpus = new RadioButton();
            rbWAV = new RadioButton();
            rbOggVorbis = new RadioButton();
            rbMP3 = new RadioButton();
            txtPath = new TextBox();
            label2 = new Label();
            btnBrowse = new Button();
            groupBox4 = new GroupBox();
            label4 = new Label();
            label3 = new Label();
            button1 = new Button();
            button2 = new Button();
            panel1 = new Panel();
            menuStrip1 = new MenuStrip();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            aboutRZsVideoDownloaderToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            toolsToolStripMenuItem = new ToolStripMenuItem();
            verifyDependenciesToolStripMenuItem = new ToolStripMenuItem();
            groupBox4.SuspendLayout();
            panel1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label1.AutoSize = true;
            label1.Location = new Point(12, 41);
            label1.Name = "label1";
            label1.Size = new Size(94, 15);
            label1.TabIndex = 0;
            label1.Text = "Enter Video URL:";
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            textBox1.Location = new Point(12, 59);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(385, 23);
            textBox1.TabIndex = 1;
            // 
            // rtbConsole
            // 
            rtbConsole.BackColor = SystemColors.ActiveCaptionText;
            rtbConsole.Dock = DockStyle.Right;
            rtbConsole.Font = new Font("Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rtbConsole.ForeColor = Color.White;
            rtbConsole.Location = new Point(403, 0);
            rtbConsole.Name = "rtbConsole";
            rtbConsole.ReadOnly = true;
            rtbConsole.Size = new Size(393, 308);
            rtbConsole.TabIndex = 0;
            rtbConsole.Text = "";
            rtbConsole.TextChanged += rtbConsole_TextChanged;
            // 
            // btnStart
            // 
            btnStart.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnStart.Location = new Point(309, 144);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(88, 72);
            btnStart.TabIndex = 3;
            btnStart.Text = "Start!";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += button1_Click;
            // 
            // rbWMV
            // 
            rbWMV.AutoSize = true;
            rbWMV.Location = new Point(194, 51);
            rbWMV.Name = "rbWMV";
            rbWMV.Size = new Size(54, 19);
            rbWMV.TabIndex = 3;
            rbWMV.TabStop = true;
            rbWMV.Text = "WMV";
            rbWMV.UseVisualStyleBackColor = true;
            // 
            // rbMKV
            // 
            rbMKV.AutoSize = true;
            rbMKV.Location = new Point(72, 51);
            rbMKV.Name = "rbMKV";
            rbMKV.Size = new Size(50, 19);
            rbMKV.TabIndex = 2;
            rbMKV.TabStop = true;
            rbMKV.Text = "MKV";
            rbMKV.UseVisualStyleBackColor = true;
            // 
            // rbWebM
            // 
            rbWebM.AutoSize = true;
            rbWebM.Location = new Point(128, 51);
            rbWebM.Name = "rbWebM";
            rbWebM.Size = new Size(60, 19);
            rbWebM.TabIndex = 1;
            rbWebM.TabStop = true;
            rbWebM.Text = "WebM";
            rbWebM.UseVisualStyleBackColor = true;
            // 
            // rbMp4
            // 
            rbMp4.AutoSize = true;
            rbMp4.Location = new Point(17, 51);
            rbMp4.Name = "rbMp4";
            rbMp4.Size = new Size(49, 19);
            rbMp4.TabIndex = 0;
            rbMp4.TabStop = true;
            rbMp4.Text = "MP4";
            rbMp4.UseVisualStyleBackColor = true;
            // 
            // rbOpus
            // 
            rbOpus.AutoSize = true;
            rbOpus.Location = new Point(214, 101);
            rbOpus.Name = "rbOpus";
            rbOpus.Size = new Size(53, 19);
            rbOpus.TabIndex = 3;
            rbOpus.TabStop = true;
            rbOpus.Text = "Opus";
            rbOpus.UseVisualStyleBackColor = true;
            // 
            // rbWAV
            // 
            rbWAV.AutoSize = true;
            rbWAV.Location = new Point(72, 101);
            rbWAV.Name = "rbWAV";
            rbWAV.Size = new Size(50, 19);
            rbWAV.TabIndex = 2;
            rbWAV.TabStop = true;
            rbWAV.Text = "WAV";
            rbWAV.UseVisualStyleBackColor = true;
            // 
            // rbOggVorbis
            // 
            rbOggVorbis.AutoSize = true;
            rbOggVorbis.Location = new Point(128, 101);
            rbOggVorbis.Name = "rbOggVorbis";
            rbOggVorbis.Size = new Size(80, 19);
            rbOggVorbis.TabIndex = 1;
            rbOggVorbis.TabStop = true;
            rbOggVorbis.Text = "OggVorbis";
            rbOggVorbis.UseVisualStyleBackColor = true;
            // 
            // rbMP3
            // 
            rbMP3.AutoSize = true;
            rbMP3.Location = new Point(17, 101);
            rbMP3.Name = "rbMP3";
            rbMP3.Size = new Size(49, 19);
            rbMP3.TabIndex = 0;
            rbMP3.TabStop = true;
            rbMP3.Text = "MP3";
            rbMP3.UseVisualStyleBackColor = true;
            // 
            // txtPath
            // 
            txtPath.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtPath.Location = new Point(12, 108);
            txtPath.Name = "txtPath";
            txtPath.ReadOnly = true;
            txtPath.Size = new Size(291, 23);
            txtPath.TabIndex = 7;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            label2.AutoSize = true;
            label2.Location = new Point(12, 90);
            label2.Name = "label2";
            label2.Size = new Size(113, 15);
            label2.TabIndex = 6;
            label2.Text = "Download Location:\r\n";
            // 
            // btnBrowse
            // 
            btnBrowse.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnBrowse.Location = new Point(309, 108);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(88, 23);
            btnBrowse.TabIndex = 8;
            btnBrowse.Text = "Browse";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // groupBox4
            // 
            groupBox4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            groupBox4.Controls.Add(rbOpus);
            groupBox4.Controls.Add(label4);
            groupBox4.Controls.Add(rbOggVorbis);
            groupBox4.Controls.Add(rbWAV);
            groupBox4.Controls.Add(rbWMV);
            groupBox4.Controls.Add(label3);
            groupBox4.Controls.Add(rbMP3);
            groupBox4.Controls.Add(rbWebM);
            groupBox4.Controls.Add(rbMKV);
            groupBox4.Controls.Add(rbMp4);
            groupBox4.Location = new Point(12, 144);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(291, 152);
            groupBox4.TabIndex = 9;
            groupBox4.TabStop = false;
            groupBox4.Text = "Formats";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(17, 83);
            label4.Name = "label4";
            label4.Size = new Size(88, 15);
            label4.TabIndex = 4;
            label4.Text = "Audio Formats:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(17, 33);
            label3.Name = "label3";
            label3.Size = new Size(86, 15);
            label3.TabIndex = 0;
            label3.Text = "Video Formats:";
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button1.Location = new Point(309, 273);
            button1.Name = "button1";
            button1.Size = new Size(88, 23);
            button1.TabIndex = 10;
            button1.Text = "Clear Console";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button2.Location = new Point(309, 222);
            button2.Name = "button2";
            button2.Size = new Size(88, 23);
            button2.TabIndex = 11;
            button2.Text = "Help";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(menuStrip1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(403, 27);
            panel1.TabIndex = 12;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { aboutToolStripMenuItem, toolsToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(403, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aboutRZsVideoDownloaderToolStripMenuItem, toolStripSeparator1, exitToolStripMenuItem });
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(52, 20);
            aboutToolStripMenuItem.Text = "About";
            // 
            // aboutRZsVideoDownloaderToolStripMenuItem
            // 
            aboutRZsVideoDownloaderToolStripMenuItem.Name = "aboutRZsVideoDownloaderToolStripMenuItem";
            aboutRZsVideoDownloaderToolStripMenuItem.Size = new Size(232, 22);
            aboutRZsVideoDownloaderToolStripMenuItem.Text = "About RZ's Video Downloader";
            aboutRZsVideoDownloaderToolStripMenuItem.Click += aboutRZsVideoDownloaderToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(229, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(232, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // toolsToolStripMenuItem
            // 
            toolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { verifyDependenciesToolStripMenuItem });
            toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            toolsToolStripMenuItem.Size = new Size(47, 20);
            toolsToolStripMenuItem.Text = "Tools";
            // 
            // verifyDependenciesToolStripMenuItem
            // 
            verifyDependenciesToolStripMenuItem.Name = "verifyDependenciesToolStripMenuItem";
            verifyDependenciesToolStripMenuItem.Size = new Size(180, 22);
            verifyDependenciesToolStripMenuItem.Text = "Verify Dependencies";
            verifyDependenciesToolStripMenuItem.Click += verifyDependenciesToolStripMenuItem_Click;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(796, 308);
            Controls.Add(panel1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(rtbConsole);
            Controls.Add(groupBox4);
            Controls.Add(btnBrowse);
            Controls.Add(txtPath);
            Controls.Add(label2);
            Controls.Add(btnStart);
            Controls.Add(textBox1);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            MaximizeBox = false;
            Name = "frmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "RZ's Video Downloader";
            Load += frmMain_Load;
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBox1;
        private Button btnStart;
        private RichTextBox rtbConsole;
        private RadioButton rbWMV;
        private RadioButton rbMKV;
        private RadioButton rbWebM;
        private RadioButton rbMp4;
        private RadioButton rbOpus;
        private RadioButton rbWAV;
        private RadioButton rbOggVorbis;
        private RadioButton rbMP3;
        private TextBox txtPath;
        private Label label2;
        private Button btnBrowse;
        private GroupBox groupBox4;
        private Label label4;
        private Label label3;
        private Button button1;
        private Button button2;
        private Panel panel1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem aboutRZsVideoDownloaderToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem verifyDependenciesToolStripMenuItem;
    }
}
