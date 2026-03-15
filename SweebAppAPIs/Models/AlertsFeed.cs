namespace SweebAppAPIs.Models
{
    public class AlertsFeed
    {
        public int IdAlert { get; set; }
        public int UserId { get; set; }
        public int DeviceId { get; set; }
        public string Name { get; set; }
        public int ThreatEventId { get; set; }
        public string URL { get; set; }
        public string Host {  get; set; }
        public string Path { get; set; }
        public string Status { get; set; }
        public string Verdict { get; set; }
        public string Severity { get; set; }
        public int IsRead { get; set; }
        public string CreatedAt { get; set; }
    }
}
