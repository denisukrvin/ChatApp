using ChatApp.Service.Helpers;
using Microsoft.AspNetCore.Mvc;
using ChatApp.Service.Interfaces;

namespace ChatApp.Service.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("all")]
        public IActionResult All()
        {
            var result = _userService.All();
            return Ok(result);
        }
    }
}