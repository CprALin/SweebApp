using System;
using System.Collections.Generic;
using System.Text;

namespace SweebAppFront.Models
{
    public class ThreatEvents
    {
        public int IdThreatEvent { get; set; }
        public string URL { get; set; } = string.Empty;
        public string Protocol { get; set; } = string.Empty;
        public string Host { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Timestamp { get; set; } = string.Empty;
        public string Verdict { get; set; } = string.Empty;
        public string ActionTaken { get; set; } = string.Empty;
        public int Score { get; set; } = 0;
        public string Category { get; set; } = string.Empty;
        public int DeviceId { get; set; }
    }
}
