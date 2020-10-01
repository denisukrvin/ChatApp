using System.Collections.Generic;
using ChatApp.Service.Models.Message;
using ChatApp.Service.Models.Common.Api;

namespace ChatApp.Service.Interfaces
{
    public interface IMessageService
    {
        List<MessageGroupModel> All(int chatId);
        MessageModel Get(int messageId);
        OperationResponse Create(int chatId, int userId, string text);
    }
}