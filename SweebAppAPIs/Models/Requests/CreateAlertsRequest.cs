using SweebAppAPIs.Enum;

namespace SweebAppAPIs.Models.Requests
{
    public class CreateAlertsRequest
    {
        public int ThreatId { get; set; }
        public string Message { get; set; } = string.Empty;
        public AlertSeverity Severity { get; set; }
    }
}
