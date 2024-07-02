namespace MobleFinal
{
    partial class VideoPlayerForm
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
            videoView1 = new LibVLCSharp.WinForms.VideoView();
            listBox1 = new ListBox();
            panel1 = new Panel();
            label2 = new Label();
            label1 = new Label();
            minus = new Button();
            plus = new Button();
            Pause = new Button();
            Play = new Button();
            timer1 = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)videoView1).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // videoView1
            // 
            videoView1.BackColor = Color.Black;
            videoView1.Location = new Point(254, 0);
            videoView1.Margin = new Padding(2, 2, 2, 2);
            videoView1.MediaPlayer = null;
            videoView1.Name = "videoView1";
            videoView1.Size = new Size(979, 562);
            videoView1.TabIndex = 0;
            videoView1.Text = "videoView1";
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(0, 0);
            listBox1.Margin = new Padding(2, 2, 2, 2);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(250, 724);
            listBox1.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(minus);
            panel1.Controls.Add(plus);
            panel1.Controls.Add(Pause);
            panel1.Controls.Add(Play);
            panel1.Location = new Point(254, 562);
            panel1.Margin = new Padding(2, 2, 2, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(980, 150);
            panel1.TabIndex = 2;
            panel1.Paint += panel1_Paint;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(848, 30);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(59, 15);
            label2.TabIndex = 7;
            label2.Text = "-- : -- : --";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(74, 30);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(59, 15);
            label1.TabIndex = 3;
            label1.Text = "-- : -- : --";
            // 
            // minus
            // 
            minus.Location = new Point(541, 78);
            minus.Margin = new Padding(2, 2, 2, 2);
            minus.Name = "minus";
            minus.Size = new Size(58, 56);
            minus.TabIndex = 6;
            minus.Text = "minus";
            minus.UseVisualStyleBackColor = true;
            minus.Click += minus_Click;
            // 
            // plus
            // 
            plus.Location = new Point(478, 78);
            plus.Margin = new Padding(2, 2, 2, 2);
            plus.Name = "plus";
            plus.Size = new Size(58, 56);
            plus.TabIndex = 5;
            plus.Text = "plus";
            plus.UseVisualStyleBackColor = true;
            plus.Click += plus_Click;
            // 
            // Pause
            // 
            Pause.Location = new Point(415, 78);
            Pause.Margin = new Padding(2, 2, 2, 2);
            Pause.Name = "Pause";
            Pause.Size = new Size(58, 56);
            Pause.TabIndex = 4;
            Pause.Text = "Pause";
            Pause.UseVisualStyleBackColor = true;
            Pause.Click += Pause_Click;
            // 
            // Play
            // 
            Play.Location = new Point(352, 78);
            Play.Margin = new Padding(2, 2, 2, 2);
            Play.Name = "Play";
            Play.Size = new Size(58, 56);
            Play.TabIndex = 3;
            Play.Text = "Play";
            Play.UseVisualStyleBackColor = true;
            Play.Click += Play_Click;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // VideoPlayerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1230, 715);
            Controls.Add(panel1);
            Controls.Add(listBox1);
            Controls.Add(videoView1);
            Margin = new Padding(2, 2, 2, 2);
            Name = "VideoPlayerForm";
            Text = "VideoPlayerForm";
            Load += VideoPlayerForm_Load;
            ((System.ComponentModel.ISupportInitialize)videoView1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private LibVLCSharp.WinForms.VideoView videoView1;
        private ListBox listBox1;
        private Panel panel1;
        private System.Windows.Forms.Timer timer1;
        private Button Play;
        private Button minus;
        private Button plus;
        private Button Pause;
        private Label label2;
        private Label label1;
    }
}