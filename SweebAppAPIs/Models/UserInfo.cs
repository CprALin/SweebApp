namespace SweebAppAPIs.Models
{
    public class UserInfo
    {
        public int IdUser { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash {  get; set; }
        public required string CreatedAt { get; set; }
        public string? LastLogin { get; set; } 
        public string? PhoneNumber { get; set; }
        public required string UserRole { get; set; }
        public string? GoogleSub { get; set; }
    }
}
