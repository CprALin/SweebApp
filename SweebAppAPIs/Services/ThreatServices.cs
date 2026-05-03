using SweebAppAPIs.Data.Repositories.Interfaces;
using SweebAppAPIs.Enum;
using SweebAppAPIs.Models.Responses;
using SweebAppAPIs.Services.Interfaces;
using SweebAppAPIs.Models;

namespace SweebAppAPIs.Services
{
    public class ThreatServices(IThreatRepository repo) : IThreatServices
    {
        private readonly IThreatRepository _repo = repo;

        public async Task<Response> CreateThreatForDevice(int deviceId, string URL, string Protocol, string verdict, ThreatStatus status, int score, string category)
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
   