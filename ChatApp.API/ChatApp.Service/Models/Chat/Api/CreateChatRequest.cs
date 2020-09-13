using System;

namespace ChatApp.Service.Models.Chat.Api
{
    public class CreateChatRequest
    {
        public int FirstMemberId { get; set; }
        public int SecondMemberId { get; set; }
    }
}