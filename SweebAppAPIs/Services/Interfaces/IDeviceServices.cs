using SweebAppAPIs.Models;
using SweebAppAPIs.Models.Responses;


namespace SweebAppAPIs.Services.Interfaces
{
    public interface IDeviceServices
    {
        Task<Response> CreateDeviceForCurrentUser(int userId,string deviceName, string deviceOS);
        Task<Response> GetDeviceAsync();
        Task<Response> DeleteDeviceAsync(int id);
    }
}
