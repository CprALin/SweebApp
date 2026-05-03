using SweebAppAPIs.Enum;
using SweebAppAPIs.Models.Responses;

namespace SweebAppAPIs.Services.Interfaces
{
    public interface IAlertsServices
    {
        Task<Response> CreateAlertAsync(int threatId, string message, AlertSeverity severity);
        Task<Response> GetAllAlerts();
        Task<Response> DeleteAlertAsync(int alertId);
        Task<Response> DeleteAllAlertAsync();

    }
}
