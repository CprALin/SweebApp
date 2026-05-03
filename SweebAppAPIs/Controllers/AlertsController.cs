using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SweebAppAPIs.Models.Requests;
using SweebAppAPIs.Services.Interfaces;

namespace SweebAppAPIs.Controllers
{
    [Route("api/v1/alerts/[controller]")]
    [ApiController]
    public class AlertsController(IAlertsServices services) : ControllerBase
    {
        private readonly IAlertsServices _services = services;

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAlerts()
        {
            var result = await _services.GetAllAlerts();

            return Ok(result);
        }

        [HttpDelete("all")]
        public async Task<IActionResult> ClearAllAlerts()
        {
            var result = await _services.DeleteAllAlertAsync();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlert([FromRoute] int id)
        {
            var result = await _services.DeleteAlertAsync(id);

            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAlert([FromBody] CreateAlertsRequest request)
        {
            var result = await _services.CreateAlertAsync(request.ThreatId, request.Message, request.Severity);
            
            if(result.Status == "Error")
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
