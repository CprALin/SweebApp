using SweebAppAPIs.Models;

namespace SweebAppAPIs.Data.Repositories.Interfaces
{
    public interface IDeviceRepository
    {
       Task<Device> CreateDeviceAsync(Device device);
       Task<Device> GetDevicesAsync();
       Task DeleteDeviceAsync(int id);
    }
}
