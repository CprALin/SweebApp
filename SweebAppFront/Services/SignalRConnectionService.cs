using Microsoft.AspNetCore.SignalR.Client;
using SweebAppAPIs.Enum;
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
        public event Action<Alerts>? OnAlertReceived;
        public event Action<int>? OnAlertDeleted;
        public event Action<ThreatEvent>? OnThreatEventReceived;
        public event Action<int, ThreatStatus>? OnNewThreatEvent;
        public event Action? OnAllAlertsDelete;

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

            _connection.On<Alerts>("ReceiveAlert", alert =>
            {
                OnAlertReceived?.Invoke(alert);
            });

            _connection.On<int>("AlertDeleted", alertId =>
            {
                OnAlertDeleted?.Invoke(alertId);
            });

            _connection.On("ClearAllAlerts", () =>
            {
                OnAllAlertsDelete?.Invoke();
            });

            _connection.On<ThreatEvent>("Threat", threat =>
            {
                OnThreatEventReceived?.Invoke(threat);
            });

            _connection.On<int, ThreatStatus>("NewThreatStatus", (threatId, status) =>
            {
                OnNewThreatEvent?.Invoke(threatId, status);
            });

            await _connection.StartAsync();
        }
    }
}
