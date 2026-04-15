using Microsoft.AspNetCore.SignalR;
using SweebAppAPIs.Models;
using SweebAppAPIs.Services.Interfaces;

namespace SweebAppAPIs.Services
{
    public class SignalRHub : Hub
    {
        public async Task SendProxyTrafficAsync(LiveTrafficResponse trafficResponse, string userId)
        {
            await Clients.User(userId).SendAsync("RecieveProxyTraffic", trafficResponse);
        }
    }
}
