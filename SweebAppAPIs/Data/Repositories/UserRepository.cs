using Microsoft.EntityFrameworkCore;
using SweebAppAPIs.Data.Repositories.Interfaces;
using SweebAppAPIs.Models;

namespace SweebAppAPIs.Data.Repositories
{
    public class UserRepository(AppDbContext context) : IUserRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<User> GetUserAsync()
        {
            var user = await _context.User.FirstOrDefaultAsync();
            if(user == null)
            {
                return new User();
            }
            return user;
        }
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

        public async Task DeleteUserAsync(int userId)
        {
            var user = await _context.User.FindAsync(userId);

            if(user != null)
            {
                _context.User.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
