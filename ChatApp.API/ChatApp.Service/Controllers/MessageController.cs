using ChatApp.Service.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ChatApp.Service.Extensions;
using ChatApp.Service.Interfaces;
using ChatApp.Service.Models.Message.Api;

namespace ChatApp.Service.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet("all")]
        public IActionResult All(int chatId)
        {
            if (chatId == 0)
                return BadRequest();

            var result = _messageService.All(chatId);
            return Ok(result);
        }

        [HttpPost("create")]
        public IActionResult Create(CreateMessageRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = HttpContext.GetUserId();
            var result = _messageService.Create(request.ChatId, userId, request.Text);
            return Ok(result);
        }
    }
}