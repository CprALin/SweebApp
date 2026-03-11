namespace SweebAppAPIs.Data.Repositories
{
    public interface IDeviceRepository
    {
        Task AddDevice(string name, string os, int userId);
        Task<Models.Devices?> GetDevicesForUser(int userId);
        Task DeleteDeviceById(int deviceId);
    }
}

