using SweebAppAPIs.Models;
using SweebAppAPIs.Models.Responses;

namespace SweebAppAPIs.Services.Interfaces
{
    public interface IUserServices
    {
        Task<Response> CreateUserAsync(string username);
        Task<Response> UpdateUserAsync(int userId, string newName);
        Task<Response> DeleteUser(int userId);
        Task<Response> GetUserAsync();
    }
}
