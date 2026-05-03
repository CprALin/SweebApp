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

    }
}
