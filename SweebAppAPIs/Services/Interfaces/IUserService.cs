using SweebAppAPIs.Models;

namespace SweebAppAPIs.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserInfo?> GetUserByIdAsync(int id);
        Task<bool> UpdateUserEmail(int userId, string newEmail);
        Task<UserSettings?> GetUserSettingsAsync(int userId);
        Task UpdateAllwaysOnTopAsync(int idSettings, int allwaysOnTop);
        Task UpdateAllowNotificationsAsync(int idSettings, int allowNotifications);
        Task UpdateThemeAsync(int idSettings, string theme);
        Task UpdateRunAtStartup(int idSettings, int runAtStartup);

    }
}
