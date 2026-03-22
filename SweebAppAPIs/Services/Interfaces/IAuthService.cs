using SweebAppAPIs.Models;

namespace SweebAppAPIs.Services.Interfaces
{
    public interface IAuthService
    {
        Task<Result> RegisterAsync(string username, string email, string password);
        Task<Result> LoginAsync(string username , string password); 
    }
}
