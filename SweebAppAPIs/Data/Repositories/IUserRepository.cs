namespace SweebAppAPIs.Data.Repositories
{
    public interface IUserRepository  
    {
        Task<Models.UserInfo?> GetUserByIdAsync(int id);
        Task<bool> RegisterAsync(string username, string email, string password);
        Task<Models.LoginUserResults?> LoginAsync(string username);
        Task<bool> UpdateUserEmail(int userId, string newEmail);
        Task<Models.UserSettings?> GetUserSettingsAsync(int userId);
        Task UpdateAllwaysOnTopAsync(int idSettings, int allwaysOnTop);
        Task UpdateAllowNotificationsAsync(int idSettings , int allowNotifications);
        Task UpdateThemeAsync(int idSettings, string theme);
        Task UpdateRunAtStartup(int idSettings, int runAtStartup);
    }
}
