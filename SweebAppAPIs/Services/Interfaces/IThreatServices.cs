using SweebAppAPIs.Enum;
using SweebAppAPIs.Models.Responses;

namespace SweebAppAPIs.Services.Interfaces
{
    public interface IThreatServices
    {
        Task<Response> CreateThreatForDevice(int deviceId, string URL, string Protocol, string verdict, ThreatStatus status, double score, string category);
        Task<Response> UpdateThreatStatus(int theatId, ThreatStatus newStatus);
        Task<Response> GetAllThreats();
        Task<Response> GetLatestThreatByUrl(string url);
        Task<Response> GetThreatByStatus(ThreatStatus status);
    }
}
