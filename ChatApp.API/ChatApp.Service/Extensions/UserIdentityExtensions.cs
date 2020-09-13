using Microsoft.AspNetCore.Http;
using ChatApp.Service.Models.User;

namespace ChatApp.Service.Extensions
{
    public static class UserIdentityExtensions
    {
        public static int GetUserId(this HttpContext context)
        {
            var user = (UserModel) context.Items["User"];
            return user.Id;
        }
    }
}