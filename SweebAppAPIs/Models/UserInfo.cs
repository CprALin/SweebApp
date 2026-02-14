namespace SweebAppAPIs.Models
{
    public class UserInfo
    {
        public int IdUser { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string HashPassword { get; private set; }
        public string CreatedAt { get; set; }
        public string LastLogin { get; set; }
        public string PhoneNumber { get; set; }
        public string UserRole { get; private set; }
    }
}
