namespace SweebAppAPIs.Models
{
    public class UserData
    {
        public int IdUser { get; set; }
        public int IdSettings { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public required string CreatedAt { get; set; }
        public required string LastLogin { get; set; }
        public required string PhoneNumber { get; set; }
        public required string UserRole { get; set; }
        public required string AllwaysOnTop { get; set; }
        public required string AllowNotifiactions { get; set; }
        public required string Theme { get; set; }
        public bool RunAtStartup { get; set; }
    }
}
