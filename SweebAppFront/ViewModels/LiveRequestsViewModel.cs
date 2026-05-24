using SweebAppAPIs.Models;
using SweebAppFront.Models;
using SweebAppFront.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SweebAppFront.ViewModels
{
    public class LiveRequestsViewModel
    {
        private readonly SignalRConnectionService _signalR;
        public ObservableCollection<ResponseProxy> LiveTraffic { get; set; } = new();

        public LiveRequestsViewModel(SignalRConnectionService signalR)
        {
            _signalR = signalR;
            _signalR.OnDataReceived += AddItem;
        }
        public void AddItem(ResponseProxy data)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                LiveTraffic.Add(data);
            });
        }
/*
        public LiveRequestsViewModel()
        {
            LiveTraffic = new ObservableCollection<LiveTrafficResponse>
            {
                new()
                {
                    Method = "GET",
                    Protocol = "HTTPS",
                    Url = "/",
                    Host = "www.google.com",
                    Port = 443,
                    IsThreat = false
                },
                new()
                {
                    Method = "GET",
                    Protocol = "HTTPS",
                    Url = "/favicon.ico",
                    Host = "www.google.com",
                    Port = 443,
                    IsThreat = false
                },
                new()
                {
                    Method = "GET",
                    Protocol = "HTTPS",
                    Url = "/api/users",
                    Host = "app.example.com",
                    Port = 443,
                    IsThreat = false
                },
                new()
                {
                    Method = "GET",
                    Protocol = "HTTP",
                    Url = "/tracking.js",
                    Host = "ads.tracker.com",
                    Port = 80,
                    IsThreat = false
                }
            };
        }
*/
    }
}
