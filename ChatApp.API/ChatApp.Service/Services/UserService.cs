using System;
using System.Linq;
using ChatApp.Service.Data;
using ChatApp.Service.Tools;
using System.Collections.Generic;
using ChatApp.Service.Interfaces;
using ChatApp.Service.Models.User;

namespace ChatApp.Service.Services
{
    public class UserService : IUserService
    {
        public List<UserModel> All(int currentUserId = 0)
        {
            using (var context = new DataContext())
            {
                var condition = context.user.Where(u => u.record_state != 1);
                // skip current user
                if (currentUserId > 0)
                    condition = condition.Where(u => u.id != currentUserId);

                return Map(condition);
            }
        }

        public UserModel Get(int userId)
        {
            if (userId == 0)
                return null;

            using (var context = new DataContext())
            {
                var condition = context.user.Where(u => u.id == userId && u.record_state != 1);
                return Map(condition).FirstOrDefault();
            }
        }

        public user Get(string userEmail)
        {
            if (string.IsNullOrWhiteSpace(userEmail))
                return null;

            using (var context = new DataContext())
            {
                return context.user.FirstOrDefault(u => u.email == userEmail && u.record_state != 1);
            }
        }

        public user CreateOrEdit(UserModel model)
        {
            using (var context = new DataContext())
            {
                user user = null;

                if (model.Id != 0)
                    user = context.user.FirstOrDefault(u => u.id == model.Id && u.record_state != 1);
                else
                    user = new user();

                if (user == null)
                    return null;

                user.name = model.Name;
                user.email = model.Email;

                if (!string.IsNullOrWhiteSpace(model.Password))
                    user.password = SecurePasswordHasher.Hash(model.Password);

                user.record_updated = DateTime.UtcNow;
                user.record_state = model.Id != 0 ? 2 : 0;

                if (model.Id == 0)
                {
                    user.creation_date = DateTime.UtcNow;
                    context.user.Add(user);
                }

                context.SaveChanges();
                return user;
            }
        }

        public static List<UserModel> Map(IQueryable<user> users)
        {
            return users.Select(u => new UserModel
            {
                Id = u.id,
                Name = u.name,
                Email = u.email
            }).ToList();
        }
    }
}