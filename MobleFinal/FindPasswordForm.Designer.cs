namespace MobleFinal
{
    partial class FindPasswordForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindPasswordForm));
            lbName = new Label();
            lbEmail = new Label();
            lbNewPassword = new Label();
            lbNewPassword2 = new Label();
            tbName = new TextBox();
            tbEmail = new TextBox();
            tbEmailCheck = new TextBox();
            tbNewPassword = new TextBox();
            tbNewPassword2 = new TextBox();
            btnEmailSend = new Button();
            btnEmailCheck = new Button();
            btnBack = new Button();
            btnRegister = new Button();
            panel1 = new Panel();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // lbName
            // 
            lbName.AutoSize = true;
            lbName.ForeColor = Color.White;
            lbName.Location = new Point(89, 115);
            lbName.Name = "lbName";
            lbName.Size = new Size(31, 15);
            lbName.TabIndex = 0;
            lbName.Text = "성명";
            // 
            // lbEmail
            // 
            lbEmail.AutoSize = true;
            lbEmail.ForeColor = Color.White;
            lbEmail.Location = new Point(89, 162);
            lbEmail.Name = "lbEmail";
            lbEmail.Size = new Size(43, 15);
            lbEmail.TabIndex = 1;
            lbEmail.Text = "이메일";
            // 
            // lbNewPassword
            // 
            lbNewPassword.AutoSize = true;
            lbNewPassword.ForeColor = Color.White;
            lbNewPassword.Location = new Point(89, 251);
            lbNewPassword.Name = "lbNewPassword";
            lbNewPassword.Size = new Size(95, 15);
            lbNewPassword.TabIndex = 2;
            lbNewPassword.Text = "새로운 비밀번호";
            // 
            // lbNewPassword2
            // 
            lbNewPassword2.AutoSize = true;
            lbNewPassword2.ForeColor = Color.White;
            lbNewPassword2.Location = new Point(89, 323);
            lbNewPassword2.Name = "lbNewPassword2";
            lbNewPassword2.Size = new Size(83, 15);
            lbNewPassword2.TabIndex = 3;
            lbNewPassword2.Text = "비밀번호 확인";
            // 
            // tbName
            // 
            tbName.Location = new Point(190, 112);
            tbName.Name = "tbName";
            tbName.Size = new Size(250, 23);
            tbName.TabIndex = 4;
            tbName.Enter += tbName_Enter;
            tbName.Leave += tbName_Leave;
            // 
            // tbEmail
            // 
            tbEmail.Location = new Point(190, 159);
            tbEmail.Name = "tbEmail";
            tbEmail.Size = new Size(250, 23);
            tbEmail.TabIndex = 5;
            tbEmail.Enter += tbEmail_Enter;
            tbEmail.Leave += tbEmail_Leave;
            // 
            // tbEmailCheck
            // 
            tbEmailCheck.Location = new Point(190, 189);
            tbEmailCheck.Name = "tbEmailCheck";
            tbEmailCheck.Size = new Size(250, 23);
            tbEmailCheck.TabIndex = 6;
            tbEmailCheck.TextChanged += tbEmailCheck_TextChanged;
            tbEmailCheck.Enter += tbEmailCheck_Enter;
            tbEmailCheck.Leave += tbEmailCheck_Leave;
            // 
            // tbNewPassword
            // 
            tbNewPassword.Location = new Point(190, 248);
            tbNewPassword.Name = "tbNewPassword";
            tbNewPassword.PasswordChar = '*';
            tbNewPassword.Size = new Size(250, 23);
            tbNewPassword.TabIndex = 7;
            tbNewPassword.Enter += tbNewPassword_Enter;
            tbNewPassword.Leave += tbNewPassword_Leave;
            // 
            // tbNewPassword2
            // 
            tbNewPassword2.Location = new Point(190, 320);
            tbNewPassword2.Name = "tbNewPassword2";
            tbNewPassword2.PasswordChar = '*';
            tbNewPassword2.Size = new Size(250, 23);
            tbNewPassword2.TabIndex = 8;
            tbNewPassword2.Enter += tbNewPassword2_Enter;
            tbNewPassword2.Leave += tbNewPassword2_Leave;
            // 
            // btnEmailSend
            // 
            btnEmailSend.Location = new Point(447, 159);
            btnEmailSend.Name = "btnEmailSend";
            btnEmailSend.Size = new Size(97, 23);
            btnEmailSend.TabIndex = 9;
            btnEmailSend.Text = "인증코드 전송";
            btnEmailSend.UseVisualStyleBackColor = true;
            btnEmailSend.Click += btnEmailSend_Click;
            // 
            // btnEmailCheck
            // 
            btnEmailCheck.Location = new Point(447, 189);
            btnEmailCheck.Name = "btnEmailCheck";
            btnEmailCheck.Size = new Size(97, 23);
            btnEmailCheck.TabIndex = 10;
            btnEmailCheck.Text = "인증 확인";
            btnEmailCheck.UseVisualStyleBackColor = true;
            btnEmailCheck.Click += btnEmailCheck_Click;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(186, 422);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(40, 30);
            btnBack.TabIndex = 11;
            btnBack.Text = "←";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // btnRegister
            // 
            btnRegister.Location = new Point(241, 422);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(150, 30);
            btnRegister.TabIndex = 12;
            btnRegister.Text = "완료";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;
            // 
            // panel1
            // 
            panel1.BackgroundImage = (Image)resources.GetObject("panel1.BackgroundImage");
            panel1.BackgroundImageLayout = ImageLayout.Stretch;
            panel1.Controls.Add(tbNewPassword);
            panel1.Controls.Add(btnRegister);
            panel1.Controls.Add(lbName);
            panel1.Controls.Add(btnBack);
            panel1.Controls.Add(lbEmail);
            panel1.Controls.Add(btnEmailCheck);
            panel1.Controls.Add(lbNewPassword);
            panel1.Controls.Add(btnEmailSend);
            panel1.Controls.Add(lbNewPassword2);
            panel1.Controls.Add(tbNewPassword2);
            panel1.Controls.Add(tbName);
            panel1.Controls.Add(tbEmail);
            panel1.Controls.Add(tbEmailCheck);
            panel1.Location = new Point(206, 163);
            panel1.Name = "panel1";
            panel1.Size = new Size(602, 567);
            panel1.TabIndex = 13;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("맑은 고딕", 36F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.White;
            label1.Location = new Point(328, 45);
            label1.Name = "label1";
            label1.Size = new Size(280, 65);
            label1.TabIndex = 20;
            label1.Text = "One Group";
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(599, 37);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(94, 89);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 21;
            pictureBox1.TabStop = false;
            // 
            // FindPasswordForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(21, 32, 49);
            ClientSize = new Size(992, 766);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FindPasswordForm";
            Text = "FindPasswordForm";
            Load += FindPasswordForm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbName;
        private Label lbEmail;
        private Label lbNewPassword;
        private Label lbNewPassword2;
        private TextBox tbName;
        private TextBox tbEmail;
        private TextBox tbEmailCheck;
        private TextBox tbNewPassword;
        private TextBox tbNewPassword2;
        private Button btnEmailSend;
        private Button btnEmailCheck;
        private Button btnBack;
        private Button btnRegister;
        private Panel panel1;
        private Label label1;
        private PictureBox pictureBox1;
    }
}