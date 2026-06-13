using SweebAppAPIs.Enum;

namespace SweebAppAPIs.Models
{
    public class ThreatEvent
    {
        public int Id { get; set; }
        public string URL { get; set; } = string.Empty;
        public string Protocol { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string Verdict { get; set; } = string.Empty;
        public ThreatStatus ActionTaken { get; set; }
        public double Score { get; set; }
        public string Category { get; set; } = string.Empty;
        public int DeviceId { get; set; }
    }
}
