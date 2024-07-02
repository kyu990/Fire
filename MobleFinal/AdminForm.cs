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
using MobleFinal._Service;
using MobleFinal.DAO;
using MobleFinal.DTO;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;


namespace MobleFinal
{
    public partial class AdminForm : Form
    {
        private UserDao userDao;
        private List<User> users;

        public AdminForm()
        {
            InitializeComponent();
            userDao = new UserDao();
            users = userDao.GetAllUsers();

            User_list.SelectedIndexChanged += User_list_SelectedIndexChanged;
        }

        private void Update_listview()
        {
            User_list.Items.Clear();
            int i = 0;
            // ListView에 번호와 이름 보여주기
            foreach (User u in users)
            {
                ListViewItem item = new ListViewItem();
                item.Text = (i + 1).ToString(); // 첫 번째 열에 번호 추가
                item.SubItems.Add(u.Name); // 두 번째 열에 이름 추가
                item.ForeColor = Color.Black; // 텍스트 색상 변경
                item.Font = new Font("맑은 고딕", 9, FontStyle.Bold); // 폰트 설정
                User_list.Items.Add(item);
                i++;
            }
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {

            Update_listview();
            User_Authoritys.Items.Add("일반 사용자");
            User_Authoritys.Items.Add("관리자");

        }

        private void User_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (User_list.SelectedItems.Count > 0)
            {
                int selectedIndex = User_list.SelectedItems[0].Index;
                ShowUserInfo(users[selectedIndex]);
            }
        }

        private void ShowUserInfo(User user)
        {
            // 사용자 정보를 각각의 컨트롤에 표시
            User_email.Text = user.Email;
            User_Name.Text = user.Name;
            User_Tel.Text = PhoneNumber(user.Tel);
            try
            {
                User_Image.Image = Image.FromFile(user.ProfilePicturePath);
            }
            catch(Exception e) { Console.WriteLine(e); }
            

            if (!string.IsNullOrEmpty(user.Address))
            {
                string[] addressParts = user.Address.Split('@');
                // 주소의 첫 번째 부분을 표시
                if (addressParts.Length >= 1)
                {
                    User_AddressNum.Text = addressParts[0];
                }
                else
                {
                    User_AddressNum.Text = ""; // 주소가 없을 때 공백 처리
                }
                // 주소의 두 번째 부분을 표시
                if (addressParts.Length >= 2)
                {
                    DB_Address1.Text = addressParts[1];
                }
                else
                {
                    DB_Address1.Text = ""; // 주소가 없을 때 공백 처리
                }
                // 주소의 세 번째 부분을 표시
                if (addressParts.Length >= 3)
                {
                    DB_Address2.Text = addressParts[2];
                }
                else
                {
                    DB_Address2.Text = ""; // 주소가 없을 때 공백 처리
                }
            }
            else
            {
                // 주소가 없는 경우 모든 TextBox를 공백으로 설정
                User_AddressNum.Text = "";
                DB_Address1.Text = "";
                DB_Address2.Text = "";
            }

            if (user.Authority == 1)
            {
                User_Authoritys.SelectedItem = "일반 사용자";
            }
            else if (user.Authority == 2)
            {
                User_Authoritys.SelectedItem = "관리자";
            }
        }

        private string PhoneNumber(string phoneNumber)
        {
            // 전화번호가 null이거나 비어있는 경우 그대로 반환
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return phoneNumber;
            }

            // 전화번호의 길이가 10자리인 경우 (010-1234-5678 형식)
            if (phoneNumber.Length == 11)
            {
                return phoneNumber.Substring(0, 3) + "-" + phoneNumber.Substring(3, 4) + "-" + phoneNumber.Substring(7);
            }
            // 전화번호의 길이가 11자리인 경우 (02-1234-5678 형식)
            else if (phoneNumber.Length == 12)
            {
                return phoneNumber.Substring(0, 2) + "-" + phoneNumber.Substring(2, 4) + "-" + phoneNumber.Substring(6);
            }
            // 그 외의 경우는 전화번호를 그대로 반환
            else
            {
                return phoneNumber;
            }
        }

