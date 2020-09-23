using ChatApp.Service.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ChatApp.Service.Interfaces;
using ChatApp.Service.Extensions;
using ChatApp.Service.Models.Profile;

namespace ChatApp.Service.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        [Route("get")]
        public IActionResult Get()
        {
            var userId = HttpContext.GetUserId();
            if (userId == 0)
                return BadRequest();

            var profile = _profileService.Get(userId);
            return Ok(profile);
        }

        [HttpPost("edit")]
        public IActionResult Edit(ProfileModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var userId = HttpContext.GetUserId();
            var result = _profileService.Edit(userId, model);
            return Ok(result);
        }
    }
}