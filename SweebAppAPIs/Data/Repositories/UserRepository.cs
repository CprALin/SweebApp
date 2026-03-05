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

             return (int)successParam.Value == 1;
        }

        public async Task<Models.LoginUserResults?> LoginAsync(string username)
        {
           return await _context.LoginUserResults.FromSqlInterpolated($"EXEC loginUser {username}").AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateUserEmail(int userId , string newEmail) 
        {
            var successParam = new SqlParameter
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

            return (int)successParam.Value == 1;
        }

        public async Task<Models.UserSettings?> GetUserSettingsAsync(int userId)
        {
            return await _context.UserSettings.FromSqlInterpolated($"EXEC getSettings {userId}").AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task UpdateAllwaysOnTopAsync(int idSettings , int allwaysOnTop)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC updateAllwaysOnTop @IdSettings , @AllwaysOnTop",
                new SqlParameter("@IdSettings" , idSettings),
                new SqlParameter("@AllwaysOnTop" , allwaysOnTop)
            )
        }

        public async Task UpdateAllowNotificationsAsync(int idSettings, int allowNotifications)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC updateAllowNotifications @IdSettings , @AllowNotifications",
                new SqlParameter("@IdSettings", idSettings),
                new SqlParameter("@AllowNotifications", allowNotifications)

            );
        }

        public async Task UpdateThemeAsync(int idSettings, string theme)
        {
            await _context.Database.ExecuteSqlRawAsync(
                 "EXEC updateTheme @IdSettings , @Theme",
                 new SqlParameter("@IdSettings", idSettings),
                 new SqlParameter("@Theme", theme)
            );
        }

        public async Task UpdateRunAtStartup(int idSettings, int runAtStartup)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC updateRunAtStartup @IdSettings , @RunAtStartup",
                new SqlParameter("@IdSettings", idSettings),
                new SqlParameter("@RunAtStartup" , runAtStartup)
            );
        }
    }
}
