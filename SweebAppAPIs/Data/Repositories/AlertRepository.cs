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
        public async Task<Models.Alerts?> GetAllAlertsForDevice(int userId, int deviceId)
        {
            return await _context.Alerts.FromSqlInterpolated($"EXEC getAllAlertsForDevice {userId} , {deviceId}").AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task UpdateAsReadAlert(int alertId, int userId)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC updateAsReadAlerts @IdAlert , @UserId",
                new SqlParameter("@IdAlerts", alertId),
                new SqlParameter("@UserId", userId)
            );
        }
    }
}
