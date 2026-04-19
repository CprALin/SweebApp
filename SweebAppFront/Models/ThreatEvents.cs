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
        public DateTime Timestamp { get; set; }
        public string Verdict { get; set; } = string.Empty;
        public string ActionTaken { get; set; } = string.Empty;
        public int Score { get; set; } = 0;
        public string Category { get; set; } = string.Empty;
        public int DeviceId { get; set; }
    }
}
