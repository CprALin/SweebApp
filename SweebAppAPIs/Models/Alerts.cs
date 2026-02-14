namespace SweebAppAPIs.Models
{
    public class Alerts
    {
        public int IdAlert { get; set; }
        public int UserId { get; set; }
        public int ThreatEventId { get; set; }
        public string Severity { get; set; }
        public bool IsRead { get; set; }
        public string CreatedAt { get; set; }
    }
}
