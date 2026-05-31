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

        public async Task<Device> GetDevicesAsync()
        {
           var device = _context.Device.FirstOrDefault();

           if(device == null)
           {
                return new Device();
           } 
           return device;
        }

        public async Task DeleteDeviceAsync(int id)
        {
                        var device = await _context.Device.FindAsync(id);
            if (device != null)
            {
                _context.Device.Remove(device);
                await _context.SaveChangesAsync();
            }
        }
    }
}
