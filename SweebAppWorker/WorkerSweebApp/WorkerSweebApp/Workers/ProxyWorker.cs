using WorkerSweebApp.Services.Interfaces;

namespace WorkerSweebApp.Workers
{
    public class ProxyWorker(IProxyService proxy, ISignalRService signalRService) : BackgroundService
    {
       private readonly IProxyService _proxyService = proxy;
       private readonly ISignalRService _signalRService = signalRService;

       protected override async Task ExecuteAsync(CancellationToken cancellationToken)
       {
            await _signalRService.StartAsync();
            await _proxyService.StartAsync(cancellationToken);
            try
            {
                await Task.Delay(Timeout.Infinite, cancellationToken);
            }
            catch (TaskCanceledException){}


            await _proxyService.StopAsync(cancellationToken);
            await _signalRService.StopAsync();
        }  
    }
}
