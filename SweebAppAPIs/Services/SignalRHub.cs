using Microsoft.AspNetCore.SignalR;
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
    }
}
