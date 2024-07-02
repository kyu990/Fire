using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MobleFinal._NotUse.Test
{
    internal class PortTest
    {
        private IPAddress iPAddress;
        private readonly int _port;

        public PortTest(int port)
        {
            iPAddress = IPAddress.Any;  // IPAddress 초기화
            _port = port;
        }

        public void StartTcpServer()
        {
            TcpListener listener = new TcpListener(iPAddress, _port);
            listener.Start();

            Console.WriteLine("서버가 시작되었습니다. 클라이언트 연결을 대기합니다...");

            TcpClient client = listener.AcceptTcpClient();
            Console.WriteLine("클라이언트가 연결되었습니다.");

            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine($"클라이언트로부터 받은 데이터: {dataReceived}");

            // 클라이언트에게 응답 전송
            string responseData = "서버에서 클라이언트로의 응답: 포트 포워딩 테스트 성공!";
            byte[] responseBytes = Encoding.UTF8.GetBytes(responseData);
            stream.Write(responseBytes, 0, responseBytes.Length);

            client.Close();
            Console.WriteLine("클라이언트와 연결을 종료합니다.");
        }
    }
}
