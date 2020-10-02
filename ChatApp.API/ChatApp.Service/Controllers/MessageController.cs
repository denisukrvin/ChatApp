using System;
using ChatApp.Service.Hubs;
using ChatApp.Service.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ChatApp.Service.Extensions;
using ChatApp.Service.Interfaces;
using Microsoft.AspNetCore.SignalR;
using ChatApp.Service.Models.Message.Api;

namespace ChatApp.Service.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private IMessageService _messageService;
        private readonly IHubContext<ChatHub> _hubContext;

        public MessageController(IMessageService messageService, IHubContext<ChatHub> hubContext)
        {
            _messageService = messageService;
            _hubContext = hubContext;
        }

        [HttpGet("all")]
        public IActionResult All(int chatId, int lastMessageId = 0)
        {
            if (chatId == 0)
                return BadRequest();

            var result = _messageService.All(chatId, lastMessageId);
            return Ok(result);
        }

        [HttpPost("create")]
        public IActionResult Create(CreateMessageRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var userId = HttpContext.GetUserId();
            var result = _messageService.Create(request.ChatId, userId, request.Text);
            var messageId = Convert.ToInt32(result.Data["message_id"]);
            var message = _messageService.Get(messageId);
            _hubContext.Clients.Group(request.ChatId.ToString()).SendAsync("ReceiveMessage", message);
            return Ok(result);
        }
    }
}