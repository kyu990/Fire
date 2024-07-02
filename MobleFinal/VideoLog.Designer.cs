namespace MobleFinal
{
    partial class VideoLog
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
            components = new System.ComponentModel.Container();
            panel1 = new Panel();
            panel2 = new Panel();
            label2 = new Label();
            label1 = new Label();
            Plus = new Button();
            PlayPause = new Button();
            Minus = new Button();
            videoView1 = new LibVLCSharp.WinForms.VideoView();
            videoList = new ListView();
            FileNumber = new ColumnHeader();
            FIleName = new ColumnHeader();
            Directory = new ColumnHeader();
            timer1 = new System.Windows.Forms.Timer(components);
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)videoView1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(62, 80, 100);
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(videoView1);
            panel1.Location = new Point(248, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1124, 817);
            panel1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(Plus);
            panel2.Controls.Add(PlayPause);
            panel2.Controls.Add(Minus);
            panel2.Location = new Point(12, 709);
            panel2.Name = "panel2";
            panel2.Size = new Size(1109, 107);
            panel2.TabIndex = 1;
            panel2.Paint += panel2_Paint;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(1018, 45);
            label2.Name = "label2";
            label2.Size = new Size(59, 15);
            label2.TabIndex = 4;
            label2.Text = "-- : -- : --";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(62, 45);
            label1.Name = "label1";
            label1.Size = new Size(59, 15);
            label1.TabIndex = 3;
            label1.Text = "-- : -- : --";
            // 
            // Plus
            // 
            Plus.Location = new Point(583, 56);
            Plus.Name = "Plus";
            Plus.Size = new Size(75, 23);
            Plus.TabIndex = 2;
            Plus.Text = "Plus";
            Plus.UseVisualStyleBackColor = true;
            Plus.Click += Plus_Click;
            // 
            // PlayPause
            // 
            PlayPause.Location = new Point(486, 56);
            PlayPause.Name = "PlayPause";
            PlayPause.Size = new Size(75, 23);
            PlayPause.TabIndex = 1;
            PlayPause.Text = "PlayPause";
            PlayPause.UseVisualStyleBackColor = true;
            PlayPause.Click += PlayPause_Click;
            // 
            // Minus
            // 
            Minus.Location = new Point(391, 56);
            Minus.Name = "Minus";
            Minus.Size = new Size(75, 23);
            Minus.TabIndex = 0;
            Minus.Text = "Minus";
            Minus.UseVisualStyleBackColor = true;
            Minus.Click += Minus_Click;
            // 
            // videoView1
            // 
            videoView1.BackColor = Color.Black;
            videoView1.Location = new Point(12, 3);
            videoView1.MediaPlayer = null;
            videoView1.Name = "videoView1";
            videoView1.Size = new Size(1109, 700);
            videoView1.TabIndex = 0;
            videoView1.Text = "videoView1";
            // 
            // videoList
            // 
            videoList.Columns.AddRange(new ColumnHeader[] { FileNumber, FIleName, Directory });
            videoList.FullRowSelect = true;
            videoList.Location = new Point(1, 102);
            videoList.Name = "videoList";
            videoList.Size = new Size(247, 714);
            videoList.TabIndex = 1;
            videoList.UseCompatibleStateImageBehavior = false;
            videoList.View = View.Details;
            videoList.SelectedIndexChanged += videoList_SelectedIndexChanged;
            // 
            // FileNumber
            // 
            FileNumber.Text = "번호";
            FileNumber.Width = 90;
            // 
            // FIleName
            // 
            FIleName.Text = "이름";
            FIleName.Width = 80;
            // 
            // Directory
            // 
            Directory.Text = "경로";
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // VideoLog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(43, 56, 74);
            ClientSize = new Size(1369, 817);
            Controls.Add(videoList);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "VideoLog";
            Text = "VideoLog";
            Load += VideoLog_Load;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)videoView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private ListView videoList;
        private ColumnHeader FileNumber;
        private ColumnHeader FIleName;
        private ColumnHeader Directory;
        private LibVLCSharp.WinForms.VideoView videoView1;
        private Panel panel2;
        private Button Plus;
        private Button PlayPause;
        private Button Minus;
        private System.Windows.Forms.Timer timer1;
        private Label label2;
        private Label label1;
    }
}