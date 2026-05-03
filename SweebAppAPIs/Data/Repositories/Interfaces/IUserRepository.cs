using SweebAppAPIs.Models;

namespace SweebAppAPIs.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateUserAsync(User user);
        Task UpdateUserAsync(int userId,string newName);
    }
}
