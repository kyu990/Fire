using System;
using System.Linq;
using System.Threading.Tasks;
using MobleFinalServer;
using Mono.Nat;

namespace MobleFinalServer.Service
{
    internal class PortForwarding
    {
        static readonly int[] internalPorts = { Program.HttpsInternalPort, Program.HlsPort };
        static readonly int[] externalPorts = { Program.HttpsExternalPort, Program.HlsPort };

        static public void StartPortForwarding()
        {
            // NAT 장치 검색 시작
            NatUtility.DeviceFound += DeviceFound;
            NatUtility.StartDiscovery();

            Console.WriteLine("NAT 장치 검색 중...");
        }

        static private void DeviceFound(object sender, DeviceEventArgs args)
        {
            INatDevice device = args.Device;
            Console.WriteLine($"NAT 장치 발견됨: {device.DeviceEndpoint}");

            try
            {
                // 기존 포트 매핑 확인 및 설정
                var mappings = device.GetAllMappings();
                foreach (var mapping in mappings)
                {
                    Console.WriteLine($"기존 매핑: {mapping}");
                }
                for(int i=0;i<internalPorts.Length;i++)
                {
                    // 기존 매핑이 없을 경우에만 새로운 포트 매핑 추가
                    if (!mappings.Any(m => m.PublicPort == externalPorts[i] && m.Protocol == Protocol.Tcp))
                    {
                        var mapping = new Mapping(Protocol.Tcp, internalPorts[i], externalPorts[i]);
                        device.CreatePortMap(mapping);

                        Console.WriteLine($"포트 포워딩 설정 완료: {mapping.PrivatePort} -> {mapping.PublicPort}");
                    }
                    else
                    {
                        Console.WriteLine($"포트 {externalPorts[i]}에 이미 매핑이 존재합니다.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"포트 포워딩 설정 중 오류 발생: {ex.Message}");
            }
            finally
            {
                // NAT 장치 검색 중지
                NatUtility.StopDiscovery();
            }
        }

        static public async Task StopPortForwardingAsync()
        {
            // NAT 장치 검색 시작
            NatUtility.DeviceFound += DeviceDelete;
            NatUtility.StartDiscovery();

            Console.WriteLine("NAT 장치 검색 중...");

            // 비동기 작업이 완료될 때까지 대기
            await Task.Delay(3000); // 적절한 대기 시간 설정

            // NAT 장치 검색 중지
            NatUtility.StopDiscovery();
        }

        static private void DeviceDelete(object sender, DeviceEventArgs args)
        {
            INatDevice device = args.Device;
            Console.WriteLine($"NAT 장치 발견됨: {device.DeviceEndpoint}");

            try
            {
                var mappings = device.GetAllMappings();
                foreach (var mapping in mappings)
                {
                    for(int i=0;i< internalPorts.Length;i++)
                    {
                        if (mapping.PrivatePort == internalPorts[i] && mapping.PublicPort == externalPorts[i])
                        {
                            device.DeletePortMap(mapping);
                            Console.WriteLine($"포트 {externalPorts[i]} 삭제 완료");
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"포트 제거 중 오류 발생: {ex.Message}");
            }
        }
    }
}

