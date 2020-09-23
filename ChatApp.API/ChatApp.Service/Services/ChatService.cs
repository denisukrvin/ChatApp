using System;
using System.Linq;
using ChatApp.Service.Data;
using ChatApp.Service.Interfaces;
using System.Collections.Generic;
using ChatApp.Service.Models.User;
using ChatApp.Service.Models.Chat;
using Microsoft.EntityFrameworkCore;
using ChatApp.Service.Models.Common.Api;

namespace ChatApp.Service.Services
{
    public class ChatService : IChatService
    {
        public List<ChatModel> All(int userId)
        {
            if (userId == 0)
                return new List<ChatModel>();

            using (var context = new DataContext())
            {
                var condition = context.chat
                    .Include(c => c.first_member_)
                    .Include(c => c.second_member_)
                    .Where(c => c.first_member_id == userId || c.second_member_id == userId && c.record_state != 1);
                return Map(condition);
            }
        }

        public OperationResponse Create(int firstMemberId, int secondMemberId)
        {
            if (firstMemberId == 0 || secondMemberId == 0 || firstMemberId == secondMemberId)
                return new OperationResponse { Success = false, Message = "Something went wrong, please try again later" };

            using (var context = new DataContext())
            {
                var existingChat = context.chat.FirstOrDefault(c => 
                        c.first_member_id == firstMemberId && c.second_member_id == secondMemberId ||
                        c.first_member_id == secondMemberId && c.second_member_id == firstMemberId &&
                        c.record_state != 1);

                if (existingChat != null)
                    return new OperationResponse { Success = true, Data = new Dictionary<string, object> { { "chat_id", existingChat.id } } };

                var chat = new chat();
                chat.first_member_id = firstMemberId;
                chat.second_member_id = secondMemberId;

                chat.creation_date = DateTime.UtcNow;
                chat.record_updated = DateTime.UtcNow;
                chat.record_state = 0;

                context.chat.Add(chat);
                context.SaveChanges();

                return new OperationResponse { Success = true, Data = new Dictionary<string, object> { { "chat_id", chat.id } } };
            }
        }

        public static List<ChatModel> Map(IQueryable<chat> chats)
        {
            return chats.Select(c => new ChatModel
            {
                Id = c.id,
                FirstMemberId = c.first_member_id,
                FirstMember = new UserModel{ Id = c.first_member_.id, Avatar = c.first_member_.avatar, Name = c.first_member_.name },
                SecondMemberId = c.second_member_id,
                SecondMember = new UserModel{ Id = c.second_member_.id, Avatar = c.second_member_.avatar, Name = c.second_member_.name }
            }).ToList();
        }
    }
}