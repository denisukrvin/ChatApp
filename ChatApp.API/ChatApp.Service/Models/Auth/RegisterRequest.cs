using System.ComponentModel.DataAnnotations;

namespace ChatApp.Service.Models.Auth
{
    public class RegisterRequest
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(30, MinimumLength = 6)]
        public string Email { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [StringLength(20, MinimumLength = 6)]
        public string PasswordConfirm { get; set; }
    }
}