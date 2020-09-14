using ChatApp.Service.Helpers;
using Microsoft.AspNetCore.Mvc;
using ChatApp.Service.Interfaces;
using ChatApp.Service.Extensions;

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
            var userId = HttpContext.GetUserId();
            var result = _userService.All(userId);
            return Ok(result);
        }
    }
}