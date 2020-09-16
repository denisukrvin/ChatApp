using System.Collections.Generic;
using ChatApp.Service.Models.Message;
using ChatApp.Service.Models.Common.Api;

namespace ChatApp.Service.Interfaces
{
    public interface IMessageService
    {
        List<MessageModel> All(int chatId);
        OperationResponse Create(int chatId, int userId, string text);
    }
}