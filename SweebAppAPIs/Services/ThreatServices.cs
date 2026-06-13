using SweebAppAPIs.Data.Repositories.Interfaces;
using SweebAppAPIs.Enum;
using SweebAppAPIs.Models.Responses;
using SweebAppAPIs.Services.Interfaces;
using SweebAppAPIs.Models;
using Microsoft.AspNetCore.SignalR;

namespace SweebAppAPIs.Services
{
    public class ThreatServices(IThreatRepository repo, IAlertRepository alertRepo, IHubContext<SignalRHub> hubContext) : IThreatServices
    {
        private readonly IThreatRepository _repo = repo;
        private readonly IAlertRepository _alertRepo = alertRepo;
        private readonly IHubContext<SignalRHub> _hubContext = hubContext;

        public async Task<Response> CreateThreatForDevice(int deviceId, string URL, string Protocol, string verdict, ThreatStatus status, double score, string category)
        {
            if (deviceId == 0 || string.IsNullOrEmpty(URL) || string.IsNullOrEmpty(Protocol) || string.IsNullOrEmpty(verdict) || string.IsNullOrEmpty(category) || score < 0)
            {
                return new Response
                {
                    Status = "Error",
                    Message = "Invalid input parameters.",
                };
            }

            var threat = new ThreatEvent
            {
                URL = URL,
                Protocol = Protocol,
                Verdict = verdict,
                Score = score,
                Category = category,
                ActionTaken = status
            };

            var result = await _repo.CreateThreatAsync(threat);

            if (result == null)
            {
                return new Response
                {
                    Status = "Error",
                    Message = "Failed to create threat event.",
                };
            }
            await _hubContext.Clients.All.SendAsync("Threat", threat);
            return new Response
            {
                Status = "Success",
                Message = "Threat event created successfully.",
                Data = result
            };
        }

        public async Task<Response> UpdateThreatStatus(int threatId, ThreatStatus newStatus)
        {
            if (threatId == 0)
            {
                return new Response
                {
                    Status = "Error",
                    Message = "Invalid threat ID.",
                };
            }

            await _repo.UpdateThreatAsync(threatId, newStatus);
            await _hubContext.Clients.All.SendAsync("NewThreatStatus",threatId,newStatus);

            if (newStatus == ThreatStatus.Allowed)
            {
                var threat = await _repo.GetThreatByIdAsync(threatId);
                var message = threat == null
                    ? "Warning: this threat was allowed manually. The user is responsible for any potential risk."
                    : $"Warning: {threat.URL} was allowed manually. The user is responsible for any potential threat.";

                var alert = new Alert
                {
                    ThreatEventId = threatId,
                    Message = message,
                    Severity = AlertSeverity.Warning
                };

                await _alertRepo.CreateAlertAsync(alert);
                await _hubContext.Clients.All.SendAsync("ReceiveAlert", alert);
            }

            return new Response
            {
                Status = "Success",
                Message = "Threat status updated successfully."
            };
        }

        public async Task<Response> GetAllThreats()
        {
            var threats = await _repo.GetAllThreatsAsync();
            return new Response
            {
                Status = "Success",
                Message = "Threat events retrieved successfully.",
                Data = threats
            };
        }

        public async Task<Response> GetLatestThreatByUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return new Response
                {
                    Status = "Error",
                    Message = "URL cannot be empty.",
                };
            }

            var threat = await _repo.GetLatestThreatByUrlAsync(url);
            return new Response
            {
                Status = "Success",
                Message = threat == null ? "Threat event not found." : "Threat event retrieved successfully.",
                Data = threat
            };
        }

        public async Task<Response> GetThreatByStatus(ThreatStatus status)
        {
            var threats = await _repo.GetThreatsByStatusAsync(status);
            return new Response
            {
                Status = "Success",
                Message = $"Threat events with status '{status}' retrieved successfully.",
                Data = threats
            };
        }
    }
}
   
