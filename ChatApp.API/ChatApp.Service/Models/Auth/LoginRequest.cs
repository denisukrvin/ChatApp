using System.ComponentModel.DataAnnotations;

namespace ChatApp.Service.Models.Auth
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        [StringLength(30, MinimumLength = 6)]
        public string Email { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string Password { get; set; }
    }
}