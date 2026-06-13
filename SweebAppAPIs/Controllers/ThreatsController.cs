using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SweebAppAPIs.Enum;
using SweebAppAPIs.Models.Requests;
using SweebAppAPIs.Services.Interfaces;

namespace SweebAppAPIs.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ThreatsController(IThreatServices services) : ControllerBase
    {
        private readonly IThreatServices _services = services;
        
        [HttpPost("create")]
        public async Task<IActionResult> CreateThreatForDevice([FromBody] CreateThreatRequest request)
        {
            var result = await _services.CreateThreatForDevice(request.DeviceId, request.URL, request.Protocol, request.Verdict, request.Status, request.Score, request.Category);

            if (result == null)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPatch("update/{threatId}")]
        public async Task<IActionResult> UpdateThreatStatus([FromRoute] int threatId, [FromBody] ThreatStatus newStatus)
        {
            var result = await _services.UpdateThreatStatus(threatId, newStatus);
            if (result == null || result.Status == "Error")
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPatch("{threatId}/allow")]
        public async Task<IActionResult> AllowThreat([FromRoute] int threatId)
        {
            var result = await _services.UpdateThreatStatus(threatId, ThreatStatus.Allowed);

            if (result == null || result.Status == "Error")
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPatch("{threatId}/block")]
        public async Task<IActionResult> BlockThreat([FromRoute] int threatId)
        {
            var result = await _services.UpdateThreatStatus(threatId, ThreatStatus.Blocked);

            if (result == null || result.Status == "Error")
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllThreats()
        {
            var result = await _services.GetAllThreats();

            return Ok(result);
        }

        [HttpGet("by-url")]
        public async Task<IActionResult> GetLatestThreatByUrl([FromQuery] string url)
        {
            var result = await _services.GetLatestThreatByUrl(url);

            if (result.Status == "Error")
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetThreatByStatus([FromRoute] ThreatStatus status)
        {
            var result = await _services.GetThreatByStatus(status);

            return Ok(result);
        }
    }
}
