using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LibVLCSharp.Shared;

namespace MobleFinal._Service
{
    internal class HlsService
    {
        private VideoWriterService videoWriterService;
        private readonly IPAddress internalIpAddress;

        public HlsService()
        {
            videoWriterService = new VideoWriterService();
            internalIpAddress = GetLocalIPAddress();
        }

        private IPAddress GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    Console.WriteLine(ip);
                    return ip;
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        public async Task VlcControllerAsync()
        {
            string outputDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output_directory");

            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            var cts = new CancellationTokenSource();

            // 실시간 캡처 및 HLS 스트리밍 시작
            videoWriterService.StartCaptureAndStreaming(outputDirectory, 1280, 720, 15.0);

            // TCP 서버 시작
            TcpListener tcpListener = new TcpListener(IPAddress.Any, Info.HlsPort);
            try
            {
                tcpListener.Start();
                Console.WriteLine($"TCP server started at port {Info.HlsPort}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error starting TCP server: {ex.Message}");
                return;
            }

            // 비동기 요청 처리
            Task.Run(async () => await HandleRequests(tcpListener, outputDirectory, cts.Token));

            // ESC 키 입력을 감지하여 서버 종료
            Console.WriteLine("Press ESC to stop the server...");
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(intercept: true);
                    if (key.Key == ConsoleKey.Escape)
                    {
                        break;
                    }
                }
                await Task.Delay(100); // CPU 사용률을 낮추기 위해 약간의 대기
            }

            // 캡처 및 스트리밍 중지
            videoWriterService.StopCaptureAndStreaming();

            // TCP 서버 종료
            cts.Cancel();
            tcpListener.Stop();
            Console.WriteLine("TCP server stopped.");
        }

        private async Task HandleRequests(TcpListener listener, string outputDirectory, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var client = await listener.AcceptTcpClientAsync();
                _ = Task.Run(async () =>
                {
                    using (client)
                    {
                        using (NetworkStream networkStream = client.GetStream())
                        {
                            try
                            {
                                // Read request
                                var reader = new StreamReader(networkStream);
                                var requestLine = await reader.ReadLineAsync();

                                if (requestLine != null && requestLine.StartsWith("GET"))
                                {
                                    var parts = requestLine.Split(' ');
                                    if (parts.Length > 1)
                                    {
                                        var requestedUrl = parts[1].TrimStart('/');
                                        string relativePath = requestedUrl.Replace("output_directory/", string.Empty);
                                        string filePath = Path.Combine(outputDirectory, relativePath);

                                        Console.WriteLine($"Request for {requestedUrl} mapped to {filePath}");

                                        using (var writer = new StreamWriter(networkStream) { AutoFlush = true })
                                        {
                                            if (File.Exists(filePath))
                                            {
                                                var buffer = File.ReadAllBytes(filePath);
                                                writer.WriteLine("HTTP/1.1 200 OK");

                                                // CORS 헤더 추가
                                                writer.WriteLine($"Access-Control-Allow-Origin: *");
                                                writer.WriteLine("X-Content-Type-Options: nosniff");

                                                writer.WriteLine($"Content-Length: {buffer.Length}");
                                                writer.WriteLine($"Content-Type: {GetContentType(filePath)}");
                                                writer.WriteLine();
                                                await networkStream.WriteAsync(buffer, 0, buffer.Length);
                                                Console.WriteLine($"Served file: {filePath}");
                                            }
                                            else
                                            {
                                                writer.WriteLine("HTTP/1.1 404 Not Found");
                                                writer.WriteLine();
                                                Console.WriteLine($"File not found: {filePath}");
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Exception: {ex.Message}");
                            }
                        }
                    }
                });
            }
        }

        private string GetContentType(string filePath)
        {
            return Path.GetExtension(filePath) switch
            {
                ".m3u8" => "application/vnd.apple.mpegurl",
                ".ts" => "video/MP2T",
                _ => "application/octet-stream",
            };
        }
    }
}

