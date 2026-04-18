using SweebAppAPIs.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SweebAppFront.ViewModels
{
    public class LiveRequestsViewModel
    {
        public ObservableCollection<LiveTrafficResponse> LiveTraffic { get; set; }

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
    }
}
