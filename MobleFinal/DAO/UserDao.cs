using MySql.Data.MySqlClient;
using MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobleFinal.DTO;
using System.Xml.Linq;


namespace MobleFinal.DAO
{
    internal class UserDao
    {
        private string server = "bearhouseserver.iptime.org";
        private string port = "13001";
        private string database = "FireDetectionFinal";
        private string id = "root";
        private string pw = "1q2w3e4r@";
        private string connectionString;

        public UserDao()
        {
            connectionString = $"Server={server};Port={port};Database={database};Uid={id};Pwd={pw};";
        }

        // 회원가입 - 아이디, 이메일, 비밀번호, 성명, 주소, 권한, 전화번호, 클라이언트시리얼
        public bool RegisterMember(string Email, string Password, string Name, string Address, int Authority, string Tel, string ClientSerial)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO User (Email, Password, Name, Address, Authority, Tel, ClientSerial) VALUES (@Email, @Password, @Name, @Address, @Authority, @Tel, @ClientSerial)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@Password", Password);
                    command.Parameters.AddWithValue("@Name", Name);
                    command.Parameters.AddWithValue("@Address", Address);
                    command.Parameters.AddWithValue("@Authority", Authority);
                    command.Parameters.AddWithValue("@Tel", Tel);
                    command.Parameters.AddWithValue("@ClientSerial", ClientSerial);

                    int rowsAffected = command.ExecuteNonQuery(); //DB 변경된 부분의 수. 

                    return rowsAffected > 0;
                }
            }
        }

        //이메일 중복 확인
        public bool CheckDuplicateEmail(string Email)
        {
            using(MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open ();
                string query = "SELECT COUNT(*) FROM User WHERE Email = @Email";
                using (MySqlCommand command=new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email",Email);
                    int count = Convert.ToInt32(command.ExecuteScalar()); // 이메일이 데이터베이스에 하나 이상 존재하면 true 반환

                    return count >0; 
                }
            }
        }

        // 회원탈퇴 -  이메일로 조회 후 삭제, SQL문 실행 후 변경사항이 있으면 true, 없으면 false
        public bool UnregisterMember(string Email)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "DELETE FROM User WHERE Email = @Email";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", Email);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }

        //로그인 - 정보 조회
        public bool LoginMember(string Email, string Password)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM User WHERE Email = @Email AND Password = @Password";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@Password", Password);

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    return count == 1; //true 반환
                }
            }
        }

        //비번 변경 - 이메일, 이름 조회(1)
        public bool FindMember(string Email, string Name)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM User WHERE Email = @Email AND Name = @Name";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@Name", Name);

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    return count == 1; //true 반환
                }
            }
        }

        //비번 변경 - 변경(2)
        public bool ChangeMember(string Email, string Password)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE User SET Password = @Password WHERE Email = @Email"; //UPDATE: 기존 데이터를 수정
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@Password", Password);
                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;

                }
            }
        }

        //권한 변경?
        public bool ChangeAuthority(int Id, string Authority)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Authority SET Authority = @Authority WHERE Id = @Id"; //UPDATE: 기존 데이터를 수정
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", Id);
                    command.Parameters.AddWithValue("@Authority", Authority);
                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;

                }
            }
        }

        ////로그인한 회원의 이름과 전화번호 반환
        public User GetMemberByEmail(string Email)
        {
            List<User> loginMembers = new List<User>();

            using (MySqlConnection connection = new MySqlConnection(connectionString)) //DB 연결을 수행하기 위함
            {
                connection.Open();

                string query = "SELECT * FROM User WHERE Email = @Email";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", Email);
                    using (MySqlDataReader reader = command.ExecuteReader()) //DB에서 결과를 읽기 위함
                    {
                        while (reader.Read())
                        {
                            //각 행에서 사용자 정보를 읽고 클래스 객체 생성
                            User user=new User(
                                reader["Email"].ToString(),
                                reader["Password"].ToString(),
                                reader["Name"].ToString(),
                                reader["Address"].ToString(),
                                Convert.ToInt32(reader["Authority"]),
                                reader["Tel"].ToString(),
                                reader["ClientSerial"].ToString(),
                                reader["ProfilePicturePath"].ToString()
                                );
                            loginMembers.Add(user); //새로운 User 객체를 loginMembers 리스트에 추가
                        }
                        reader.Close();
                    }
                }
            }
            return loginMembers[0]; //리스트 반환. 
        }


        // 전체 사용자의 이름 리스트 반환
        public List<User> GetAllUsers()
        {
            List<User> userList = new List<User>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM User";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User user = new User(
                                reader["Email"].ToString(),
                                reader["Name"].ToString(),
                                reader["Address"].ToString(),
                                Convert.ToInt32(reader["Authority"]),
                                reader["Tel"].ToString(),
                                reader["ClientSerial"].ToString(),
                                reader["ProfilePicturePath"].ToString()
                                );
                            userList.Add(user);
                        }
                    }
                }
            }
            return userList;
        }

        public bool UpdateAdmin(User user)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "UPDATE User SET Name = @Name, Address = @Address, Authority = @Authority, Tel = @Tel WHERE Email = @Email";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", user.Name);
                    command.Parameters.AddWithValue("@Address", user.Address);
                    command.Parameters.AddWithValue("@Authority", user.Authority);
                    command.Parameters.AddWithValue("@Tel", user.Tel);
                    command.Parameters.AddWithValue("@Email", user.Email);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }

        public bool UpdateUser(User user)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "UPDATE User SET Name = @Name, Password = @Password, Address = @Address, Authority = @Authority, Tel = @Tel WHERE Email = @Email";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", user.Name);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@Address", user.Address);
                    command.Parameters.AddWithValue("@Authority", user.Authority);
                    command.Parameters.AddWithValue("@Tel", user.Tel);
                    command.Parameters.AddWithValue("@Email", user.Email);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }
    }
}
