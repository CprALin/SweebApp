using SweebAppAPIs.Enum;

namespace SweebAppAPIs.Models.Requests
{
    public class CreateThreatRequest
    {
        public int DeviceId { get; set; } 
        public string URL { get; set; } = string.Empty;
        public string Protocol { get; set; } = string.Empty;
        public string Verdict { get; set; } = string.Empty;
        public ThreatStatus Status { get; set; }
        public double Score { get; set; }
        public string Category { get; set; } = string.Empty;
    }
}
