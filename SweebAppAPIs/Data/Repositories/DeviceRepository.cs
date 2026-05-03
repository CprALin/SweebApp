using SweebAppAPIs.Data.Repositories.Interfaces;
using SweebAppAPIs.Models;

namespace SweebAppAPIs.Data.Repositories
{
    public class DeviceRepository(AppDbContext context) : IDeviceRepository
    {
        private readonly AppDbContext _context = context;
        
        public async Task<Device> CreateDeviceAsync(Device device)
        {
            _context.Device.Add(device);

            await _context.SaveChangesAsync();

            return device;
        }
    }
}
