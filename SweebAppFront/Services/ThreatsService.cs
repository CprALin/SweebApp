using Microsoft.AspNetCore.SignalR.Client;
using SweebAppAPIs.Enum;
using SweebAppAPIs.Models;
using SweebAppFront.Models;
using SweebAppFront.Services.Interfaces;
using SweebAppFront.ViewModels;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;

namespace SweebAppFront.Services
{
    public class ThreatsService : BaseViewModel, IThreatsService
    {
        private ObservableCollection<ThreatEvent> _threatEvents = new();
        private readonly SignalRConnectionService _signalRConnectionService;
        private HttpClient _client = new HttpClient();
        private JsonSerializerOptions _options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, WriteIndented = true };
        private Uri uriThreats = new Uri("https://localhost:7832/api/v1/Threats/all");
        private Uri uriUpdateThreat = new Uri("https://localhost:7832/api/v1/Threats/update/");
        public ObservableCollection<ThreatEvent> ThreatEvents { 
            get => _threatEvents;
            set 
            { 
                if(_threatEvents != value)
                {
                    _threatEvents = value;
                    OnPropertyChanged();
                }   
            } 
        }

        public ThreatsService(SignalRConnectionService signalRConnectionService)
        {
            _signalRConnectionService = signalRConnectionService;

            _signalRConnectionService.OnThreatEventReceived += threat =>
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    ThreatEvents.Add(threat);
                });
            };

            _signalRConnectionService.OnNewThreatEvent += (threatId, status) =>
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    var threat = ThreatEvents.FirstOrDefault(t => t.Id == threatId);
                    if (threat != null)
                    {
                        threat.ActionTaken = status;
                        var index = ThreatEvents.IndexOf(threat);
                        ThreatEvents.RemoveAt(index);
                        ThreatEvents.Insert(index, threat);
                    }
                });
            };
        }

        public async Task LoadThreats()
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uriThreats);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Failed to load threats: {response.StatusCode}");
                    return;
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<List<ThreatEvent>>>(responseContent, _options);

                if(apiResponse == null || apiResponse.Data == null)
                {
                    return;
                }

                ThreatEvents.Clear();

                foreach (ThreatEvent threat in apiResponse.Data)
                {
                    ThreatEvents.Add(threat);
                }
            } catch (Exception ex)
            {
                Console.WriteLine($"Failed to load threats: {ex.Message}");
                return;
            }
        }

        public async Task UpdateAction(int threatId, ThreatStatus status)
        {
            try
            {
                var json = JsonSerializer.Serialize(status, _options);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PatchAsync(new Uri(uriUpdateThreat, threatId.ToString()), content);

                if (!response.IsSuccessStatusCode)
                {
                    return;
                }

                var threat = ThreatEvents.FirstOrDefault(t => t.Id == threatId);
                if (threat != null)
                {
                    threat.ActionTaken = status;
                    var index = ThreatEvents.IndexOf(threat);
                    ThreatEvents.RemoveAt(index);
                    ThreatEvents.Insert(index, threat);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to update threat action: {ex.Message}");
                return;
            }
        }

    }
}
