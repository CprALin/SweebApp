using System.Reflection.Metadata;

namespace SweebAppAPIs.Data.Repositories
{
	public interface IThreatRepository
    {
		Task AddThreatEvent(string url , string protocol , string host , string path , string status , string verdict , string actionTaken , int score , string category , int deviceId);
		Task<Models.ThreatEvents?> GetThreatEventsForDevice(int deviceId);
		Task AddDetectionReason(string reasonCode, int weight, string details, int threatEventId);
		Task<Models.DetectionReasons?> GetDetectionReason(int threatEventId);
		Task<Models.ThreatEventsWithDevice?> GetThreatEventsByUser(int userId);
		Task<Models.ThreatEventsWithDevice?> GetThreatEventsByDevice(int userId , int deviceId);
		Task<Models.ThreatEventsWithDevice?> GetRecentThreatEvents(int userId);
	}
}
