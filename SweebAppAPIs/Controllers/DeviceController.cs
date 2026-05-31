using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SweebAppAPIs.Models.Requests;
using SweebAppAPIs.Services.Interfaces;

namespace SweebAppAPIs.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DeviceController(IDeviceServices services) : ControllerBase
    {
        private readonly IDeviceServices _services = services;

        [HttpPost("create")]
        public async Task<IActionResult> CreateDeviceForCurrentUser([FromBody] CreateDeviceRequest request)
        {
            var result = await _services.CreateDeviceForCurrentUser(request.UserId, request.DeviceName, request.DeviceOS);

            if (result.Status == "Error")
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetDevice()
        {
            var result = await _services.GetDeviceAsync();

            if (result.Status == "Error")
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("delete/{deviceId}")]
        public async Task<IActionResult> DeleteDevice([FromRoute] int deviceId)
        {
            var result = await _services.DeleteDeviceAsync(deviceId);
            if (result.Status == "Error")
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
