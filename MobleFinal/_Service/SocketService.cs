using MobleFinal.DTO;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MobleFinal._Service
{
    internal class SocketService
    {
        private TcpListener server;
        private Thread arduinoThread;
        private bool arduIsRunning;
        private TcpClient arduinoClient;
        private NetworkStream arduinoStream;
        private readonly IPAddress ip;

        public SocketService()
        {
            ip = IPAddress.Any; // 환경 별 ip 할당
            server = new TcpListener(ip, Info.ArduPort);

            arduinoThread = new Thread(new ThreadStart(RunServer)); // RunServer가 클라이언트 연결 수락받는 메서드
            arduIsRunning = false;
            arduinoClient = null;
        }

        public void Arduino()
        {
            arduIsRunning = true;
            arduinoThread.Start();
        }

        public void StopServer()
        {
            arduIsRunning = false;
            server.Stop();
            arduinoThread.Join();
        }

        private void RunServer()
        {
            StartServer();

            while (arduIsRunning)
            {
                try
                {
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Client connected.");

                    if (arduinoClient == null)
                    {
                        arduinoClient = client;
                        arduinoStream = arduinoClient.GetStream();
                    }

                    Thread clientThread = new Thread(() => HandleClient(client));
                    clientThread.Start();
                }
                catch (SocketException ex)
                {
                    Console.WriteLine("SocketException while accepting client: " + ex.Message);
                    TryRestartServer();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception while accepting client: " + ex.Message);
                }
            }
        }

        private void StartServer()
        {
            try
            {
                server.Start();
                Console.WriteLine("Server started, waiting for clients...");
            }
            catch (SocketException ex)
            {
                Console.WriteLine("SocketException: " + ex.Message);
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return;
            }
        }

        private void TryRestartServer()
        {
            int retryDelay = 3000; // 3초

            while (!arduIsRunning)
            {
                try
                {
                    server = new TcpListener(ip, Info.ArduPort); // 서버 객체를 다시 생성
                    StartServer();
                    return;
                }
                catch (Exception)
                {
                    Thread.Sleep(retryDelay);
                }
            }

            if (!arduIsRunning)
            {
                Console.WriteLine("Failed to restart the server after multiple attempts.");
            }
        }

        private async void HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead;

            try
            {
                // 데이터 수신
                bytesRead = stream.Read(buffer, 0, buffer.Length);
                string jsonStringData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine("수신 데이터 : " + jsonStringData);

                // 수신된 JSON 데이터를 파싱하고 콘솔에 출력
                Sensor sensorData = JsonConvert.DeserializeObject<Sensor>(jsonStringData);

                // 출력을 sensorData.Fire 등으로 하면 결과값 나옴.
                Console.WriteLine("파싱 데이터 : " + sensorData);

                // FireDetect 폼으로 파싱 데이터 전달
                FireDetectForm.SetSensorData(sensorData);

                // 데이터를 다른 서버로 전송
                await SensorDataSend(jsonStringData);

                // 데이터 처리 및 응답 생성  
                string response = FireDetectForm.pumpController.ToString();
                byte[] responseData = Encoding.UTF8.GetBytes(response + "\n");
                stream.Write(responseData, 0, responseData.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error handling client: " + ex.Message);
            }
            finally
            {
                stream.Close();
                client.Close();
            }
        }

        private async Task SensorDataSend(string data) // json 문자열 데이터
        {
            TcpClient client = new TcpClient();
            IPEndPoint serverEP = new IPEndPoint(IPAddress.Parse("192.168.0.88"), Info.TcpPort); // 서버 IP와 포트 설정

            try
            {
                await client.ConnectAsync(serverEP.Address, serverEP.Port);
                NetworkStream stream = client.GetStream();

                byte[] dataToSend = Encoding.UTF8.GetBytes(data);
                await stream.WriteAsync(dataToSend, 0, dataToSend.Length);
                Console.WriteLine("서버로 데이터 전송.");

                stream.Close();
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("데이터 전송 실패 : " + ex.Message);
            }
        }
    }
}
