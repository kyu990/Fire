namespace MobleFinal
{
    partial class FireDetectForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FireDetectForm));
            panel1 = new Panel();
            label7 = new Label();
            groupBox3 = new GroupBox();
            bt_start = new Button();
            bt_stop = new Button();
            btnCalibration = new Button();
            PumpOnButton = new Button();
            btnSnapshot = new Button();
            groupBox2 = new GroupBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            BatteryData = new Label();
            HumidityData = new Label();
            TempData = new Label();
            CdsData = new Label();
            GasData = new Label();
            FireData = new Label();
            label6 = new Label();
            label1 = new Label();
            label2 = new Label();
            label5 = new Label();
            label3 = new Label();
            label4 = new Label();
            label8 = new Label();
            PumpLabel = new Label();
            groupBox1 = new GroupBox();
            VideoPlayer = new PictureBox();
            panel1.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)VideoPlayer).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(62, 80, 100);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(groupBox3);
            panel1.Controls.Add(groupBox2);
            panel1.Controls.Add(groupBox1);
            panel1.Location = new Point(-1, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1375, 817);
            panel1.TabIndex = 0;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Microsoft Sans Serif", 35.9999962F, FontStyle.Bold, GraphicsUnit.Point);
            label7.ForeColor = Color.White;
            label7.Location = new Point(28, 34);
            label7.Margin = new Padding(2, 0, 2, 0);
            label7.Name = "label7";
            label7.Size = new Size(325, 55);
            label7.TabIndex = 17;
            label7.Text = "실시간 화재 감지";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(bt_start);
            groupBox3.Controls.Add(bt_stop);
            groupBox3.Controls.Add(btnCalibration);
            groupBox3.Controls.Add(PumpOnButton);
            groupBox3.Controls.Add(btnSnapshot);
            groupBox3.ForeColor = Color.White;
            groupBox3.Location = new Point(1150, 619);
            groupBox3.Margin = new Padding(2, 3, 2, 3);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(2, 3, 2, 3);
            groupBox3.Size = new Size(193, 149);
            groupBox3.TabIndex = 16;
            groupBox3.TabStop = false;
            groupBox3.Text = "groupBox3";
            // 
            // bt_start
            // 
            bt_start.BackgroundImageLayout = ImageLayout.None;
            bt_start.Image = (Image)resources.GetObject("bt_start.Image");
            bt_start.Location = new Point(109, 101);
            bt_start.Name = "bt_start";
            bt_start.Size = new Size(31, 31);
            bt_start.TabIndex = 1;
            bt_start.UseVisualStyleBackColor = true;
            bt_start.Click += bt_start_Click;
            // 
            // bt_stop
            // 
            bt_stop.Image = (Image)resources.GetObject("bt_stop.Image");
            bt_stop.Location = new Point(146, 101);
            bt_stop.Name = "bt_stop";
            bt_stop.Size = new Size(31, 31);
            bt_stop.TabIndex = 1;
            bt_stop.UseVisualStyleBackColor = true;
            bt_stop.Click += bt_stop_Click;
            // 
            // btnCalibration
            // 
            btnCalibration.ForeColor = Color.Black;
            btnCalibration.Location = new Point(16, 86);
            btnCalibration.Name = "btnCalibration";
            btnCalibration.Size = new Size(78, 46);
            btnCalibration.TabIndex = 3;
            btnCalibration.Text = "Calibration";
            btnCalibration.UseVisualStyleBackColor = true;
            btnCalibration.Click += btnCalibration_Click;
            // 
            // PumpOnButton
            // 
            PumpOnButton.BackColor = Color.Crimson;
            PumpOnButton.ForeColor = SystemColors.ControlText;
            PumpOnButton.Location = new Point(16, 24);
            PumpOnButton.Name = "PumpOnButton";
            PumpOnButton.Size = new Size(78, 46);
            PumpOnButton.TabIndex = 2;
            PumpOnButton.Text = "Pump \r\nON/OFF";
            PumpOnButton.UseVisualStyleBackColor = false;
            PumpOnButton.Click += PumpOnButton_Click;
            // 
            // btnSnapshot
            // 
            btnSnapshot.BackgroundImageLayout = ImageLayout.None;
            btnSnapshot.ForeColor = Color.Black;
            btnSnapshot.Location = new Point(100, 24);
            btnSnapshot.Name = "btnSnapshot";
            btnSnapshot.Size = new Size(78, 46);
            btnSnapshot.TabIndex = 2;
            btnSnapshot.Text = "GRAB";
            btnSnapshot.UseVisualStyleBackColor = true;
            btnSnapshot.Click += btnSnapshot_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(tableLayoutPanel1);
            groupBox2.ForeColor = Color.White;
            groupBox2.Location = new Point(1150, 101);
            groupBox2.Margin = new Padding(2, 3, 2, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(2, 3, 2, 3);
            groupBox2.Size = new Size(193, 495);
            groupBox2.TabIndex = 15;
            groupBox2.TabStop = false;
            groupBox2.Text = "Sensor";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50.9708748F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8.899677F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40.12945F));
            tableLayoutPanel1.Controls.Add(BatteryData, 2, 5);
            tableLayoutPanel1.Controls.Add(HumidityData, 2, 4);
            tableLayoutPanel1.Controls.Add(TempData, 2, 3);
            tableLayoutPanel1.Controls.Add(CdsData, 2, 2);
            tableLayoutPanel1.Controls.Add(GasData, 2, 1);
            tableLayoutPanel1.Controls.Add(FireData, 2, 0);
            tableLayoutPanel1.Controls.Add(label6, 0, 5);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(label2, 0, 1);
            tableLayoutPanel1.Controls.Add(label5, 0, 4);
            tableLayoutPanel1.Controls.Add(label3, 0, 2);
            tableLayoutPanel1.Controls.Add(label4, 0, 3);
            tableLayoutPanel1.Controls.Add(label8, 0, 6);
            tableLayoutPanel1.Controls.Add(PumpLabel, 2, 6);
            tableLayoutPanel1.Location = new Point(2, 19);
            tableLayoutPanel1.Margin = new Padding(2, 3, 2, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 7;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
            tableLayoutPanel1.Size = new Size(187, 469);
            tableLayoutPanel1.TabIndex = 18;
            // 
            // BatteryData
            // 
            BatteryData.Anchor = AnchorStyles.None;
            BatteryData.AutoSize = true;
            BatteryData.Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Regular, GraphicsUnit.Point);
            BatteryData.ForeColor = Color.White;
            BatteryData.Location = new Point(119, 356);
            BatteryData.Name = "BatteryData";
            BatteryData.Size = new Size(59, 25);
            BatteryData.TabIndex = 8;
            BatteryData.Text = "None";
            // 
            // HumidityData
            // 
            HumidityData.Anchor = AnchorStyles.None;
            HumidityData.AutoSize = true;
            HumidityData.Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Regular, GraphicsUnit.Point);
            HumidityData.ForeColor = Color.White;
            HumidityData.Location = new Point(119, 289);
            HumidityData.Name = "HumidityData";
            HumidityData.Size = new Size(59, 25);
            HumidityData.TabIndex = 7;
            HumidityData.Text = "None";
            // 
            // TempData
            // 
            TempData.Anchor = AnchorStyles.None;
            TempData.AutoSize = true;
            TempData.Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Regular, GraphicsUnit.Point);
            TempData.ForeColor = Color.White;
            TempData.Location = new Point(119, 222);
            TempData.Name = "TempData";
            TempData.Size = new Size(59, 25);
            TempData.TabIndex = 6;
            TempData.Text = "None";
            // 
            // CdsData
            // 
            CdsData.Anchor = AnchorStyles.None;
            CdsData.AutoSize = true;
            CdsData.Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Regular, GraphicsUnit.Point);
            CdsData.ForeColor = Color.White;
            CdsData.Location = new Point(119, 155);
            CdsData.Name = "CdsData";
            CdsData.Size = new Size(59, 25);
            CdsData.TabIndex = 5;
            CdsData.Text = "None";
            // 
            // GasData
            // 
            GasData.Anchor = AnchorStyles.None;
            GasData.AutoSize = true;
            GasData.Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Regular, GraphicsUnit.Point);
            GasData.ForeColor = Color.White;
            GasData.Location = new Point(119, 88);
            GasData.Name = "GasData";
            GasData.Size = new Size(59, 25);
            GasData.TabIndex = 4;
            GasData.Text = "None";
            // 
            // FireData
            // 
            FireData.Anchor = AnchorStyles.None;
            FireData.AutoSize = true;
            FireData.Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Regular, GraphicsUnit.Point);
            FireData.ForeColor = Color.White;
            FireData.Location = new Point(119, 21);
            FireData.Name = "FireData";
            FireData.Size = new Size(59, 25);
            FireData.TabIndex = 3;
            FireData.Text = "None";
            // 
            // label6
            // 
            label6.Anchor = AnchorStyles.None;
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Regular, GraphicsUnit.Point);
            label6.ForeColor = SystemColors.ButtonFace;
            label6.Location = new Point(22, 354);
            label6.Margin = new Padding(2, 0, 2, 0);
            label6.Name = "label6";
            label6.Size = new Size(51, 29);
            label6.TabIndex = 14;
            label6.Text = "전원";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.None;
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ButtonFace;
            label1.Location = new Point(22, 19);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(51, 29);
            label1.TabIndex = 9;
            label1.Text = "불꽃";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.None;
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ButtonFace;
            label2.Location = new Point(22, 86);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(51, 29);
            label2.TabIndex = 10;
            label2.Text = "가스";
            // 
            // label5
            // 
            label5.Anchor = AnchorStyles.None;
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Regular, GraphicsUnit.Point);
            label5.ForeColor = SystemColors.ButtonFace;
            label5.Location = new Point(22, 287);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(51, 29);
            label5.TabIndex = 13;
            label5.Text = "습도";
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.None;
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.ButtonFace;
            label3.Location = new Point(22, 153);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(51, 29);
            label3.TabIndex = 11;
            label3.Text = "조도";
            // 
            // label4
            // 
            label4.Anchor = AnchorStyles.None;
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Regular, GraphicsUnit.Point);
            label4.ForeColor = SystemColors.ButtonFace;
            label4.Location = new Point(22, 220);
            label4.Margin = new Padding(2, 0, 2, 0);
            label4.Name = "label4";
            label4.Size = new Size(51, 29);
            label4.TabIndex = 12;
            label4.Text = "온도";
            // 
            // label8
            // 
            label8.Anchor = AnchorStyles.None;
            label8.AutoSize = true;
            label8.Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Regular, GraphicsUnit.Point);
            label8.ForeColor = SystemColors.ButtonFace;
            label8.Location = new Point(4, 423);
            label8.Margin = new Padding(2, 0, 2, 0);
            label8.Name = "label8";
            label8.Size = new Size(87, 25);
            label8.TabIndex = 15;
            label8.Text = "스프링클러";
            // 
            // PumpLabel
            // 
            PumpLabel.Anchor = AnchorStyles.None;
            PumpLabel.AutoSize = true;
            PumpLabel.Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Regular, GraphicsUnit.Point);
            PumpLabel.Location = new Point(119, 423);
            PumpLabel.Margin = new Padding(2, 0, 2, 0);
            PumpLabel.Name = "PumpLabel";
            PumpLabel.Size = new Size(59, 25);
            PumpLabel.TabIndex = 16;
            PumpLabel.Text = "None";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(VideoPlayer);
            groupBox1.FlatStyle = FlatStyle.Flat;
            groupBox1.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            groupBox1.ForeColor = Color.White;
            groupBox1.Location = new Point(28, 101);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1103, 667);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Video Stream";
            // 
            // VideoPlayer
            // 
            VideoPlayer.BackgroundImageLayout = ImageLayout.Stretch;
            VideoPlayer.Location = new Point(2, 16);
            VideoPlayer.Name = "VideoPlayer";
            VideoPlayer.Size = new Size(1100, 645);
            VideoPlayer.TabIndex = 0;
            VideoPlayer.TabStop = false;
            // 
            // FireDetectForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1369, 817);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FireDetectForm";
            Text = "FireDetectForm";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)VideoPlayer).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private GroupBox groupBox1;
        private PictureBox VideoPlayer;
        private Button bt_start;
        private Button bt_stop;
 		private Button btnSnapshot;
        private Button btnCalibration;
        private Button PumpOnButton;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Label label7;
        private TableLayoutPanel tableLayoutPanel1;
        private Label BatteryData;
        private Label HumidityData;
        private Label TempData;
        private Label CdsData;
        private Label GasData;
        private Label FireData;
        private Label label6;
        private Label label1;
        private Label label2;
        private Label label5;
        private Label label3;
        private Label label4;
        private Label label8;
        private Label PumpLabel;
    }
}