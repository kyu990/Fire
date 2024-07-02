using MobleFinalServer.Models;
using MySql.Data.MySqlClient;
using Dapper;

namespace MobleFinalServer.Repository
{
    public class SensorRepository
    {
        private string connectionUrl;
        public SensorRepository(string connectionUrl)
        {
            this.connectionUrl = connectionUrl;
        }

        public bool AddSensorData(Sensor sensor)
        {
            using (var connection = new MySqlConnection(connectionUrl))
            {
                connection.Open();
                var sql = @"INSERT INTO Sensor(ClientSerial, Fire, Temp, Humidity, Gas, CDS) VALUES(@ClientSerial, @Fire, @Temp, @Humidity, @Gas, @CDS)";
                return connection.Execute(sql, sensor) > 0;
            }
        }

        public async Task<IEnumerable<Sensor>> GetSensorDataByTimeAsync(string clientSerial, DateTime minTime, DateTime maxTime)
        {
            using (var connection = new MySqlConnection(connectionUrl))
            {
                await connection.OpenAsync();
                var sql = @"SELECT * FROM Sensor WHERE ClientSerial = @ClientSerial AND Time BETWEEN @MinTime AND @MaxTime";

                var parameters = new { ClientSerial = clientSerial, MinTime = minTime, MaxTime = maxTime };

                var sensorData = (await connection.QueryAsync<Sensor>(sql, parameters));

                return sensorData;
            }
        }
    }
}
