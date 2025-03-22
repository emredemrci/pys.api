using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using pys.api.Services;

namespace pys.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;

        public AuthController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request.Username == "admin" && request.Password == "1234")
            {
                var token = _jwtService.GenerateToken(request.Username);
                return Ok(new { token });
            }

            return Unauthorized();
        }
    }
}
