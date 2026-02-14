using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SweebAppAPIs.Models;

namespace SweebAppAPIs.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Models.UserInfo?> GetUserByIdAsync(int id)
        {
            try
            {
                return await _context.Users.FromSqlRaw("EXEC getUserById @p0", id).AsNoTracking().FirstOrDefaultAsync();
            }
            catch(Exception)
            {
               throw;
            }

        }
    }
}
