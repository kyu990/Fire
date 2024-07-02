using MobleFinal._Service;
using MobleFinal.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;

namespace MobleFinal
{
    public partial class FindPasswordForm : Form
    {
        private Form login;
        private static int count = 0; //인증코드 확인 시+
        private UserDao memberDao;

        public static Random randomNum = new Random(); // 전역변수로 난수 발생 객체 생성
        public static int checkNum = randomNum.Next(10000000, 99999999); //난수는 10,000,000~99,999,999 사이의 8자리 수

        public FindPasswordForm()
        {
            InitializeComponent();
            memberDao = new UserDao();
        }

        public FindPasswordForm(Form LoginForm)
        {
            InitializeComponent();
            memberDao = new UserDao();
            this.login = LoginForm;
        }

        //폼 로드
        private void FindPasswordForm_Load(object sender, EventArgs e)
        {
            if (tbName.Text == "")
            {
                tbName.Text = "성명";
                tbName.ForeColor = Color.Black;
            }

            if (tbEmail.Text == "")
            {
                tbEmail.Text = "이메일";
                tbEmail.ForeColor = Color.Black;
            }

            if (tbEmailCheck.Text == "")
            {
                tbEmailCheck.Text = "인증 코드";
                tbEmailCheck.ForeColor = Color.Black;
            }

            if (tbNewPassword.Text == "")
            {
                tbNewPassword.Text = "비밀번호";
                tbNewPassword.ForeColor = Color.Black;
            }

            if (tbNewPassword2.Text == "")
            {
                tbNewPassword2.Text = "비밀번호 확인";
                tbNewPassword2.ForeColor = Color.Black;
            }
            //버튼 비활성화
            btnEmailCheck.Enabled = false;
            //새 비번 입력창 비활성화
            tbNewPassword.Enabled = false;
            tbNewPassword2.Enabled = false;
        }

