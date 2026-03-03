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
            return await _context.Users.FromSqlInterpolated($"EXEC getUserById {id}").AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<bool> RegisterAsync(string username, string email, string password)
        {
         
            int successParam = new SqlParameter
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

             return successParam.Value == 1;
        }

        public async Task<(int UserId , string PasswordHash)> LoginAsync(string username)
        {
           return await _context.LoginUserResults.FromSqlInterpolated($"EXEC loginUser {username}").AsNoTrack().FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateUserEmail(int userId , string newEmail) 
        {
            int successParam = new SqlParameter
            {
                ParameterName = "@Success",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC updateUserEmailById @UserId , @NewEmail , @Success OUTPUT",
                new SqlParameter("@UserId" , userId),
                new SqlParameter("@NewEmail" , newEmail),
                successParam
            );

            return successParam.Value == 1;
        }
    }
}
