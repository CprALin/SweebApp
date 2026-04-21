using SweebAppFront.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SweebAppFront.Models
{
    public class Alerts
    {
        public int IdAlert { get; set; }
        public string Message { get; set; } = string.Empty;
        public AlertSeverity Severity { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
        public string TimeDisplay => CreatedAt.ToString("HH:mm");
    }
}
