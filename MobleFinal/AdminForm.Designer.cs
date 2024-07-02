namespace MobleFinal
{
    partial class AdminForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminForm));
            panel1 = new Panel();
            User_list = new ListView();
            Number = new ColumnHeader();
            User = new ColumnHeader();
            panel2 = new Panel();
            panel3 = new Panel();
            bt_upload = new Button();
            bt_undo = new Button();
            User_Authoritys = new ComboBox();
            User_AddressNum = new TextBox();
            DB_Address2 = new TextBox();
            DB_Address1 = new TextBox();
            User_Tel = new TextBox();
            User_Contrynumber = new TextBox();
            User_Name = new TextBox();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            User_email = new TextBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            User_Image = new PictureBox();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)User_Image).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(43, 56, 74);
            panel1.Controls.Add(User_list);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(273, 817);
            panel1.TabIndex = 0;
            // 
            // User_list
            // 
            User_list.Columns.AddRange(new ColumnHeader[] { Number, User });
            User_list.FullRowSelect = true;
            User_list.Location = new Point(1, 102);
            User_list.Name = "User_list";
            User_list.Size = new Size(247, 714);
            User_list.TabIndex = 0;
            User_list.UseCompatibleStateImageBehavior = false;
            User_list.View = View.Details;
            User_list.SelectedIndexChanged += User_list_SelectedIndexChanged;
            // 
            // Number
            // 
            Number.Text = "사용자 번호";
            Number.Width = 90;
            // 
            // User
            // 
            User.Text = "이름";
            User.Width = 80;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(62, 80, 100);
            panel2.Controls.Add(panel3);
            panel2.Controls.Add(User_Image);
            panel2.Location = new Point(248, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1124, 817);
            panel2.TabIndex = 1;
            // 
            // panel3
            // 
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Controls.Add(bt_upload);
            panel3.Controls.Add(bt_undo);
            panel3.Controls.Add(User_Authoritys);
            panel3.Controls.Add(User_AddressNum);
            panel3.Controls.Add(DB_Address2);
            panel3.Controls.Add(DB_Address1);
            panel3.Controls.Add(User_Tel);
            panel3.Controls.Add(User_Contrynumber);
            panel3.Controls.Add(User_Name);
            panel3.Controls.Add(label7);
            panel3.Controls.Add(label6);
            panel3.Controls.Add(label5);
            panel3.Controls.Add(User_email);
            panel3.Controls.Add(label4);
            panel3.Controls.Add(label3);
            panel3.Controls.Add(label2);
            panel3.Controls.Add(label1);
            panel3.Location = new Point(72, 70);
            panel3.Name = "panel3";
            panel3.Size = new Size(742, 633);
            panel3.TabIndex = 2;
            // 
            // bt_upload
            // 
            bt_upload.Image = (Image)resources.GetObject("bt_upload.Image");
            bt_upload.Location = new Point(705, 595);
            bt_upload.Name = "bt_upload";
            bt_upload.Size = new Size(32, 33);
            bt_upload.TabIndex = 3;
            bt_upload.UseVisualStyleBackColor = true;
            bt_upload.Click += bt_upload_Click;
            // 
            // bt_undo
            // 
            bt_undo.Image = (Image)resources.GetObject("bt_undo.Image");
            bt_undo.Location = new Point(663, 595);
            bt_undo.Name = "bt_undo";
            bt_undo.Size = new Size(32, 33);
            bt_undo.TabIndex = 3;
            bt_undo.UseVisualStyleBackColor = true;
            bt_undo.Click += bt_undo_Click;
            // 
            // User_Authoritys
            // 
            User_Authoritys.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            User_Authoritys.FormattingEnabled = true;
            User_Authoritys.Location = new Point(199, 109);
            User_Authoritys.Name = "User_Authoritys";
            User_Authoritys.Size = new Size(104, 25);
            User_Authoritys.TabIndex = 3;
            // 
            // User_AddressNum
            // 
            User_AddressNum.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            User_AddressNum.Location = new Point(199, 450);
            User_AddressNum.Name = "User_AddressNum";
            User_AddressNum.Size = new Size(63, 25);
            User_AddressNum.TabIndex = 2;
            // 
            // DB_Address2
            // 
            DB_Address2.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            DB_Address2.Location = new Point(199, 546);
            DB_Address2.Name = "DB_Address2";
            DB_Address2.Size = new Size(413, 25);
            DB_Address2.TabIndex = 2;
            // 
            // DB_Address1
            // 
            DB_Address1.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            DB_Address1.Location = new Point(199, 501);
            DB_Address1.Name = "DB_Address1";
            DB_Address1.Size = new Size(413, 25);
            DB_Address1.TabIndex = 2;
            // 
            // User_Tel
            // 
            User_Tel.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            User_Tel.Location = new Point(199, 325);
            User_Tel.Name = "User_Tel";
            User_Tel.Size = new Size(185, 25);
            User_Tel.TabIndex = 2;
            // 
            // User_Contrynumber
            // 
            User_Contrynumber.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            User_Contrynumber.Location = new Point(199, 271);
            User_Contrynumber.Name = "User_Contrynumber";
            User_Contrynumber.ReadOnly = true;
            User_Contrynumber.Size = new Size(185, 25);
            User_Contrynumber.TabIndex = 2;
            User_Contrynumber.Text = "****** - *******";
            // 
            // User_Name
            // 
            User_Name.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            User_Name.Location = new Point(199, 219);
            User_Name.Name = "User_Name";
            User_Name.Size = new Size(185, 25);
            User_Name.TabIndex = 2;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("맑은 고딕", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label7.ForeColor = Color.White;
            label7.Location = new Point(67, 499);
            label7.Name = "label7";
            label7.Size = new Size(50, 25);
            label7.TabIndex = 1;
            label7.Text = "주소";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("맑은 고딕", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label6.ForeColor = Color.White;
            label6.Location = new Point(67, 450);
            label6.Name = "label6";
            label6.Size = new Size(95, 25);
            label6.TabIndex = 1;
            label6.Text = "우편 번호";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("맑은 고딕", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label5.ForeColor = Color.White;
            label5.Location = new Point(67, 325);
            label5.Name = "label5";
            label5.Size = new Size(95, 25);
            label5.TabIndex = 1;
            label5.Text = "전화 번호";
            // 
            // User_email
            // 
            User_email.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            User_email.Location = new Point(199, 68);
            User_email.Name = "User_email";
            User_email.ReadOnly = true;
            User_email.Size = new Size(185, 25);
            User_email.TabIndex = 2;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("맑은 고딕", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ForeColor = Color.White;
            label4.Location = new Point(67, 271);
            label4.Name = "label4";
            label4.Size = new Size(95, 25);
            label4.TabIndex = 1;
            label4.Text = "주민 번호";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("맑은 고딕", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = Color.White;
            label3.Location = new Point(67, 219);
            label3.Name = "label3";
            label3.Size = new Size(109, 25);
            label3.TabIndex = 1;
            label3.Text = "이름 (한글)";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("맑은 고딕", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.White;
            label2.Location = new Point(67, 109);
            label2.Name = "label2";
            label2.Size = new Size(95, 25);
            label2.TabIndex = 1;
            label2.Text = "관리 등급";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("맑은 고딕", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.White;
            label1.Location = new Point(67, 68);
            label1.Name = "label1";
            label1.Size = new Size(114, 25);
            label1.TabIndex = 1;
            label1.Text = "사용자 정보";
            // 
            // User_Image
            // 
            User_Image.BackgroundImageLayout = ImageLayout.Stretch;
            User_Image.Image = (Image)resources.GetObject("User_Image.Image");
            User_Image.Location = new Point(849, 70);
            User_Image.Name = "User_Image";
            User_Image.Size = new Size(210, 263);
            User_Image.SizeMode = PictureBoxSizeMode.StretchImage;
            User_Image.TabIndex = 0;
            User_Image.TabStop = false;
            // 
            // AdminForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(1369, 817);
            Controls.Add(panel2);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "AdminForm";
            Text = "AdminForm";
            Load += AdminForm_Load;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)User_Image).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private ListView User_list;
        private Panel panel2;
        private Label label1;
        private PictureBox User_Image;
        private Panel panel3;
        private TextBox User_email;
        private Label label2;
        private ComboBox User_Authoritys;
        private TextBox User_Contrynumber;
        private TextBox User_Name;
        private Label label4;
        private Label label3;
        private TextBox User_AddressNum;
        private TextBox DB_Address2;
        private TextBox DB_Address1;
        private TextBox User_Tel;
        private Label label7;
        private Label label6;
        private Label label5;
        private Button bt_undo;
        private Button bt_upload;
        private ColumnHeader Number;
        private ColumnHeader User;
    }
}