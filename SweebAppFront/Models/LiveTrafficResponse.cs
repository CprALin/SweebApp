namespace SweebAppAPIs.Models
{
    public class LiveTrafficResponse
    {
        public string Method { get; set; } = string.Empty;
        public string Protocol { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public bool IsThreat { get; set; }
    }
}
