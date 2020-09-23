using System.ComponentModel.DataAnnotations;

namespace ChatApp.Service.Models.Profile
{
    public class ProfileModel
    {
        public string Avatar { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(500, MinimumLength = 1)]
        public string Description { get; set; }
    }
}