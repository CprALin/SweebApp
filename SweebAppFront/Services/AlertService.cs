using SweebAppFront.Models;
using SweebAppFront.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;

namespace SweebAppFront.Services
{
    public class AlertService : IAlertService
    {
        public ObservableCollection<Alerts> Alerts { get; set; } = new();
        private readonly SignalRConnectionService _signalRConnectionService;

        HttpClient _client = new HttpClient();
        JsonSerializerOptions _options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, WriteIndented = true };
        Uri uriAlerts = new Uri("https://localhost:7832/api/v1/Alerts/all");

        public AlertService(SignalRConnectionService signalRConnectionService)
        {
            _signalRConnectionService = signalRConnectionService;


            _signalRConnectionService.OnAlertReceived += alert =>
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Alerts.Add(alert);
                });
            };

            _signalRConnectionService.OnAllAlertsDelete += () =>
            {
                Alerts.Clear();
            };

            _signalRConnectionService.OnAlertDeleted += id =>
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    var alert = Alerts.FirstOrDefault(a => a.Id == id);
                    if (alert != null)
                        Alerts.Remove(alert);
                });
            };

        }

        public async Task GetAlerts()
        {
            try
            {
                var response = await _client.GetAsync(uriAlerts);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Failed to load alerts: {response.StatusCode}");
                    return;
                }

                var json = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<List<Alerts>>>(json, _options);

                if(apiResponse == null || apiResponse.Data == null)
                {
                    return;
                }

                Alerts.Clear();

                foreach (var alert in apiResponse.Data)
                {
                    Alerts.Add(alert);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load alerts: {ex.Message}");
            }
        }

        public async Task ClearAllAlerts()
        {
            try
            {
                var response = await _client.DeleteAsync(uriAlerts);
                if (response.IsSuccessStatusCode)
                {
                    Alerts.Clear();
                }
                else
                {
                    Console.WriteLine($"Failed to clear alerts: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to clear alerts: {ex.Message}");
            }
        }
        public async Task ClearAlertById(int id)
        {
            try
            {
                var response = await _client.DeleteAsync(new Uri($"https://localhost:7832/api/v1/Alerts/{id}"));
                if (response.IsSuccessStatusCode)
                {
                    var alert = Alerts.FirstOrDefault(a => a.Id == id);
                    if (alert != null)
                    {
                        Alerts.Remove(alert);
                    }
                }
                else
                {
                    Console.WriteLine($"Failed to clear alert {id}: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to clear alert {id}: {ex.Message}");
            }
        }
    }
}
