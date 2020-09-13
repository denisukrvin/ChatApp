using System.Collections.Generic;
using ChatApp.Service.Models.Chat;
using ChatApp.Service.Models.Common.Api;

namespace ChatApp.Service.Interfaces
{
    public interface IChatService
    {
        List<ChatModel> All(int userId);
        OperationResponse Create(int firstMemberId, int secondMemberId);
    }
}