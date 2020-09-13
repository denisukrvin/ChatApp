using ChatApp.Service.Models.User;

namespace ChatApp.Service.Models.Chat
{
    public class ChatModel
    {
        public int Id { get; set; }
        public int FirstMemberId { get; set; }
        public UserModel FirstMember { get; set; }
        public int SecondMemberId { get; set; }
        public UserModel SecondMember { get; set; }
    }
}