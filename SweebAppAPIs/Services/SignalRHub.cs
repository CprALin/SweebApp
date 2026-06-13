using Microsoft.AspNetCore.SignalR;
using SweebAppAPIs.Enum;
using SweebAppAPIs.Models;
using WorkerSweebApp.Models;

namespace SweebAppAPIs.Services
{
    public class SignalRHub : Hub
    {
        public async Task SendProxyTrafficAsync(ResponseProxy response)
        {
            Console.WriteLine($"[API RECEIVED] {response.Url}");

            await Clients.All.SendAsync("ReceiveProxyTraffic", response);
        }

        public async Task ClearAllAsync()
        {
            await Clients.All.SendAsync("ClearAllAlerts");
        }

        public async Task SendAlertsAsync(Alert alert)
        {
            await Clients.All.SendAsync("ReceiveAlert", alert);
        }

        public async Task DeleteAlertsAsync(int id)
        {
            await Clients.All.SendAsync("AlertDeleted", id);
        }

        public async Task ThreatsAsync(ThreatEvent threat)
        {
            await Clients.All.SendAsync("Threat", threat);
        }

        public async Task NewThreatAsync(int id, ThreatStatus status)
        {
            await Clients.All.SendAsync("NewThreatStatus", status);
        }
    }
}