        private void bt_undo_Click(object sender, EventArgs e)
        {
            if (User_list.SelectedItems.Count > 0)
            {
                int selectedIndex = User_list.SelectedItems[0].Index;
                ShowUserInfo(users[selectedIndex]);
            }
        }

        private string ChangePhoneNumber(string phoneNumber)
        {
            // 전화번호에서 기호를 제거하여 순수 숫자만 남깁니다.
            StringBuilder normalizedNumber = new StringBuilder();
            foreach (char c in phoneNumber)
            {
                if (char.IsDigit(c))
                {
                    normalizedNumber.Append(c);
                }
            }
            return normalizedNumber.ToString();
        }

        // 사용자가 입력한 주소를 하나의 문자열로 합치는 메서드
        private string CombineAddress()
        {
            // 주소 번호, 첫 번째 주소, 두 번째 주소를 가져옵니다.
            string addressNum = User_AddressNum.Text;
            string address1 = DB_Address1.Text;
            string address2 = DB_Address2.Text;

            // 세 부분을 "@"로 구분하여 하나의 문자열로 합칩니다.
            string combinedAddress = $"{addressNum}@{address1}@{address2}";

            return combinedAddress;
        }

        private void bt_upload_Click(object sender, EventArgs e)
        {
            // 이름 필드가 비어있는지 확인
            if (string.IsNullOrEmpty(User_Name.Text))
            {
                MessageBox.Show("이름을 입력하세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // 이후 코드 실행 중지
            }

            // 전화번호 형식 검사
            if (!IsPhoneNumberValid(User_Tel.Text))
            {
                MessageBox.Show("전화번호 형식이 올바르지 않습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // 이후 코드 실행 중지
            }

            // 주소 번호 형식 검사
            if (!IsAddressNumValid(User_AddressNum.Text))
            {
                MessageBox.Show("우편 번호는 숫자로만 구성되어야 합니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // 이후 코드 실행 중지
            }

            // 사용자가 선택한 권한 확인
            int authority = 0;
            if (User_Authoritys.SelectedItem != null)
            {
                if (User_Authoritys.SelectedItem.ToString() == "일반 사용자")
                {
                    authority = 1;
                }
                else if (User_Authoritys.SelectedItem.ToString() == "관리자")
                {
                    authority = 2;
                }
            }

            // 권한이 선택되었는지 확인
            if (authority == 0)
            {
                MessageBox.Show("권한을 선택하세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // 이후 코드 실행 중지
            }

            // 모든 유효성 검사를 통과한 경우에만 업로드 진행
            // 현재 선택된 사용자의 정보 업데이트
            if (User_list.SelectedItems.Count > 0)
            {
                int selectedIndex = User_list.SelectedItems[0].Index;
                User selectedUser = users[selectedIndex];
                selectedUser.Name = User_Name.Text;
                selectedUser.Tel = ChangePhoneNumber(User_Tel.Text.Replace("-", "")); // 전화번호 정규화 및 기호 제거
                string address = CombineAddress(); // 사용자가 입력한 주소를 합치기
                selectedUser.Address = address;
                selectedUser.Authority = authority;

                // 데이터베이스에 변경된 사용자 정보 업데이트
                userDao.UpdateAdmin(selectedUser);

                // 업로드 완료 메시지 표시
                MessageBox.Show("정보가 업로드되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Update_listview();
            }
            else
            {
                MessageBox.Show("사용자를 선택하세요.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // 전화번호 형식 검사 메서드
        private bool IsPhoneNumberValid(string phoneNumber)
        {
            // 전화번호는 010-1234-5678 형식
            string pattern = @"^01[0-9]-\d{4}-\d{4}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }

        // 주소 번호 형식 검사 메서드
        private bool IsAddressNumValid(string addressNum)
        {
            // 주소 번호가 숫자로만 구성되어 있는지 확인
            foreach (char c in addressNum)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            // 형식이 올바르면 true 반환
            return true;
        }
    }
}


