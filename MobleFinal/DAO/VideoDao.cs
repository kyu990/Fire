using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobleFinal.DAO
{
    internal class VideoDao
    {
        private string server = "bearhouseserver.iptime.org";
        private string port = "13001";
        private string database = "FireDetectionFinal";
        private string id = "root";
        private string pw = "1q2w3e4r@";
        private string connectionString;

        public VideoDao()
        {
            connectionString = $"Server={server};Port={port};Database={database};Uid={id};Pwd={pw};";
        }

        // 센서 데이터 저장
        public bool SaveSensor(string ClientSerial, bool Fire, double Temp, double Humidity, double Gas, double CDS, double Dust)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Sensor (ClientSerial, Fire, Temp, Humidity, Gas, CDS, Dust) VALUES (@ClientSerial, @Fire, @Temp, @Humidity, @Gas, @CDS, @Dust)";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ClientSerial", ClientSerial);
                    command.Parameters.AddWithValue("@Fire", Fire);
                    command.Parameters.AddWithValue("@Temp", Temp);
                    command.Parameters.AddWithValue("@Humidity", Humidity);
                    command.Parameters.AddWithValue("@Gas", Gas);
                    command.Parameters.AddWithValue("@CDS", CDS);
                    command.Parameters.AddWithValue("@Dust", Dust);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }
    }
}
