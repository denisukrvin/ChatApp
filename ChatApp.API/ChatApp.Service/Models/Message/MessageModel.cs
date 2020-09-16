using System;
using ChatApp.Service.Models.User;

namespace ChatApp.Service.Models.Message
{
    public class MessageModel
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public int UserId { get; set; }
        public UserModel User { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}