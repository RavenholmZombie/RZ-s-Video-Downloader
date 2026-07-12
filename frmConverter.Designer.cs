namespace RZVD
{
    partial class frmConverter
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
            gBoxInput = new GroupBox();
            label3 = new Label();
            lblInputStatus = new Label();
            btnBrowseInput = new Button();
            txtInput = new TextBox();
            gBoxConvert = new GroupBox();
            groupBox1 = new GroupBox();
            rtbLog = new RichTextBox();
            tabFormats = new TabControl();
            tbPageAudio = new TabPage();
            rbOpus = new RadioButton();
            rbOggVorbis = new RadioButton();
            rbWav = new RadioButton();
            rbMp3 = new RadioButton();
            tbPageVideo = new TabPage();
            rbWmv = new RadioButton();
            rbWebM = new RadioButton();
            rbMkv = new RadioButton();
            rbMp4 = new RadioButton();
            label2 = new Label();
            label1 = new Label();
            btnBrowseDestination = new Button();
            txtDestination = new TextBox();
            btnConvert = new Button();
            gBoxInput.SuspendLayout();
            gBoxConvert.SuspendLayout();
            groupBox1.SuspendLayout();
            tabFormats.SuspendLayout();
            tbPageAudio.SuspendLayout();
            tbPageVideo.SuspendLayout();
            SuspendLayout();
            // 
            // gBoxInput
            // 
            gBoxInput.Controls.Add(label3);
            gBoxInput.Controls.Add(lblInputStatus);
            gBoxInput.Controls.Add(btnBrowseInput);
            gBoxInput.Controls.Add(txtInput);
            gBoxInput.Location = new Point(12, 12);
            gBoxInput.Name = "gBoxInput";
            gBoxInput.Size = new Size(426, 145);
            gBoxInput.TabIndex = 0;
            gBoxInput.TabStop = false;
            gBoxInput.Text = "Input File";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label3.Location = new Point(18, 26);
            label3.Name = "label3";
            label3.Size = new Size(157, 15);
            label3.TabIndex = 5;
            label3.Text = "Choose media file to convert:";
            // 
            // lblInputStatus
            // 
            lblInputStatus.AutoSize = true;
            lblInputStatus.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            lblInputStatus.Location = new Point(18, 88);
            lblInputStatus.Name = "lblInputStatus";
            lblInputStatus.Size = new Size(58, 15);
            lblInputStatus.TabIndex = 2;
            lblInputStatus.Text = "Waiting...";
            // 
            // btnBrowseInput
            // 
            btnBrowseInput.Location = new Point(333, 44);
            btnBrowseInput.Name = "btnBrowseInput";
            btnBrowseInput.Size = new Size(75, 23);
            btnBrowseInput.TabIndex = 1;
            btnBrowseInput.Text = "Browse";
            btnBrowseInput.UseVisualStyleBackColor = true;
            btnBrowseInput.Click += btnBrowseInput_Click;
            // 
            // txtInput
            // 
            txtInput.Location = new Point(18, 44);
            txtInput.Name = "txtInput";
            txtInput.ReadOnly = true;
            txtInput.Size = new Size(309, 23);
            txtInput.TabIndex = 0;
            // 
            // gBoxConvert
            // 
            gBoxConvert.Controls.Add(groupBox1);
            gBoxConvert.Controls.Add(tabFormats);
            gBoxConvert.Controls.Add(label2);
            gBoxConvert.Controls.Add(label1);
            gBoxConvert.Controls.Add(btnBrowseDestination);
            gBoxConvert.Controls.Add(txtDestination);
            gBoxConvert.Location = new Point(12, 163);
            gBoxConvert.Name = "gBoxConvert";
            gBoxConvert.Size = new Size(426, 264);
            gBoxConvert.TabIndex = 1;
            gBoxConvert.TabStop = false;
            gBoxConvert.Text = "Conversion";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(rtbLog);
            groupBox1.Location = new Point(216, 78);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(200, 154);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "Log";
            // 
            // rtbLog
            // 
            rtbLog.BackColor = Color.Black;
            rtbLog.BorderStyle = BorderStyle.None;
            rtbLog.Dock = DockStyle.Fill;
            rtbLog.ForeColor = Color.White;
            rtbLog.Location = new Point(3, 19);
            rtbLog.Name = "rtbLog";
            rtbLog.ReadOnly = true;
            rtbLog.Size = new Size(194, 132);
            rtbLog.TabIndex = 0;
            rtbLog.Text = "Waiting...";
            // 
            // tabFormats
            // 
            tabFormats.Controls.Add(tbPageAudio);
            tabFormats.Controls.Add(tbPageVideo);
            tabFormats.Location = new Point(18, 96);
            tabFormats.Name = "tabFormats";
            tabFormats.SelectedIndex = 0;
            tabFormats.Size = new Size(196, 140);
            tabFormats.TabIndex = 7;
            // 
            // tbPageAudio
            // 
            tbPageAudio.Controls.Add(rbOpus);
            tbPageAudio.Controls.Add(rbOggVorbis);
            tbPageAudio.Controls.Add(rbWav);
            tbPageAudio.Controls.Add(rbMp3);
            tbPageAudio.Location = new Point(4, 24);
            tbPageAudio.Name = "tbPageAudio";
            tbPageAudio.Padding = new Padding(3);
            tbPageAudio.Size = new Size(188, 112);
            tbPageAudio.TabIndex = 0;
            tbPageAudio.Text = "Audio";
            tbPageAudio.UseVisualStyleBackColor = true;
            // 
            // rbOpus
            // 
            rbOpus.AutoSize = true;
            rbOpus.Location = new Point(5, 31);
            rbOpus.Name = "rbOpus";
            rbOpus.Size = new Size(53, 19);
            rbOpus.TabIndex = 7;
            rbOpus.TabStop = true;
            rbOpus.Text = "Opus";
            rbOpus.UseVisualStyleBackColor = true;
            rbOpus.CheckedChanged += OutputFormat_CheckedChanged;
            // 
            // rbOggVorbis
            // 
            rbOggVorbis.AutoSize = true;
            rbOggVorbis.Location = new Point(60, 31);
            rbOggVorbis.Name = "rbOggVorbis";
            rbOggVorbis.Size = new Size(80, 19);
            rbOggVorbis.TabIndex = 5;
            rbOggVorbis.TabStop = true;
            rbOggVorbis.Text = "OggVorbis";
            rbOggVorbis.UseVisualStyleBackColor = true;
            rbOggVorbis.CheckedChanged += OutputFormat_CheckedChanged;
            // 
            // rbWav
            // 
            rbWav.AutoSize = true;
            rbWav.Location = new Point(60, 6);
            rbWav.Name = "rbWav";
            rbWav.Size = new Size(50, 19);
            rbWav.TabIndex = 6;
            rbWav.TabStop = true;
            rbWav.Text = "WAV";
            rbWav.UseVisualStyleBackColor = true;
            rbWav.CheckedChanged += OutputFormat_CheckedChanged;
            // 
            // rbMp3
            // 
            rbMp3.AutoSize = true;
            rbMp3.Location = new Point(5, 6);
            rbMp3.Name = "rbMp3";
            rbMp3.Size = new Size(49, 19);
            rbMp3.TabIndex = 4;
            rbMp3.TabStop = true;
            rbMp3.Text = "MP3";
            rbMp3.UseVisualStyleBackColor = true;
            rbMp3.CheckedChanged += OutputFormat_CheckedChanged;
            // 
            // tbPageVideo
            // 
            tbPageVideo.Controls.Add(rbWmv);
            tbPageVideo.Controls.Add(rbWebM);
            tbPageVideo.Controls.Add(rbMkv);
            tbPageVideo.Controls.Add(rbMp4);
            tbPageVideo.Location = new Point(4, 24);
            tbPageVideo.Name = "tbPageVideo";
            tbPageVideo.Padding = new Padding(3);
            tbPageVideo.Size = new Size(188, 112);
            tbPageVideo.TabIndex = 1;
            tbPageVideo.Text = "Video";
            tbPageVideo.UseVisualStyleBackColor = true;
            // 
            // rbWmv
            // 
            rbWmv.AutoSize = true;
            rbWmv.Location = new Point(75, 31);
            rbWmv.Name = "rbWmv";
            rbWmv.Size = new Size(54, 19);
            rbWmv.TabIndex = 7;
            rbWmv.TabStop = true;
            rbWmv.Text = "WMV";
            rbWmv.UseVisualStyleBackColor = true;
            rbWmv.CheckedChanged += OutputFormat_CheckedChanged;
            // 
            // rbWebM
            // 
            rbWebM.AutoSize = true;
            rbWebM.Location = new Point(6, 31);
            rbWebM.Name = "rbWebM";
            rbWebM.Size = new Size(60, 19);
            rbWebM.TabIndex = 5;
            rbWebM.TabStop = true;
            rbWebM.Text = "WebM";
            rbWebM.UseVisualStyleBackColor = true;
            rbWebM.CheckedChanged += OutputFormat_CheckedChanged;
            // 
            // rbMkv
            // 
            rbMkv.AutoSize = true;
            rbMkv.Location = new Point(75, 6);
            rbMkv.Name = "rbMkv";
            rbMkv.Size = new Size(50, 19);
            rbMkv.TabIndex = 6;
            rbMkv.TabStop = true;
            rbMkv.Text = "MKV";
            rbMkv.UseVisualStyleBackColor = true;
            rbMkv.CheckedChanged += OutputFormat_CheckedChanged;
            // 
            // rbMp4
            // 
            rbMp4.AutoSize = true;
            rbMp4.Location = new Point(6, 6);
            rbMp4.Name = "rbMp4";
            rbMp4.Size = new Size(49, 19);
            rbMp4.TabIndex = 4;
            rbMp4.TabStop = true;
            rbMp4.Text = "MP4";
            rbMp4.UseVisualStyleBackColor = true;
            rbMp4.CheckedChanged += OutputFormat_CheckedChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label2.Location = new Point(18, 78);
            label2.Name = "label2";
            label2.Size = new Size(129, 15);
            label2.TabIndex = 6;
            label2.Text = "Choose Output Format:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.Location = new Point(18, 24);
            label1.Name = "label1";
            label1.Size = new Size(71, 15);
            label1.TabIndex = 4;
            label1.Text = "Destination:";
            // 
            // btnBrowseDestination
            // 
            btnBrowseDestination.Location = new Point(333, 42);
            btnBrowseDestination.Name = "btnBrowseDestination";
            btnBrowseDestination.Size = new Size(75, 23);
            btnBrowseDestination.TabIndex = 3;
            btnBrowseDestination.Text = "Browse";
            btnBrowseDestination.UseVisualStyleBackColor = true;
            btnBrowseDestination.Click += btnBrowseDestination_Click;
            // 
            // txtDestination
            // 
            txtDestination.Location = new Point(18, 42);
            txtDestination.Name = "txtDestination";
            txtDestination.ReadOnly = true;
            txtDestination.Size = new Size(309, 23);
            txtDestination.TabIndex = 2;
            // 
            // btnConvert
            // 
            btnConvert.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnConvert.ForeColor = Color.Green;
            btnConvert.Location = new Point(12, 433);
            btnConvert.Name = "btnConvert";
            btnConvert.Size = new Size(426, 29);
            btnConvert.TabIndex = 2;
            btnConvert.Text = "CONVERT!";
            btnConvert.UseVisualStyleBackColor = true;
            btnConvert.Click += btnConvert_Click;
            // 
            // frmConverter
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(450, 471);
            Controls.Add(btnConvert);
            Controls.Add(gBoxConvert);
            Controls.Add(gBoxInput);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "frmConverter";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Media Converter";
            Load += frmConverter_Load;
            gBoxInput.ResumeLayout(false);
            gBoxInput.PerformLayout();
            gBoxConvert.ResumeLayout(false);
            gBoxConvert.PerformLayout();
            groupBox1.ResumeLayout(false);
            tabFormats.ResumeLayout(false);
            tbPageAudio.ResumeLayout(false);
            tbPageAudio.PerformLayout();
            tbPageVideo.ResumeLayout(false);
            tbPageVideo.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox gBoxInput;
        private Button btnBrowseInput;
        private TextBox txtInput;
        private Label lblInputStatus;
        private GroupBox gBoxConvert;
        private Label label1;
        private Button btnBrowseDestination;
        private TextBox txtDestination;
        private Label label2;
        private TabControl tabFormats;
        private TabPage tbPageAudio;
        private TabPage tbPageVideo;
        private GroupBox groupBox1;
        private RichTextBox rtbLog;
        private Button btnConvert;
        private Label label3;
        private RadioButton rbWmv;
        private RadioButton rbWebM;
        private RadioButton rbMkv;
        private RadioButton rbMp4;
        private RadioButton rbOpus;
        private RadioButton rbOggVorbis;
        private RadioButton rbWav;
        private RadioButton rbMp3;
    }
}