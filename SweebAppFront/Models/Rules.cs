using System;
using System.Collections.Generic;
using System.Text;

namespace SweebAppFront.Models
{
    public class Rules
    {
        public int IdRule { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsEnabled { get; set; }
        public bool Priority { get; set; }
        public string Action { get; set; } = string.Empty;
        public string MatchType { get; set; } = string.Empty;
        public string Pattern { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;

    }
}
