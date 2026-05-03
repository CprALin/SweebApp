using SweebAppAPIs.Data.Repositories.Interfaces;
using SweebAppAPIs.Models;

namespace SweebAppAPIs.Data.Repositories
{
    public class UserRepository(AppDbContext context) : IUserRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<User> CreateUserAsync(User user)
        {
            _context.User.Add(user);

            await _context.SaveChangesAsync();

            return user;
        }

        public async Task UpdateUserAsync(int userId,string newName)
        {
            var user = await _context.User.FindAsync(userId);

            user?.UserName = newName;

            await _context.SaveChangesAsync();
        }

    }
}
