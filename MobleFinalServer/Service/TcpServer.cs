using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using MobleFinalServer.Controllers;
using MobleFinalServer.Models;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace MobleFinalServer.Service
{
    public class TcpServer
    {
        private readonly int _port;
		private readonly IHubContext<SensorHub> _hubContext;
        private readonly ILogger<TcpServer> _logger;
        // tcp 통신으로 수신한 데이터 중 ClientSerial을 기준으로 딕셔너리에서 해당하는 큐를 찾고
        // 데이터 저장, ClientSerial 별로 signalr을 이용해 클라이언트로 센서 데이터 전달
        private readonly ConcurrentDictionary<string, ConcurrentQueue<Sensor>> sensorValueDict = new ConcurrentDictionary<string, ConcurrentQueue<Sensor>>();
        public TcpServer(int port, IHubContext<SensorHub> hubContext, ILogger<TcpServer> logger)
        {
            _port = port;
            _hubContext = hubContext;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            TcpListener listener = new TcpListener(IPAddress.Any, _port);
            listener.Start();
            Console.WriteLine($"TCP Server started on port {_port}");

            while (!cancellationToken.IsCancellationRequested)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                _ = Task.Run(() => HandleClientAsync(client), cancellationToken);
            }
            listener.Stop();
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            using (client)
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[1024];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string jsonString = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"{DateTime.Now} 수신 데이터 : {jsonString}");

                byte[] data = Encoding.UTF8.GetBytes("ASP.NET : Good!");

                stream.Write(data);

                try
                {
                    Sensor sensorData = JsonConvert.DeserializeObject<Sensor>(jsonString);
                    sensorData.Id = 0;
                    sensorData.Time = DateTime.Now;
                    sensorData.ClientSerial = "24y5m29d";

                    if (sensorData != null)
                    {
                        var sensorQueue = sensorValueDict.GetOrAdd(sensorData.ClientSerial, new ConcurrentQueue<Sensor>());
                        sensorQueue.Enqueue(sensorData);
                        await _hubContext.Clients.Group(sensorData.ClientSerial).SendAsync("RT_SensorData", sensorData);
                    }
                    else
                    {
                        _logger.LogWarning("Failed to deserialize JSON to Sensor object.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while handling client data.");
                }
            }
        }
    }
}
