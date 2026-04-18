using System;
using System.Collections.Generic;
using System.Text;

namespace SweebAppFront.Models
{
    public class Devices
    {
        public int IdDevice { get; set; }
        public string Name { get; set; } = string.Empty;
        public string OS { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int UserId { get; set; }
    }
}
