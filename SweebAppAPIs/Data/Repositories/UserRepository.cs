using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SweebAppAPIs.Models;
using Microsoft.Data.SqlClient;

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
                return await _context.Users.FromSqlRaw($"EXEC getUserById {id}").AsNoTracking().FirstOrDefaultAsync();
            }
            catch(Exception)
            {
               throw;
            }

        }

        public async Task<bool> RegisterAsync(string username , string email , string password)
        {
            try
            {
                var successParam = new SqlParameter
                {
                    ParameterName = "@Success",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Output
                };

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC registerUser @Username , @Email , @PasswordHash , @Success OUTPUT",
                    new SqlParameter("@Username", username),
                    new SqlParameter("@Email", email),
                    new SqlParameter("@PasswordHash", password),
                    successParam
                );
            }
            catch (Exception)
            {
                throw;
            }
    }
}
