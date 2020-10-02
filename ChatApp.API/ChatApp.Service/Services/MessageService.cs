using System;
using System.Linq;
using ChatApp.Service.Data;
using System.Collections.Generic;
using ChatApp.Service.Interfaces;
using ChatApp.Service.Models.User;
using Microsoft.EntityFrameworkCore;
using ChatApp.Service.Models.Message;
using ChatApp.Service.Models.Common.Api;

namespace ChatApp.Service.Services
{
    public class MessageService : IMessageService
    {
        public List<MessageGroupModel> All(int chatId, int lastMessageId = 0)
        {
            if (chatId == 0)
                return new List<MessageGroupModel>();

            using (var context = new DataContext())
            {
                // query
                var condition = context.message
                            .Include(m => m.user_)
                            .Where(m => m.chat_id == chatId && m.record_state != 1);

                // infinite scroll
                if (lastMessageId > 0)
                    condition = condition.Where(m => m.id < lastMessageId);

                var messagesCount = condition.Count();
                if (messagesCount >= 15)
                    condition = condition.Skip(messagesCount - 15);

                // grouping
                var groupedMessages = condition
                            .ToList()
                            .GroupBy(m => m.creation_date.Date)
                            .Select(g => new MessageGroupModel
                            {
                                Date = g.Key.ToString("yyyy-MM-dd"),
                                Messages = g.Select(m => new MessageModel
                                {
                                    Id = m.id,
                                    ChatId = m.chat_id,
                                    UserId = m.user_id,
                                    User = new UserModel { Id = m.user_.id, Avatar = m.user_.avatar, Name = m.user_.name },
                                    Text = m.text,
                                    Date = m.creation_date
                                }).ToList()
                            }).ToList();

                return groupedMessages;
            }
        }

        public MessageModel Get(int messageId)
        {
            if (messageId == 0)
                return null;

            using (var context = new DataContext())
            {
                var condition = context.message.Include(m => m.user_).Where(m => m.id == messageId && m.record_state != 1);
                return Map(condition).FirstOrDefault();
            }
        }

        public OperationResponse Create(int chatId, int userId, string text)
        {
            if (chatId == 0 || userId == 0 || string.IsNullOrWhiteSpace(text))
                return new OperationResponse { Success = false, Message = "Something went wrong, please try again later" };

            using (var context = new DataContext())
            {
                var message = new message();
                message.chat_id = chatId;
                message.user_id = userId;
                message.text = text;

                message.creation_date = DateTime.UtcNow;
                message.record_updated = DateTime.UtcNow;
                message.record_state = 0;

                context.message.Add(message);
                context.SaveChanges();

                return new OperationResponse { Success = true, Data = new Dictionary<string, object> { { "message_id", message.id } } };
            }
        }

        public static List<MessageModel> Map(IQueryable<message> messages)
        {
            return messages.Select(m => new MessageModel
            {
                Id = m.id,
                ChatId = m.chat_id,
                UserId = m.user_id,
                User = new UserModel { Id = m.user_.id, Avatar = m.user_.avatar, Name = m.user_.name },
                Text = m.text,
                Date = m.creation_date
            }).ToList();
        }
    }
}