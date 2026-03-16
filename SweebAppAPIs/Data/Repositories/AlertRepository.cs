using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace SweebAppAPIs.Data.Repositories
{
    public class AlertRepository(AppDbContext context) : IAlertRepository
    {
        private readonly AppDbContext _context = context;

        public async Task AddAlert(int userId, int deviceId, int threatEventId, string severity, int isRead)
        {
            await _context.Database.ExecuteSqlRawAsync(
               "EXEC addAlert @UserId , @DeviceId , @ThreatEventId , @Severity , @IsRead",
               new SqlParameter("@UserId", userId),
               new SqlParameter("@DeviceId", deviceId),
               new SqlParameter("@ThreatEventId", threatEventId),
               new SqlParameter("@Severity", severity),
               new SqlParameter("@IsRead", isRead)
            );
        }
        public async Task<Models.Alerts?> GetHotAlert(int userId, int deviceId, int threatEventId)
        {
            return await _context.Alerts.FromSqlInterpolated($"EXEC getHotAlert {userId} , {deviceId} , {threatEventId}").AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<List<Models.Alerts>> GetAllAlertsForDevice(int userId, int deviceId)
        {
            return await _context.Alerts.FromSqlInterpolated($"EXEC getAllAlertsForDevice {userId} , {deviceId}").ToListAsync();
        }
        public async Task UpdateAsReadAlert(int alertId, int userId)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC updateAsReadAlerts @IdAlert , @UserId",
                new SqlParameter("@IdAlerts", alertId),
                new SqlParameter("@UserId", userId)
            );
        }

        public async Task<List<Models.AlertsFeed>> GetAlertsFeedByUser(int userId)
        {
            return await _context.AlertFeeds.FromSqlInterpolated($"EXEC getAlertsFeedByUser {userId}").ToListAsync();
        }

        public async Task<List<Models.AlertsFeed>> GetAlertsFeedByDevice(int deviceId, int userId)
        {
            return await _context.AlertFeeds.FromSqlInterpolated($"EXEC getAlertsFeedByDevice {deviceId} , {userId}").ToListAsync();
        }

        public async Task<List<Models.AlertsFeed>> GetUnreadAlertsCount(int userId)
        {
            return await _context.AlertFeeds.FromSqlInterpolated($"EXEC getUnreadAlertsCount {userId}").ToListAsync();
        }
    }
}
