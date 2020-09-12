using Microsoft.AspNetCore.Mvc;
using ChatApp.Service.Interfaces;
using ChatApp.Service.Models.Auth;

namespace ChatApp.Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _authService.Login(request);
            return Ok(result);
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _authService.Register(request);
            return Ok(result);
        }
    }
}