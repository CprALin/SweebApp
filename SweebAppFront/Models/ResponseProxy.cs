using System;
using System.Collections.Generic;
using System.Text;

namespace SweebAppFront.Models
{
    public class ResponseProxy
    {
        public string Method { get; set; } = string.Empty;
        public string Protocol { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public bool IsThreat { get; set; }
    }
}
