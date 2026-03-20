using SweebAppAPIs.Models;

namespace SweebAppAPIs.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(string username, string email, string password);
        Task<LoginUserResults?> LoginAsync(string username); 
    }
}
