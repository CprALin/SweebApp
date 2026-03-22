using System.ComponentModel.DataAnnotations;

namespace SweebAppAPIs.Models
{
    public class UserSettings
    {
        [Key]
        public int UserId { get; set; }
        public bool AllwaysOnTop { get; set; }
        public bool AllowNotifications { get; set; }
        public string Theme { get; set; } = string.Empty;
        public bool RunAtStartup { get; set; }
    }
}
