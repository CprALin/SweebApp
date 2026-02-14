namespace SweebAppAPIs.Models
{
    public class UserSettings
    {
        public int UserId { get; set; }
        public bool AllwaysOnTop { get; set; }
        public bool AllowNotifications { get; set; }
        public string Theme { get; set; }
        public bool RunAtStartup { get; set; }
    }
}
