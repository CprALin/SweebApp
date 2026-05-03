using SweebAppAPIs.Enum;
using SweebAppAPIs.Models.Responses;

namespace SweebAppAPIs.Services.Interfaces
{
    public interface IThreatServices
    {
        Task<Response> CreateThreatForDevice(int deviceId, string URL, string Protocol, string verdict, ThreatStatus status, int score, string category);
        Task<Response> UpdateThreatStatus(int theatId, ThreatStatus newStatus);
        Task<Response> GetAllThreats();
        Task<Response> GetThreatByStatus(ThreatStatus status);
    }
}
