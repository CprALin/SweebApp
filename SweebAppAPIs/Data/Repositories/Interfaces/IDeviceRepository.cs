using SweebAppAPIs.Models;

namespace SweebAppAPIs.Data.Repositories.Interfaces
{
    public interface IDeviceRepository
    {
        public Task<Device> CreateDeviceAsync(Device device);
    }
}
