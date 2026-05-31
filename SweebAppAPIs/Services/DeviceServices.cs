using SweebAppAPIs.Data.Repositories.Interfaces;
using SweebAppAPIs.Models;
using SweebAppAPIs.Models.Responses;
using SweebAppAPIs.Services.Interfaces;

namespace SweebAppAPIs.Services
{
    public class DeviceServices(IDeviceRepository repo) : IDeviceServices
    {
       private readonly IDeviceRepository _repo = repo;

       public async Task<Response> CreateDeviceForCurrentUser(int userId, string deviceName, string deviceOS)
       {
          if(userId == 0 || string.IsNullOrEmpty(deviceName) || string.IsNullOrEmpty(deviceOS)){
                return new Response
                {
                    Status = "Error",
                    Message = "User ID, Device Name, and Device OS are required."
                };
          }

          var device = new Device
          {
              UserId = userId,
              Name = deviceName,
              OS = deviceOS
          };

          var response = await _repo.CreateDeviceAsync(device);

          if(response == null)
          {
              return new Response
              {
                  Status = "Error",
                  Message = "Failed to create device."
              };
          }

          return new Response
          {
              Status = "Success",
              Message = "Device created successfully.",
              Data = response
          };
       }

       public async Task<Response> GetDeviceAsync()
       {
            var response = await _repo.GetDevicesAsync();
            if (response == null)
            {
                return new Response
                {
                    Status = "Error",
                    Message = "Failed to retrieve device."
                };
            }
            return new Response
            {
                Status = "Success",
                Message = "Devices retrieved successfully.",
                Data = response
            };
       }

       public async Task<Response> DeleteDeviceAsync(int id)
       {
            if (id == 0)
            {
                return new Response
                {
                    Status = "Error",
                    Message = "Device ID is required."
                };
            }
            await _repo.DeleteDeviceAsync(id);
            return new Response
            {
                Status = "Success",
                Message = "Device deleted successfully."
            };
       }

    }
}
