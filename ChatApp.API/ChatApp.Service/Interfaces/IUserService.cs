using ChatApp.Service.Data;
using System.Collections.Generic;
using ChatApp.Service.Models.User;

namespace ChatApp.Service.Interfaces
{
    public interface IUserService
    {
        List<UserModel> All();
        UserModel Get(int userId);
        user Get(string userEmail);
        user CreateOrEdit(UserModel model);
    }
}