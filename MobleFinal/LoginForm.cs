using MobleFinal._Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Xml.Linq;
using MobleFinal.DTO;
using MobleFinal.DAO;


namespace MobleFinal
{
    public partial class LoginForm : Form
    {
        private UserDao userDao;
        private SqliteHandler sqliteHandler;

        public LoginForm()
        {
            InitializeComponent();
            userDao = new UserDao();
            sqliteHandler = new SqliteHandler();
        }

        //로그인. 메인폼으로 이동 ***
        private void btnLogin_Click(object sender, EventArgs e)
        {
            //일반 사용자
            string email = tbEmail.Text;
            string password = tbPassword.Text;
            bool login = userDao.LoginMember(email, password);
            
            string Id = tbEmail.Text;
            string Password = tbPassword.Text;
			bool adminLogin = sqliteHandler.AdminCheck(Id, Password);

			if (login)
            {
                //일반 사용자 폼
                MessageBox.Show("로그인이 완료되었습니다.");
                Info.User = userDao.GetMemberByEmail(email);
                MainUserForm userform = new MainUserForm();
                userform.StartPosition = FormStartPosition.CenterScreen;
                userform.Show();
                userform.BringToFront();
           

            }
            //어드민 로그인
            else if (adminLogin)
            {
				//관리자 폼 열기
				MessageBox.Show("어드민 로그인이 완료되었습니다.");
                
                MainAdminForm main = new MainAdminForm();
                main.StartPosition = FormStartPosition.CenterScreen;
                main.Show();
                main.BringToFront();			
			}
			else
            {
                MessageBox.Show("이메일 또는 비밀번호가 잘못되었습니다.");
            }
        }

        //회원가입으로 이동
        private void btnRegister_Click(object sender, EventArgs e)
        {
            RegisterForm register = new RegisterForm(this);
            register.StartPosition = FormStartPosition.CenterScreen;
            register.Show();
            this.Hide();
        }

        //비밀번호 찾기로 이동
        private void btnFindPw_Click(object sender, EventArgs e)
        {
            FindPasswordForm findpassword = new FindPasswordForm(this);
            findpassword.StartPosition = FormStartPosition.CenterScreen;
            findpassword.Show();
            this.Hide();
        }

        //나가기
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.FlatAppearance.MouseDownBackColor = Color.Transparent;
            // 창 최소화
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
