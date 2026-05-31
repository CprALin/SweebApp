using SweebAppAPIs.Enum;

namespace SweebAppAPIs.Models
{
    public class Alert
    {
        public int Id { get; set; }
        public string Message { get; set; } = string.Empty;
        public AlertSeverity Severity { get; set; }
        public int ThreatEventId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
