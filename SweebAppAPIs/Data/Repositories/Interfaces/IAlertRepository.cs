using SweebAppAPIs.Models;

namespace SweebAppAPIs.Data.Repositories.Interfaces
{
    public interface IAlertRepository
    {
        Task<Alert> CreateAlertAsync(Alert alert);
        Task<List<Alert>> GetAllAlerts();
        Task DeleteAlertAsync(int alertId);
        Task DeleteAllAlertsAsync();
    }
}
