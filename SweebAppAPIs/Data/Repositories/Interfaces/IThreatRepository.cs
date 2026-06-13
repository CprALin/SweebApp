using SweebAppAPIs.Enum;
using SweebAppAPIs.Models;

namespace SweebAppAPIs.Data.Repositories.Interfaces
{
    public interface IThreatRepository
    {
        Task<ThreatEvent> CreateThreatAsync(ThreatEvent threatEvent);
        Task<ThreatEvent?> GetThreatByIdAsync(int threatId);
        Task<ThreatEvent?> GetLatestThreatByUrlAsync(string url);
        Task UpdateThreatAsync(int threatId, ThreatStatus newStatus);
        Task<List<ThreatEvent>> GetAllThreatsAsync();
        Task<List<ThreatEvent>> GetThreatsByStatusAsync(ThreatStatus status);
    }
}
