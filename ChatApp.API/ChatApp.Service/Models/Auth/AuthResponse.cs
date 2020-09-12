using System;

namespace ChatApp.Service.Models.Auth
{
    public class AuthResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserToken { get; set; }
    }
}