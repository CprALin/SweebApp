using Microsoft.AspNetCore.SignalR.Client;
using WorkerSweebApp.Models;
using WorkerSweebApp.Services.Interfaces;

namespace WorkerSweebApp.Services
{
    public class SignalRService : ISignalRService
    {
        private readonly HubConnection _connection;

        public SignalRService()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:4238/signalRHub")
                .WithAutomaticReconnect()
                .Build();
        }

        public async Task StartAsync()
        {
            await _connection.StartAsync();
            Console.WriteLine($"Connection id : {_connection.ConnectionId}");
        }

        public async Task StopAsync()
        {
            await _connection.StopAsync();
        }

        public async Task SendProxyTrafficAsync(ResponseProxy response)
        {
            var userId = "testUserId";
            await _connection.InvokeAsync("SendProxyTrafficAsync", response, userId); 
        }
    }
}
