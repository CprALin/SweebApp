using System;
using System.Collections.Generic;
using System.Text;

namespace SweebAppFront.Models
{
    public class Alerts
    {
        public int IdAlert { get; set; }
        public int UserId { get; set; }
        public int DeviceId { get; set; }
        public int ThreatEventId { get; set; }
        public string Severity { get; set; } = string.Empty;
        public bool IsRead { get; set; }
        public string CreatedAt { get; set; } = string.Empty;
    }
}
