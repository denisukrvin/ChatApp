using System;
using System.Threading.Tasks;
using ChatApp.Service.Helpers;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Service.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var chatId = httpContext.Request.Query["chatId"];

            await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            var httpContext = Context.GetHttpContext();
            var chatId = httpContext.Request.Query["chatId"];

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId);
            await base.OnDisconnectedAsync(ex);
        }
    }
}