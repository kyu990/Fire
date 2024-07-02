using System;
using System.Linq;
using System.Threading.Tasks;
using MobleFinal._Service;
using Mono.Nat;

namespace MobleFinal._NotUse
{
    internal class PortForwarding
    {
        static readonly int internalPort = Info.HlsPort;
        static readonly int externalPort = Info.HlsPort;

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

                // 기존 매핑이 없을 경우에만 새로운 포트 매핑 추가
                if (!mappings.Any(m => m.PublicPort == externalPort && m.Protocol == Protocol.Tcp))
                {
                    var mapping = new Mapping(Protocol.Tcp, internalPort, externalPort);
                    device.CreatePortMap(mapping);

                    Console.WriteLine($"포트 포워딩 설정 완료: {mapping.PrivatePort} -> {mapping.PublicPort}");
                }
                else
                {
                    Console.WriteLine($"포트 {externalPort}에 이미 매핑이 존재합니다.");
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
                    if (mapping.PrivatePort == internalPort && mapping.PublicPort == externalPort)
                    {
                        device.DeletePortMap(mapping);
                        Console.WriteLine($"포트 {externalPort} 삭제 완료");
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
