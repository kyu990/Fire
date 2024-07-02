using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Rtsp.Sdp;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System;
using System.IO;
using MobleFinal.DAO;
using MobleFinal.DTO;

namespace MobleFinal._Service
{
    internal class SqliteHandler
    {
        private string connectionString;

        public SqliteHandler()
        {
            string databaseDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\bin"));
            string databasePath = Path.Combine(databaseDirectory, "sqlite.db");
            connectionString = $"Data Source={databasePath};Version=3;";

            if (!File.Exists(databasePath))
            {
                SQLiteConnection.CreateFile(databasePath);
            }

            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // 어드민 테이블 생성
                string adminTableQuery = @"CREATE TABLE IF NOT EXISTS admin (
                                           Id TEXT PRIMARY KEY,
                                           Password TEXT NOT NULL)";

                using (var command = new SQLiteCommand(adminTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                //비디오 테이블 생성
                string videoTableQuery = @"CREATE TABLE IF NOT EXISTS videos (
                                           Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                           filename TEXT NOT NULL,
                                           filepath TEXT NOT NULL,
                                           authKey BLOB NOT NULL,
                                           iv BLOB NOT NULL)";

                using (var command = new SQLiteCommand(videoTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
                // 기본 어드민 데이터 삽입
                InsertAdminIfNotExists("guna0407@naver.com", "1234");
                InsertAdminIfNotExists("admin", "1234");
                InsertAdminIfNotExists("guna", "1234");
            }
        }

        // 어드민 데이터 삽입 메서드
        private void InsertAdminIfNotExists(string id, string password)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string checkAdminExistQuery = "SELECT COUNT(*) FROM admin WHERE Id = @Id";
                using (var command = new SQLiteCommand(checkAdminExistQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    int count = Convert.ToInt32(command.ExecuteScalar());

                    if (count == 0)
                    {
                        string insertAdminQuery = "INSERT INTO admin (Id, Password) VALUES (@Id, @Password)";
                        using (var insertCommand = new SQLiteCommand(insertAdminQuery, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@Id", id);
                            insertCommand.Parameters.AddWithValue("@Password", password);
                            insertCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        // 어드민 확인
        public bool AdminCheck(string id, string password)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string selectQuery = @"SELECT COUNT(*) FROM admin 
                                       WHERE Id = @Id AND Password = @Password";

                using (var command = new SQLiteCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Password", password);

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    return count > 0;
                }
            }
        }


        public void InsertVideoData(string filename, string filepath, string password)
        {
            using (SQLiteConnection connection = new SQLiteConnection(this.connectionString))
            {
                connection.Open();

                string insertQuery = "INSERT INTO videos (filename, filepath, password) VALUES (@Filename, @FilePath, @Password)";
                using (SQLiteCommand command = new SQLiteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@Filename", filename);
                    command.Parameters.AddWithValue("@FilePath", filepath);
                    command.Parameters.AddWithValue("@Password", password);

                    command.ExecuteNonQuery();
                }
            }
        }

        public Video GetVideo(string filename)
        {
            Video video = null;
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string selectQuery = @"SELECT * FROM videos 
                                   WHERE LOWER(filename) = LOWER(@Filename)";

                    using (var command = new SQLiteCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Filename", filename.ToLower());

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                video = new Video
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    FileName = reader["filename"].ToString(),
                                    FilePath = reader["filepath"].ToString(),
                                    Password = reader["password"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // 오류 처리 코드 작성
                Console.WriteLine("Error fetching video: " + ex.Message);
            }

            return video;
        }
    }
}
