using System;
using System.Collections.Generic;
using System.Text;

namespace SweebAppFront.Models
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
    }
}
