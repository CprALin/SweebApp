using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace SweebAppAPIs.Data.Repositories
{
    public class ThreatRepository(AppDbContext context) : IThreatRepository
    {
        private readonly AppDbContext _context = context;

        public async Task AddThreatEvent(string url, string protocol, string host, string path, string status, string verdict, string actionTaken, int score, string category, int deviceId)
        {
            await _context.Database.ExecuteSqlRawAsync(
               "EXEC addThreatEvent @URL , @Protocol , @Host , @Path , @Status , @Verdict , @ActionTaken , @Score , @Category , @DeviceId",
               new SqlParameter("@URL" , url),
               new SqlParameter("@Protocol" , protocol),
               new SqlParameter("@Host" , host),
               new SqlParameter("@Path" , path),
               new SqlParameter("@Status" , status),
               new SqlParameter("@Verdict" , verdict),
               new SqlParameter("@ActionTaken" , actionTaken),
               new SqlParameter("@Score" , score),
               new SqlParameter("@Category" , category),
               new SqlParameter("@DeviceId" , deviceId)
            );
        }

        public async Task<Models.ThreatEvents?> GetThreatEventsForDevice(int deviceId)
        {
            return await _context.ThreatEvents.FromSqlInterpolated($"EXEC getThreatEventsForDevice {deviceId}").AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task AddDetectionReason(string reasonCode, int weight, string details, int threatEventId)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC addDetectionReason @ReasonCode , @Weight , @Details , @ThreatEventId",
                new SqlParameter("@ReasonCode", reasonCode),
                new SqlParameter("@Weight", weight),
                new SqlParameter("@Details", details),
                new SqlParameter("@ThreatEventId", threatEventId)
            );
        }

        public async Task<Models.DetectionReasons?> GetDetectionReason(int threatEventId)
        {
            return await _context.DetectionReasons.FromSqlInterpolated($"EXEC getDetectionReason {threatEventId}").AsNoTracking().FirstOrDefaultAsync();
        }
    }
}
