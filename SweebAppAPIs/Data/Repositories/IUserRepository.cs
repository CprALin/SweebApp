namespace SweebAppAPIs.Data.Repositories
{
    public interface IUserRepository  
    {
        Task<Models.UserInfo?> GetUserByIdAsync(int id);
        Task<bool> RegisterAsync(string username, string email, string password);
        Task<(int UserId, string PasswordHash)> LoginAsync(string username);
        Task<bool> UpdateUserEmail(int userId, string newEmail);
    }
}
