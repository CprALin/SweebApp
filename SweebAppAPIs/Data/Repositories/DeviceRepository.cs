using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace SweebAppAPIs.Data.Repositories
{
    public class DeviceRepository(AppDbContext context) : IDeviceRepository
    {
        private readonly AppDbContext _context = context;

        public async Task AddDevice(string name , string os , int userId)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC addDevice @Name , @OS , @UserId",
                new SqlParameter("@Name" , name),
                new SqlParameter("@OS" , os),
                new SqlParameter("@UserId" , userId)
            );
        }

        public async Task<Models.Devices?> GetDevicesForUser(int userId)
        {
            return await _context.Devices.FromSqlInterpolated($"EXEC getDevicesForUser {userId}").AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task DeleteDeviceById(int deviceId)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC deleteDeviceById @IdDevice",
                new SqlParameter("@IdDevice" , deviceId)
            );
        }
        
    }
}
