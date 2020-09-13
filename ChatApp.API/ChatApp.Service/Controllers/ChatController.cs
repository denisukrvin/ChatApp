using ChatApp.Service.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ChatApp.Service.Extensions;
using ChatApp.Service.Interfaces;
using ChatApp.Service.Models.Chat.Api;

namespace ChatApp.Service.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet("all")]
        public IActionResult All()
        {
            var userId = HttpContext.GetUserId();
            var result = _chatService.All(userId);
            return Ok(result);
        }

        [HttpPost("create")]
        public IActionResult Create(CreateChatRequest request)
        {
            if (request == null)
                return BadRequest();

            var userId = HttpContext.GetUserId();
            if (request.FirstMemberId != userId && request.SecondMemberId != userId)
                return BadRequest();

            var result = _chatService.Create(request.FirstMemberId, request.SecondMemberId);
            return Ok(result);
        }
    }
}