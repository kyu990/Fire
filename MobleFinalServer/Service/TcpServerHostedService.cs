using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace MobleFinalServer.Service
{
    public class TcpServerHostedService : IHostedService
    {
        private readonly TcpServer _tcpServer;
        private Task _executingTask;
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();

        public TcpServerHostedService(TcpServer tcpServer)
        {
            _tcpServer = tcpServer;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _executingTask = _tcpServer.StartAsync(_cts.Token);
            if (_executingTask.IsCompleted)
            {
                return _executingTask;
            }
            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_executingTask == null)
            {
                return;
            }

            _cts.Cancel();

            await Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite, cancellationToken));

            cancellationToken.ThrowIfCancellationRequested();
        }
    }
}
