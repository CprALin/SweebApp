using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SweebAppAPIs.Models.Requests;
using SweebAppAPIs.Services.Interfaces;

namespace SweebAppAPIs.Controllers
{
    [Route("api/v1/device/[controller]")]
    [ApiController]
    public class DeviceController(IDeviceServices services) : ControllerBase
    {
        private readonly IDeviceServices _services = services;

        [HttpPost("create")]
        public async Task<IActionResult> CreateDeviceForCurrentUser([FromBody] CreateDeviceRequest request)
        {
            var result = await _services.CreateDeviceForCurrentUser(request.UserId, request.DeviceName, request.DeviceOS);

            if(result.Status == "Error")
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
