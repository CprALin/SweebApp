using WorkerSweebApp.Services.Interfaces;

namespace WorkerSweebApp.Workers
{
    public class ProxyWorker(IProxyService proxy) : BackgroundService
    {
       private readonly IProxyService _proxyService = proxy;

       protected override async Task ExecuteAsync(CancellationToken cancellationToken)
       {
            await _proxyService.StartAsync(cancellationToken);

            await Task.Delay(Timeout.Infinite, cancellationToken);
        }  
    }
}
