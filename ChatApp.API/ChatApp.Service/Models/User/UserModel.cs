using System;

namespace ChatApp.Service.Models.User
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Avatar { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}