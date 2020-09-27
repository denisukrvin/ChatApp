using System;
using System.Linq;
using ChatApp.Service.Data;
using System.Collections.Generic;
using ChatApp.Service.Interfaces;
using ChatApp.Service.Models.Profile;
using ChatApp.Service.Models.Common.Api;

namespace ChatApp.Service.Services
{
    public class ProfileService : IProfileService
    {
        public ProfileModel Get(int userId)
        {
            if (userId == 0)
                return null;

            using (var context = new DataContext())
            {
                var condition = context.user.Where(u => u.id == userId && u.record_state != 1);
                return Map(condition).FirstOrDefault();
            }
        }

        public OperationResponse Edit(int userId, ProfileModel model)
        {
            if (userId == 0 || model == null)
                return new OperationResponse { Success = false, Message = "Something went wrong, please try again later" };

            using (var context = new DataContext())
            {
                var user = context.user.FirstOrDefault(u => u.id == userId && u.record_state != 1);
                if (user == null)
                    return new OperationResponse { Success = false, Message = "User is not found" };
                
                user.name = model.Name;
                user.description = model.Description;

                if (!string.IsNullOrWhiteSpace(model.Avatar))
                    user.avatar = model.Avatar;

                user.record_updated = DateTime.UtcNow;
                user.record_state = 2;
                
                context.SaveChanges();
                return new OperationResponse { Success = true, Message = "Successfully updated" };
            }
        }

        public static List<ProfileModel> Map(IQueryable<user> users)
        {
            return users.Select(p => new ProfileModel
            {
                Avatar = p.avatar,
                Name = p.name,
                Description = p.description
            }).ToList();
        }
    }
}