        //이메일로 인증코드 전송 ***
        private void btnEmailSend_Click(object sender, EventArgs e)
        {
            string name = tbName.Text;
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            string email = tbEmail.Text;
            bool find = memberDao.FindMember(email, name);
            string SystemMailId = "mobleonegroup@gmail.com";
            string SystemMailPwd = "xvvpeczivufwiwzf"; //앱 비밀번호: xvvpeczivufwiwzf  //비번: Group1!@#!

            MailMessage mail = new MailMessage();

            mail.To.Add(email); //받을 사람 주소
            mail.From = new MailAddress(SystemMailId); //보낸 사람 주소
            mail.Subject = "[One Group] 비밀번호 변경 본인인증"; //이메일 제목
            mail.Body = "아래의 인증코드를 입력하시면 본인인증이 정상적으로 완료됩니다.\n\n";
            mail.Body += checkNum.ToString(); //이메일 내용

            mail.IsBodyHtml = true; //이메일 HTML 사용
            mail.Priority = MailPriority.High; //이메일 중요도 높음
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure; //이메일 전송 실패시 알림 발생

            mail.SubjectEncoding = Encoding.UTF8;
            mail.BodyEncoding = Encoding.UTF8;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.Timeout = 10000;

            smtp.UseDefaultCredentials = false; //true
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new System.Net.NetworkCredential(SystemMailId, SystemMailPwd);


            if (find)
            { //인증코드 보내기 
                try
                {
                    smtp.Send(mail); //SMTP 객체를 통한 이메일 발송
                    smtp.Dispose(); //SMTP CLIENT 연결 해제

                    MessageBox.Show("인증 코드가 전송되었습니다.", "전송 완료");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else if (name == null || name == "성명" || name == "성명을 입력해 주세요.")
            {
                MessageBox.Show("성명을 입력해 주세요.");
            }
            else if (!Regex.IsMatch(email, emailPattern))
            {
                MessageBox.Show("이메일의 형식이 올바르지 않습니다.\n예) moble@google.com");
            }
            else
            {
                MessageBox.Show("없는 회원 정보입니다.");
            }
        }

        //인증코드 인증
        private void btnEmailCheck_Click(object sender, EventArgs e)
        {
            if (tbEmailCheck.Text == checkNum.ToString())
            {
                MessageBox.Show("이메일 인증이 완료되었습니다.", "인증 성공");
                count++;
                tbNewPassword.Enabled = true;
                tbNewPassword2.Enabled = true;
            }
            else
            {
                MessageBox.Show("인증 번호가 다릅니다.", "인증 실패");
            }

        }

        //비밀번호 변경 완료 버튼
        private void btnRegister_Click(object sender, EventArgs e)
        {
            //인증코드, 비번1, 비번2(서로 동일해야함)이 적혀있어야 함
            //새로운 비밀번호가 정규식에 맞게 입력되어야 함
            //비밀번호 정규식 (8~16자의 영문 대/소문자, 숫자, 특수문자)
            string email=tbEmail.Text;
            string passwordPattern = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[!@#$%^&*])[A-Za-z\d!@#$%^&*]{8,16}$";
            string password = tbNewPassword.Text;
            
            if (Regex.IsMatch(password, passwordPattern)
               && tbNewPassword.Text == tbNewPassword2.Text)
            {
                memberDao.ChangeMember(email, password);
                MessageBox.Show("비밀번호 변경이 완료되었습니다.");
                this.Close();
                login.Show();
            }
            if (!Regex.IsMatch(password, passwordPattern))
            {
                MessageBox.Show("비밀번호는 8~16자의 영문 대/소문자, 숫자, 특수문자를 사용해 주세요.");
            }
            if (tbNewPassword.Text != tbNewPassword2.Text)
            {
                MessageBox.Show("비밀번호가 일치하지 않습니다.");
            }
        }

        //뒤로가기 버튼
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
            login.Show();
        }

        private void tbName_Enter(object sender, EventArgs e)
        {
            if (tbName.Text == "성명")
            {
                tbName.Text = "";
                tbName.ForeColor = Color.Black;
            }
            else if (tbName.Text == "성명을 입력해 주세요.")
            {
                tbName.Text = "";
                tbName.ForeColor = Color.Black;
            }
        }

        private void tbName_Leave(object sender, EventArgs e)
        {
            if (tbName.Text == "")
            {
                tbName.Text = "성명을 입력해 주세요.";
                tbName.ForeColor = Color.Red;
            }
        }

        private void tbEmail_Enter(object sender, EventArgs e)
        {
            if (tbEmail.Text == "이메일")
            {
                tbEmail.Text = "";
                tbEmail.ForeColor = Color.Black;
            }
            else if (tbEmail.Text == "이메일을 입력해 주세요.")
            {
                tbEmail.Text = "";
                tbEmail.ForeColor = Color.Black;
            }
        }

        private void tbEmail_Leave(object sender, EventArgs e)
        {
            if (tbEmail.Text == "")
            {
                tbEmail.Text = "이메일을 입력해 주세요.";
                tbEmail.ForeColor = Color.Red;
            }
        }

        private void tbEmailCheck_Enter(object sender, EventArgs e)
        {
            if (tbEmailCheck.Text == "인증 코드")
            {
                tbEmailCheck.Text = "";
                tbEmailCheck.ForeColor = Color.Black;
            }
            else if (tbEmailCheck.Text == "인증 코드를 입력해 주세요.")
            {
                tbEmailCheck.Text = "";
                tbEmailCheck.ForeColor = Color.Black;
            }
        }

        private void tbEmailCheck_Leave(object sender, EventArgs e)
        {
            if (tbEmailCheck.Text == "")
            {
                tbEmailCheck.Text = "인증 코드를 입력해 주세요.";
                tbEmailCheck.ForeColor = Color.Red;
            }
        }

        private void tbNewPassword_Enter(object sender, EventArgs e)
        {
            if (tbNewPassword.Text == "비밀번호")
            {
                tbNewPassword.Text = "";
                tbNewPassword.ForeColor = Color.Black;
            }
            if (tbNewPassword.Text == "비밀번호를 입력해 주세요.")
            {
                tbNewPassword.Text = "";
                tbNewPassword.ForeColor = Color.Black;
            }
        }

        private void tbNewPassword_Leave(object sender, EventArgs e)
        {
            if (tbNewPassword.Text == "")
            {
                tbNewPassword.Text = "비밀번호를 입력해 주세요.";
                tbNewPassword.ForeColor = Color.Red;
            }
        }

        private void tbNewPassword2_Enter(object sender, EventArgs e)
        {
            if (tbNewPassword2.Text == "비밀번호 확인")
            {
                tbNewPassword2.Text = "";
                tbNewPassword2.ForeColor = Color.Black;
            }
            else if (tbNewPassword2.Text == "비밀번호 확인이 필요합니다.")
            {
                tbNewPassword2.Text = "";
                tbNewPassword2.ForeColor = Color.Black;
            }
        }

        private void tbNewPassword2_Leave(object sender, EventArgs e)
        {
            if (tbNewPassword2.Text == "")
            {
                tbNewPassword2.Text = "비밀번호 확인이 필요합니다.";
                tbNewPassword2.ForeColor = Color.Red;
            }
        }

        private void tbEmailCheck_TextChanged(object sender, EventArgs e)
        {
            // 텍스트 박스의 텍스트가 변경될 때
            if (tbEmailCheck.Text.Contains("인증 코드"))
            {
                btnEmailCheck.Enabled = false;
            }
            else if (tbEmailCheck.Text.Contains("인증 코드를 입력해 주세요"))
            {
                btnEmailCheck.Enabled = false;
            }
            else
            {
                btnEmailCheck.Enabled = true;
            }
        }
    }
}
