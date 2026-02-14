namespace SweebAppAPIs.Models
{
    public class DetectionReasons
    {
        public int IdDetReason { get; set; }
        public string ReasonCode { get; set; }
        public int Weight { get; set; }
        public string Detail { get; set; }
        public int ThreatEventId { get; set; }
    }
}
