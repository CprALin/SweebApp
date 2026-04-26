using SweebAppFront.Enums;
using SweebAppFront.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SweebAppFront.Models
{
    public class Alerts : BaseViewModel
    {
        public int IdAlert { get; set; }
        public string Message { get; set; } = string.Empty;
        public AlertSeverity Severity { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
