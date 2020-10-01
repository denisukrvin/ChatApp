using System.Collections.Generic;

namespace ChatApp.Service.Models.Message
{
    public class MessageGroupModel
    {
        public string Date { get; set; }
        public List<MessageModel> Messages { get; set; }
    }
}