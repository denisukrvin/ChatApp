using ChatApp.Service.Tools;
using ChatApp.Service.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ChatApp.Service.Interfaces;
using ChatApp.Service.Extensions;
using Microsoft.Extensions.Options;
using ChatApp.Service.Models.Profile;

namespace ChatApp.Service.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private IProfileService _profileService;

        public ProfileController(IOptions<AppSettings> appSettings, IProfileService profileService)
        {
            _appSettings = appSettings.Value;
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
            // save avatar
            if (!string.IsNullOrWhiteSpace(model.Avatar))
            {
                var base64Image = model.Avatar.Substring(model.Avatar.IndexOf(",") + 1);
                model.Avatar = ImgBBExtensions.UploadImage(_appSettings.ApiKey, base64Image);
            }          

            var result = _profileService.Edit(userId, model);
            return Ok(result);
        }
    }
}