using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SweebAppAPIs.Services.Interfaces;

namespace SweebAppAPIs.Controllers
{
    [Route("api/v1/user/[controller]")]
    [ApiController]
    public class UserController(IUserServices services) : ControllerBase
    {
        private readonly IUserServices _services = services;

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] string username)
        {
            var result = await _services.CreateUserAsync(username);

            if(result.Status == "Error" || result.Status == "Info")
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPatch("update/{id}")]
        public async Task<IActionResult> UpdateUserAsyync([FromRoute] int id,[FromBody] string newName)
        {
            var result = await _services.UpdateUserAsync(id, newName);

            if(result.Status == "Info")
            {
                return BadRequest(result);
            };

            return Ok(result);
        }
    }
}
