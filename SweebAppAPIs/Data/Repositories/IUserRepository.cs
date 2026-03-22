namespace SweebAppAPIs.Data.Repositories
{
    /// <summary>
    /// Defines a contract for user account and settings management operations, including user retrieval, registration,
    /// authentication, and user-specific configuration updates.
    /// </summary>
    /// <remarks>Implementations of this interface provide asynchronous methods for managing user information
    /// and preferences. Methods support retrieving user details, registering new users, authenticating users, and
    /// updating user settings such as email, theme, notification preferences, and startup behavior. All methods are
    /// asynchronous and intended for use in environments where non-blocking operations are required, such as web
    /// applications or services.</remarks>
    public interface IUserRepository  
    {
        Task<Models.UserInfo?> GetUserByIdAsync(int id);
        /// <summary>
        /// Asynchronously registers a new user account with the specified username, email address, and password.
        /// </summary>
        /// <param name="username">The unique username to associate with the new user account. Cannot be null or empty.</param>
        /// <param name="email">The email address to associate with the new user account. Must be a valid email format and cannot be null or
        /// empty.</param>
        /// <param name="password">The password for the new user account. Cannot be null or empty.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if registration
        /// succeeds; otherwise, <see langword="false"/>.</returns>
        Task<bool> RegisterAsync(string username, string email, string password);
        Task<Models.LoginUserResults?> LoginAsync(string username);
        Task<bool> UpdateUserEmail(int userId, string newEmail);
        Task<Models.UserSettings?> GetUserSettingsAsync(int userId);
        Task UpdateAllwaysOnTopAsync(int idSettings, int allwaysOnTop);
        Task UpdateAllowNotificationsAsync(int idSettings , int allowNotifications);
        Task UpdateThemeAsync(int idSettings, string theme);
        Task UpdateRunAtStartup(int idSettings, int runAtStartup);
        Task<Models.UserData?> GetUserData(string username);
    }
}
