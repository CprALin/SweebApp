using System.Reflection.Metadata;

namespace SweebAppAPIs.Data.Repositories
{
	public interface IThreatRepository
    {
		Task AddThreatEvent(string url , string protocol , string host , string path , string status , string verdict , string actionTaken , int score , string category , int deviceId);
		Task<List<Models.ThreatEvents>> GetThreatEventsForDevice(int deviceId);
		Task AddDetectionReason(string reasonCode, int weight, string details, int threatEventId);
		Task<Models.DetectionReasons?> GetDetectionReason(int threatEventId);
		Task<List<Models.ThreatEventsWithDevice>> GetThreatEventsByUser(int userId);
		Task<List<Models.ThreatEventsWithDevice>> GetThreatEventsByDevice(int userId , int deviceId);
		Task<List<Models.ThreatEventsWithDevice>> GetRecentThreatEvents(int userId);
	}
}
