using MySql.Data.MySqlClient;
using MobleFinalServer.Models;
using Dapper;
using Microsoft.AspNetCore.Server.Kestrel.Core.Features;
namespace MobleFinalServer.Repository
{
    public class UserRepository
    {
        private string connectionUrl;
        public UserRepository(string connectionUrl)
        {
            this.connectionUrl = connectionUrl;
        }

        /// <summary>
        /// select문은 DB에서 조회하고 반환하는데 시간이 걸릴 수 있으므로 비동기적으로 처리하는 것이 성능에 좋음
        /// 그외 delete, update, insert문은 대부분의 경우 간단하게 끝나고 DB를 수정하는 행위이기 때문에 동시에 접근하면 안됨
        /// 즉 동기적으로 처리하는 것이 좋음
        /// </summary>


        //회원가입
        public bool AddUser(User user)
        {
            using (var connection = new MySqlConnection(connectionUrl))
            {
                connection.Open();
                
                user.IsEmailConfirmed = false;

                var sql = @"INSERT INTO User (Email, Password, Name, Address, Tel, ClientSerial, IsEmailConfirmed, EmailConfirmationToken, ProfilePicturePath)
                    VALUES (@Email, @Password, @Name, @Address, @Tel, @ClientSerial, @IsEmailConfirmed, @EmailConfirmationToken, @ProfilePicturePath)";
                return connection.Execute(sql, user) > 0;
            }
        }

        //회원 정보 수정
        public bool UpdateUser(User user)
        {
            using (var connection = new MySqlConnection(connectionUrl))
            {
                connection.Open();
                var sql = @"update User set Password = @Password, Name = @Name, Address = @Address, Tel = @Tel, ClientSerial = @ClientSerial where Email = @Email";
                return connection.Execute(sql, user) > 0;
            }
        }

        //회원 정보 수정
        public bool UpdateUser(EditUser user)
        {
            using (var connection = new MySqlConnection(connectionUrl))
            {
                connection.Open();
                var sql = @"update User set Authority = @Authority, Address = @Address, Tel = @Tel where Email = @Email";
                return connection.Execute(sql, user) > 0;
            }
        }

        //회원 이메일 인증 정보 수정
        public bool UpdateUser(string email, bool state)
        {
            using (var connection = new MySqlConnection(connectionUrl))
            {
                connection.Open();
                var sql = @"update User set IsEmailConfirmed = @IsEmailConfirmed where Email = @Email";
                var parameters = new { IsEmailConfirmed = state, Email = email };
                int affectedRows = connection.Execute(sql, parameters);
                return affectedRows > 0;
            }
        }
        //회원 탈퇴
        public bool DeleteUser(string email)
        {
            using (var connection = new MySqlConnection(connectionUrl))
            {
                connection.Open();
                var sql = @"delete from User where Email = @Email";
                return connection.Execute(sql, new { email }) > 0;
            }
        }

        //로그인
        public async Task<bool> LoginAsync(Login user)
        {
            using (var connection = new MySqlConnection(connectionUrl))
            {
                await connection.OpenAsync();
                var sql = @"SELECT * FROM User WHERE Email = @Email AND Password = @Password";
                var result = await connection.QueryAsync<User>(sql, user);

                var userList = result.AsList();
                if (userList.Count > 0)
                {
                    return userList[0].IsEmailConfirmed == true;
                }
                else
                {
                    return false;
                }
            }
        }

        //이메일 중복 확인
        public async Task<bool> CheckEmailDuplicateAsync(string email) //있으면 true, 없으면 false
        {
            using (var connection = new MySqlConnection(connectionUrl))
            {
                await connection.OpenAsync();
                var sql = @"SELECT COUNT(*) FROM User WHERE Email = @Email";
                int count = await connection.QuerySingleAsync<int>(sql, new { email });
                return count > 0;
            }
        }

        //이메일로 유저 정보 조회 및 반환
        public async Task<User> GetUserByEmailAsync(string email)
        {
            using (var connection = new MySqlConnection(connectionUrl))
            {
                await connection.OpenAsync();
                var sql = @"SELECT * FROM User WHERE Email = @Email";
                return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Email = email });
            }
        }

		//이메일로 유저 정보 조회 및 반환
		public async Task<string> GetProfilePathByEmailAsync(string email)
		{
			using (var connection = new MySqlConnection(connectionUrl))
			{
				await connection.OpenAsync();
				var sql = @"SELECT ProfilePicturePath FROM User WHERE Email = @Email";
				return await connection.QueryFirstOrDefaultAsync<string>(sql, new { Email = email });
			}
		}

		public async Task<EditUser> GetEUserByEmailAsync(string email)
        {
            using (var connection = new MySqlConnection(connectionUrl))
            {
                await connection.OpenAsync();
                var sql = @"SELECT * FROM User WHERE Email = @Email";
                return await connection.QueryFirstOrDefaultAsync<EditUser>(sql, new { Email = email });
            }
        }

        public async Task<IEnumerable<User>> GetUserBySerialAsync(string clientSerial, int pageSize, int pageNumber)
        {
            using (var connection = new MySqlConnection(connectionUrl))
            {
                await connection.OpenAsync();
                var sql = @"SELECT * FROM User WHERE ClientSerial = @ClientSerial ORDER BY Name LIMIT @Offset, @Limit";
                var parameters = new { clientSerial, Offset = (pageNumber - 1) * pageSize, Limit = pageSize };
                var userData = await connection.QueryAsync<User>(sql, parameters);
                return userData;
            }
        }
        public async Task<int> GetUserCountAsync(string clientSerial)
        {
            using (var connection = new MySqlConnection(connectionUrl))
            {
                await connection.OpenAsync();
                var sql = "SELECT COUNT(*) FROM User WHERE ClientSerial = @ClientSerial";
                var count = await connection.ExecuteScalarAsync<int>(sql, new { clientSerial });
                return count;
            }
        }
    }
}
