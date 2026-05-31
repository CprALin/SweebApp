using System;
using System.Collections.Generic;
using System.Text;

namespace SweebAppFront.Models
{
    public class Devices
    {
        public int IdDevice { get; set; }
        public int UserId { get; set; }
        public string DeviceName { get; set; } = string.Empty;
        public string OS { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
