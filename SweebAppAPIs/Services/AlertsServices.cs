using SweebAppAPIs.Data.Repositories.Interfaces;
using SweebAppAPIs.Services.Interfaces;
using SweebAppAPIs.Models.Responses;
using SweebAppAPIs.Enum;
using SweebAppAPIs.Models;

namespace SweebAppAPIs.Services
{
    public class AlertsServices(IAlertRepository repo) : IAlertsServices
    {
        private readonly IAlertRepository _repo = repo;

        public async Task<Response> CreateAlertAsync(int threatId, string message, AlertSeverity severity)
        {
            if (threatId == 0)
            {
                return new Response
                {
                    Status = "Error",
                    Message = "Threat ID cannot be zero.",
                };
            }

            if (message == null)
            {
                return new Response
                {
                    Status = "Error",
                    Message = "Message cannot be null.",
                };
            }

            var alert = new Alert
            {
                ThreatEventId = threatId,
                Message = message,
                Severity = severity
            };

            var result = await _repo.CreateAlertAsync(alert);

            if (result == null)
            {
                return new Response
                {
                    Status = "Error",
                    Message = "Failed to create alert.",
                };
            }

            return new Response
            {
                Status = "Success",
                Message = "Alert created successfully.",
                Data = result
            };
        }

        public async Task<Response> GetAllAlerts()
        {
            var alerts = await _repo.GetAllAlerts();

            return new Response
            {
                Status = "Success",
                Message = "Alerts retrieved successfully.",
                Data = alerts
            };
        }

        public async Task<Response> DeleteAlertAsync(int alertId)
        {
            if(alertId == 0)
            {
                return new Response
                {
                    Status = "Error",
                    Message = "Alert ID cannot be zero.",
                };
            }

            await _repo.DeleteAlertAsync(alertId);

            return new Response
            {
                Status = "Success",
                Message = "Alert deleted successfully.",
            };
        }

        public async Task<Response> DeleteAllAlertAsync()
        {
            await _repo.DeleteAllAlertsAsync();
            return new Response
            {
                Status = "Success",
                Message = "All alerts deleted successfully.",
            };
        }
    }
}
