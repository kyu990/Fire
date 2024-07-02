namespace MobleFinal
{
    partial class RegisterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegisterForm));
            lbEmail = new Label();
            lbPassword = new Label();
            lbName = new Label();
            lbAddress = new Label();
            lbTel = new Label();
            tbEmail = new TextBox();
            tbEmailCheck = new TextBox();
            tbPassword = new TextBox();
            tbPassword2 = new TextBox();
            tbName = new TextBox();
            tbAddress = new TextBox();
            tbTel = new TextBox();
            cbEmail = new ComboBox();
            btnEmailSend = new Button();
            btnEmailCheck = new Button();
            btnPasswordCheck = new Button();
            btnBack = new Button();
            btnRegister = new Button();
            lbDomain = new Label();
            panel1 = new Panel();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            panel2 = new Panel();
            tbAddress2 = new TextBox();
            tbAddress1 = new TextBox();
            label2 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // lbEmail
            // 
            lbEmail.AutoSize = true;
            lbEmail.ForeColor = Color.White;
            lbEmail.Location = new Point(97, 101);
            lbEmail.Name = "lbEmail";
            lbEmail.Size = new Size(43, 15);
            lbEmail.TabIndex = 0;
            lbEmail.Text = "이메일";
            // 
            // lbPassword
            // 
            lbPassword.AutoSize = true;
            lbPassword.ForeColor = Color.White;
            lbPassword.Location = new Point(97, 201);
            lbPassword.Name = "lbPassword";
            lbPassword.Size = new Size(55, 15);
            lbPassword.TabIndex = 1;
            lbPassword.Text = "비밀번호";
            // 
            // lbName
            // 
            lbName.AutoSize = true;
            lbName.ForeColor = Color.White;
            lbName.Location = new Point(97, 297);
            lbName.Name = "lbName";
            lbName.Size = new Size(31, 15);
            lbName.TabIndex = 2;
            lbName.Text = "성명";
            // 
            // lbAddress
            // 
            lbAddress.AutoSize = true;
            lbAddress.ForeColor = Color.White;
            lbAddress.Location = new Point(97, 348);
            lbAddress.Name = "lbAddress";
            lbAddress.Size = new Size(55, 15);
            lbAddress.TabIndex = 3;
            lbAddress.Text = "우편번호";
            // 
            // lbTel
            // 
            lbTel.AutoSize = true;
            lbTel.ForeColor = Color.White;
            lbTel.Location = new Point(97, 469);
            lbTel.Name = "lbTel";
            lbTel.Size = new Size(55, 15);
            lbTel.TabIndex = 4;
            lbTel.Text = "전화번호";
            // 
            // tbEmail
            // 
            tbEmail.Location = new Point(176, 97);
            tbEmail.Name = "tbEmail";
            tbEmail.Size = new Size(130, 23);
            tbEmail.TabIndex = 5;
            tbEmail.Enter += tbEmail_Enter;
            tbEmail.Leave += tbEmail_Leave;
            // 
            // tbEmailCheck
            // 
            tbEmailCheck.Location = new Point(176, 152);
            tbEmailCheck.Name = "tbEmailCheck";
            tbEmailCheck.Size = new Size(250, 23);
            tbEmailCheck.TabIndex = 6;
            tbEmailCheck.TextChanged += tbEmailCheck_TextChanged;
            tbEmailCheck.Enter += tbEmailCheck_Enter;
            tbEmailCheck.Leave += tbEmailCheck_Leave;
            // 
            // tbPassword
            // 
            tbPassword.Location = new Point(176, 198);
            tbPassword.Name = "tbPassword";
            tbPassword.PasswordChar = '*';
            tbPassword.Size = new Size(250, 23);
            tbPassword.TabIndex = 7;
            tbPassword.Enter += tbPassword_Enter;
            tbPassword.Leave += tbPassword_Leave;
            // 
            // tbPassword2
            // 
            tbPassword2.Location = new Point(176, 247);
            tbPassword2.Name = "tbPassword2";
            tbPassword2.PasswordChar = '*';
            tbPassword2.Size = new Size(250, 23);
            tbPassword2.TabIndex = 8;
            tbPassword2.TextChanged += tbPassword2_TextChanged;
            tbPassword2.Enter += tbPassword2_Enter;
            tbPassword2.Leave += tbPassword2_Leave;
            // 
            // tbName
            // 
            tbName.Location = new Point(176, 294);
            tbName.Name = "tbName";
            tbName.Size = new Size(250, 23);
            tbName.TabIndex = 9;
            tbName.Enter += tbName_Enter;
            tbName.Leave += tbName_Leave;
            // 
            // tbAddress
            // 
            tbAddress.Location = new Point(176, 345);
            tbAddress.Name = "tbAddress";
            tbAddress.Size = new Size(71, 23);
            tbAddress.TabIndex = 10;
            tbAddress.Enter += tbAddress_Enter;
            tbAddress.Leave += tbAddress_Leave;
            // 
            // tbTel
            // 
            tbTel.Location = new Point(176, 466);
            tbTel.Name = "tbTel";
            tbTel.Size = new Size(250, 23);
            tbTel.TabIndex = 11;
            tbTel.Enter += tbTel_Enter;
            tbTel.Leave += tbTel_Leave;
            // 
            // cbEmail
            // 
            cbEmail.FormattingEnabled = true;
            cbEmail.Items.AddRange(new object[] { "gmail.com", "naver.com", "daum.net" });
            cbEmail.Location = new Point(332, 98);
            cbEmail.Name = "cbEmail";
            cbEmail.Size = new Size(94, 23);
            cbEmail.TabIndex = 12;
            // 
            // btnEmailSend
            // 
            btnEmailSend.Location = new Point(448, 97);
            btnEmailSend.Name = "btnEmailSend";
            btnEmailSend.Size = new Size(97, 23);
            btnEmailSend.TabIndex = 13;
            btnEmailSend.Text = "인증 코드 전송";
            btnEmailSend.UseVisualStyleBackColor = true;
            btnEmailSend.Click += btnEmailSend_Click;
            // 
            // btnEmailCheck
            // 
            btnEmailCheck.Location = new Point(448, 152);
            btnEmailCheck.Name = "btnEmailCheck";
            btnEmailCheck.Size = new Size(97, 23);
            btnEmailCheck.TabIndex = 14;
            btnEmailCheck.Text = "인증 확인";
            btnEmailCheck.UseVisualStyleBackColor = true;
            btnEmailCheck.Click += btnEmailCheck_Click;
            // 
            // btnPasswordCheck
            // 
            btnPasswordCheck.Location = new Point(448, 247);
            btnPasswordCheck.Name = "btnPasswordCheck";
            btnPasswordCheck.Size = new Size(97, 23);
            btnPasswordCheck.TabIndex = 15;
            btnPasswordCheck.Text = "비밀번호 확인";
            btnPasswordCheck.UseVisualStyleBackColor = true;
            btnPasswordCheck.Click += btnPasswordCheck_Click;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(194, 549);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(40, 30);
            btnBack.TabIndex = 16;
            btnBack.Text = "←";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // btnRegister
            // 
            btnRegister.Location = new Point(255, 549);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(150, 30);
            btnRegister.TabIndex = 17;
            btnRegister.Text = "완료";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;
            // 
            // lbDomain
            // 
            lbDomain.AutoSize = true;
            lbDomain.ForeColor = Color.White;
            lbDomain.Location = new Point(310, 101);
            lbDomain.Name = "lbDomain";
            lbDomain.Size = new Size(19, 15);
            lbDomain.TabIndex = 18;
            lbDomain.Text = "@";
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(21, 32, 49);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(panel2);
            panel1.Location = new Point(-2, -2);
            panel1.Name = "panel1";
            panel1.Size = new Size(992, 766);
            panel1.TabIndex = 19;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("맑은 고딕", 36F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.White;
            label1.Location = new Point(343, 40);
            label1.Name = "label1";
            label1.Size = new Size(280, 65);
            label1.TabIndex = 19;
            label1.Text = "One Group";
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(614, 32);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(94, 89);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 19;
            pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            panel2.BackgroundImage = (Image)resources.GetObject("panel2.BackgroundImage");
            panel2.BackgroundImageLayout = ImageLayout.Stretch;
            panel2.Controls.Add(tbAddress2);
            panel2.Controls.Add(tbAddress1);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(tbEmailCheck);
            panel2.Controls.Add(tbAddress);
            panel2.Controls.Add(tbName);
            panel2.Controls.Add(lbDomain);
            panel2.Controls.Add(tbPassword2);
            panel2.Controls.Add(lbEmail);
            panel2.Controls.Add(tbPassword);
            panel2.Controls.Add(btnRegister);
            panel2.Controls.Add(tbTel);
            panel2.Controls.Add(lbPassword);
            panel2.Controls.Add(cbEmail);
            panel2.Controls.Add(btnBack);
            panel2.Controls.Add(tbEmail);
            panel2.Controls.Add(lbName);
            panel2.Controls.Add(btnEmailSend);
            panel2.Controls.Add(btnPasswordCheck);
            panel2.Controls.Add(lbTel);
            panel2.Controls.Add(lbAddress);
            panel2.Controls.Add(btnEmailCheck);
            panel2.Location = new Point(195, 127);
            panel2.Name = "panel2";
            panel2.Size = new Size(602, 624);
            panel2.TabIndex = 19;
            // 
            // tbAddress2
            // 
            tbAddress2.Location = new Point(176, 422);
            tbAddress2.Name = "tbAddress2";
            tbAddress2.Size = new Size(250, 23);
            tbAddress2.TabIndex = 20;
            // 
            // tbAddress1
            // 
            tbAddress1.Location = new Point(176, 384);
            tbAddress1.Name = "tbAddress1";
            tbAddress1.Size = new Size(250, 23);
            tbAddress1.TabIndex = 20;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.White;
            label2.Location = new Point(97, 384);
            label2.Name = "label2";
            label2.Size = new Size(31, 15);
            label2.TabIndex = 19;
            label2.Text = "주소";
            // 
            // RegisterForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(989, 761);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "RegisterForm";
            Text = "RegisterForm";
            Load += RegisterForm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label lbEmail;
        private Label lbPassword;
        private Label lbName;
        private Label lbAddress;
        private Label lbTel;
        private TextBox tbEmail;
        private TextBox tbEmailCheck;
        private TextBox tbPassword;
        private TextBox tbPassword2;
        private TextBox tbName;
        private TextBox tbAddress;
        private TextBox tbTel;
        private ComboBox cbEmail;
        private Button btnEmailSend;
        private Button btnEmailCheck;
        private Button btnPasswordCheck;
        private Button btnBack;
        private Button btnRegister;
        private Label lbDomain;
        private Panel panel1;
        private Panel panel2;
        private Label label1;
        private PictureBox pictureBox1;
        private TextBox tbAddress2;
        private TextBox tbAddress1;
        private Label label2;
    }
}