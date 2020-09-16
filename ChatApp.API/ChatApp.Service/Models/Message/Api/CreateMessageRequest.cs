using System.ComponentModel.DataAnnotations;

namespace ChatApp.Service.Models.Message.Api
{
    public class CreateMessageRequest
    {
        [Required]
        public int ChatId { get; set; }
        [Required]
        [StringLength(2000, MinimumLength = 1)]
        public string Text { get; set; }
    }
}