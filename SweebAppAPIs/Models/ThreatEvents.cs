namespace SweebAppAPIs.Models
{
    public class ThreatEvents
    {
        public int IdThreatEvent { get; set; }
        public string URL { get; set; }
        public string Protocol { get; set; }
        public string Host { get; set; }
        public string Path { get; set; }
        public string Status { get; set; }
        public string Timestamp { get; set; }
        public string ActionTaken { get; set; }
        public int Score { get; set; }
        public string Category { get; set; }
        public int DeviceId { get; set; }
    }
}
