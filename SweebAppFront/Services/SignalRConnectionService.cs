using Microsoft.AspNetCore.SignalR.Client;
using SweebAppFront.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SweebAppFront.Services
{
    public class SignalRConnectionService
    {
        private HubConnection? _connection;
        public event Action<ResponseProxy>? OnDataReceived;

        public async Task StratAsync()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7832/signalRHub")
                .WithAutomaticReconnect()
                .Build();

            _connection.On<ResponseProxy>("ReceiveProxyTraffic", data =>
            {
                OnDataReceived?.Invoke(data);
            });

            await _connection.StartAsync();
        }
    }
}
