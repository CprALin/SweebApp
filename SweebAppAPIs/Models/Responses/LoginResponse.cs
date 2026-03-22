namespace SweebAppAPIs.Models.Responses
{
    public class LoginResponse
    {
        public required bool Success { get; set; }
        public int IdUser { get; set; }
        public int IdSettings { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public required DateTime CreatedAt { get; set; }
        public DateTime? LastLogin { get; set; }
        public string? PhoneNumber { get; set; }
        public required string UserRole { get; set; }
        public bool AllwaysOnTop { get; set; }
        public bool AllowNotifications { get; set; }
        public required string Theme { get; set; }
        public bool RunAtStartup { get; set; }
        public required string Token { get; set; }
    }
}
