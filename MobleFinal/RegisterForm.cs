using MobleFinal._Service;
using MobleFinal.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;

namespace MobleFinal
{
    public partial class RegisterForm : Form
    {
        private Form login;
        private static int count = 0;
        private UserDao memberDao;

        public static Random randomNum = new Random(); // 전역변수로 난수 발생 객체 생성
        public static int checkNum = randomNum.Next(10000000, 99999999); //난수는 10,000,000~99,999,999 사이의 8자리 수

        public RegisterForm()
        {
            InitializeComponent();
            memberDao = new UserDao();
        }

        public RegisterForm(Form LoginForm)
        {
            InitializeComponent();
            memberDao = new UserDao();
            this.login = LoginForm;
        }

        //폼 로드
        private void RegisterForm_Load(object sender, EventArgs e)
        {
            //버튼 비활성화
            btnEmailCheck.Enabled = false;
            btnPasswordCheck.Enabled = false;

        }

        //이메일로 인증코드 전송 ***
        private void btnEmailSend_Click(object sender, EventArgs e)
        {
            
            MessageBox.Show("인증 번호가 발송 되었습니다.");
            //이메일 정규식 (5~20자, 적어도 하나의 영문 소문자, 숫자는 선택)
            string emailPattern = @"^(?=.*[a-z])[a-z0-9]{5,20}$";
            string email1 = tbEmail.Text;
            string email2 = cbEmail.Text;
            string email = email1 + "@" + email2;

            string SystemMailId = "mobleonegroup@gmail.com";
            string SystemMailPwd = "xvvpeczivufwiwzf"; //xvvpeczivufwiwzf  //Group1!@#!

            MailMessage mail = new MailMessage();

            mail.To.Add(email); //받을 사람 주소
            mail.From = new MailAddress(SystemMailId); //보낸 사람 주소
            mail.Subject = "[One Group] 회원가입 본인인증"; //이메일 제목
            
            mail.Body = "One Group 서비스에 가입하신 것을 환영합니다.<br>아래의 인증코드를 입력하시면 가입이 정상적으로 완료됩니다.<br><br>";
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
            //
            bool checkEmail = memberDao.CheckDuplicateEmail(email);
            if (checkEmail)
            {
                MessageBox.Show("이미 존재하는 Email입니다.");
            }
            else
            {
                if (!Regex.IsMatch(email1, emailPattern))
                {
                    MessageBox.Show("이메일은 5~20자의 영문 소문자, 숫자를 사용해 주세요.");
                }
                else if (cbEmail.SelectedItem == null)
                {
                    MessageBox.Show("도메인을 선택해 주세요.");
                }
                else
                {   //인증코드 보내기 
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
            }
        }

        //인증코드 인증
        private void btnEmailCheck_Click(object sender, EventArgs e)
        {
            if (tbEmailCheck.Text == checkNum.ToString())
            {
                MessageBox.Show("이메일 인증이 완료되었습니다.", "인증 성공");
            }
            else
            {
                MessageBox.Show("인증 번호가 다릅니다.", "인증 실패");
            }
        }

        //비밀번호 중복 확인
        private void btnPasswordCheck_Click(object sender, EventArgs e)
        {
            //비밀번호 정규식 (8~16자, 적어도 하나의 영문 대/소문자 & 숫자 & 특수문자)
            string passwordPattern = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[!@#$%^&*])[A-Za-z\d!@#$%^&*]{8,16}$";
            string password = tbPassword.Text;
            string password2 = tbPassword2.Text;

            if (Regex.IsMatch(password, passwordPattern))
            {
                if (password == password2)
                {
                    MessageBox.Show("비밀번호가 일치합니다.");
                    count++;
                }
                else
                {
                    MessageBox.Show("비밀번호가 일치하지 않습니다.");
                }
            }
            else
            {
                MessageBox.Show("비밀번호는 8~16자의 영문 대/소문자, 숫자, 특수문자를 사용해 주세요.");
            }

        }

        //로그인 폼으로 돌아가기
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
            login.Show();
        }

        //회원가입
        private void btnRegister_Click(object sender, EventArgs e)
        {
            string email1 = tbEmail.Text;
            string email2 = cbEmail.Text;
            string email = email1 + "@" + email2;
            string password = tbPassword2.Text;
            string address = CombineAddress();
            int authority = 1;

            //이름 정규식 (2자 이상, 한글)
            string namePattern = @"^[가-힣]{2,}$";
            string name = tbName.Text;

            //전화번호 정규식 (8자, 010으로 시작)
            string telPattern = @"^010\d{8}$";
            string tel = tbTel.Text;

            //텍스트 검사 (검사할문자열,정규식)
            if (Regex.IsMatch(name, namePattern)
                && Regex.IsMatch(tel, telPattern)
                && (address != "주소를 입력해 주세요."))
            {
                memberDao.RegisterMember(email, password, name, address, authority, tel, Info.ClientSerial);
                MessageBox.Show("회원가입이 완료되었습니다.");
                this.Close();
                login.Show();
            }
            else if (!Regex.IsMatch(name, namePattern))
            {
                MessageBox.Show("이름은 한글로 2자 이상 작성해 주세요.");
            }
            else if (!Regex.IsMatch(tel, telPattern))
            {
                MessageBox.Show("전화번호를 다시 확인해 주세요.\n 예) 01012345678");
            }
            else if (address == "주소를 입력해 주세요.")
            {
                MessageBox.Show("주소를 입력해 주세요.");
            }
            else if (count != 2)
            {
                MessageBox.Show("확인이 필요합니다");
            }

            if (tbEmail.Text == "")
            {
                tbEmail.Text = "이메일을 입력해 주세요.";
                tbEmail.ForeColor = Color.Red;
            }
            if (tbEmailCheck.Text == "")
            {
                tbEmailCheck.Text = "인증 코드를 입력해 주세요.";
                tbEmailCheck.ForeColor = Color.Red;
            }
            if (tbPassword.Text == "")
            {
                tbPassword.Text = "비밀번호를 입력해 주세요.";
                tbPassword.ForeColor = Color.Red;
            }
            if (tbPassword2.Text == "")
            {
                tbPassword2.Text = "비밀번호 확인이 필요합니다.";
                tbPassword2.ForeColor = Color.Red;
            }
            if (tbName.Text == "")
            {
                tbName.Text = "성명을 입력해 주세요.";
                tbName.ForeColor = Color.Red;
            }
            if (tbAddress.Text == "")
            {
                tbAddress.Text = "우편번호를 입력해 주세요.";
                tbAddress.ForeColor = Color.Red;
            }
            if (tbTel.Text == "")
            {
                tbTel.Text = "전화번호를 입력해 주세요.";
                tbTel.ForeColor = Color.Red;
            }
        }

        private string CombineAddress()
        {
            // 주소 번호, 첫 번째 주소, 두 번째 주소를 가져옵니다.
            string addressNum = tbAddress.Text;
            string address1 = tbAddress1.Text;
            string address2 = tbAddress2.Text;

            // 세 부분을 "@"로 구분하여 하나의 문자열로 합칩니다.
            string combinedAddress = $"{addressNum}@{address1}@{address2}";

            return combinedAddress;
        }


        private void tbEmail_Enter(object sender, EventArgs e)
        {
            //포커스가 들어갔을 때
            if (tbEmail.Text == "")
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
            //포커스가 나갔을 때
            if (tbEmail.Text == "")
            {
                tbEmail.Text = "이메일을 입력해 주세요.";
                tbEmail.ForeColor = Color.Red;
            }
        }

        private void tbEmailCheck_Enter(object sender, EventArgs e)
        {
            if (tbEmailCheck.Text == "")
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

        private void tbPassword_Enter(object sender, EventArgs e)
        {
            if (tbPassword.Text == "")
            {
                tbPassword.Text = "";
                tbPassword.ForeColor = Color.Black;
            }
            if (tbPassword.Text == "비밀번호를 입력해 주세요.")
            {
                tbPassword.Text = "";
                tbPassword.ForeColor = Color.Black;
            }
        }

        private void tbPassword_Leave(object sender, EventArgs e)
        {
            if (tbPassword.Text == "")
            {
                tbPassword.Text = "비밀번호를 입력해 주세요.";
                tbPassword.ForeColor = Color.Red;
            }
        }

        private void tbPassword2_Enter(object sender, EventArgs e)
        {
            if (tbPassword2.Text == "비밀번호 확인")
            {
                tbPassword2.Text = "";
                tbPassword2.ForeColor = Color.Black;
            }
            else if (tbPassword2.Text == "비밀번호 확인이 필요합니다.")
            {
                tbPassword2.Text = "";
                tbPassword2.ForeColor = Color.Black;
            }
        }

        private void tbPassword2_Leave(object sender, EventArgs e)
        {
            if (tbPassword2.Text == "")
            {
                tbPassword2.Text = "비밀번호 확인이 필요합니다.";
                tbPassword2.ForeColor = Color.Red;
            }
        }

        private void tbName_Enter(object sender, EventArgs e)
        {
            if (tbName.Text == "")
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

        private void tbAddress_Enter(object sender, EventArgs e)
        {
            if (tbAddress.Text == "")
            {
                tbAddress.Text = "";
                tbAddress.ForeColor = Color.Black;
            }
            else if (tbAddress.Text == "주소를 입력해 주세요.")
            {
                tbAddress.Text = "";
                tbAddress.ForeColor = Color.Black;
            }
        }

        private void tbAddress_Leave(object sender, EventArgs e)
        {
            if (tbAddress.Text == "")
            {
                tbAddress.Text = "주소를 입력해 주세요.";
                tbAddress.ForeColor = Color.Red;
            }
        }

        private void tbTel_Enter(object sender, EventArgs e)
        {
            if (tbTel.Text == "")
            {
                tbTel.Text = "";
                tbTel.ForeColor = Color.Black;
            }
            else if (tbTel.Text == "전화번호를 입력해 주세요.")
            {
                tbTel.Text = "";
                tbTel.ForeColor = Color.Black;
            }
        }

        private void tbTel_Leave(object sender, EventArgs e)
        {
            if (tbTel.Text == "")
            {
                tbTel.Text = "전화번호를 입력해 주세요.";
                tbTel.ForeColor = Color.Red;
            }
        }

        private void tbEmailCheck_TextChanged(object sender, EventArgs e)
        {
            //인증 코드가 입력되면 인증확인 버튼 활성화

            // 텍스트 박스의 텍스트가 변경될 때
            if (tbEmailCheck.Text.Contains("인증 코드를 입력해 주세요"))
            {
                btnEmailCheck.Enabled = false;
            }
            else
            {
                btnEmailCheck.Enabled = true;
            }
        }

        private void tbPassword2_TextChanged(object sender, EventArgs e)
        {
            if (tbEmailCheck.Text.Contains("비밀번호 확인이 필요합니다"))
            {
                btnPasswordCheck.Enabled = false;
            }
            else
            {
                btnPasswordCheck.Enabled = true;
            }
        }
    }
}
