using Titanium.Web.Proxy.EventArguments;

namespace WorkerSweebApp.Services.Interfaces
{
    public interface IProxyService
    {
        Task StartAsync(CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);
        void RemoveSystemProxy();
    }
}
