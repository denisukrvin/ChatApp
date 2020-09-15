using System.ComponentModel.DataAnnotations;

namespace ChatApp.Service.Models.Chat.Api
{
    public class CreateChatRequest
    {
        [Required]
        public int UserId { get; set; }
    }
}