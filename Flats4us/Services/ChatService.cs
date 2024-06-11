using AutoMapper;
using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Helpers.Enums;
using Flats4us.Helpers.Exceptions;
using Flats4us.Hubs;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;

namespace Flats4us.Services
{
    public class ChatService : IChatService
    {
        public readonly Flats4usContext _context;
        private readonly IMapper _mapper;
        private readonly IHubContext<ChatHub> _chatHub;
        private readonly INotificationService _notificationService;

        public ChatService(Flats4usContext context,
            IMapper mapper,
            IHubContext<ChatHub> chatHub,
            INotificationService notificationService)
        {
            _context = context;
            _mapper = mapper;
            _chatHub = chatHub;
            _notificationService = notificationService;
        }

        public async Task<List<ChatMessageDto>> GetChatHistoryAsync(int chatId, int requestUserId)
        {
            var chat = await _context.Chats.FindAsync(chatId);

            if (chat == null) throw new ArgumentException("Chat not found");

            if (chat.User1Id != requestUserId && chat.User2Id != requestUserId) throw new ForbiddenException("You do not have access to this chat");

            return await _context.ChatMessages
                .Where(cm => cm.ChatId == chatId)
                .OrderBy(cm => cm.DateTime)
                .Select(cm => _mapper.Map<ChatMessageDto>(cm))
                .ToListAsync();
        }

        public async Task<int?> GetChatParticipantAsync(int chatId, int senderUserId)
        {
            var chat = await _context.Chats.FindAsync(chatId);

            if (chat == null)
            {
                throw new ArgumentException("Chat not found.");
            }

            if (chat.User1Id != senderUserId && chat.User2Id != senderUserId)
                throw new ForbiddenException("You do not have access to this chat");

            if (chat.User1Id == senderUserId)
            {
                return chat.User2Id;
            }
            else if (chat.User2Id == senderUserId)
            {
                return chat.User2Id;
            }
            else return null;
        }

        public async Task SendMessageAsync(int senderId, int receiverId, string message)
        {
            var chatId = await EnsureChatSessionAsync(senderId, receiverId);

            await SaveMessageAsync(chatId, senderId, message);

            var sender = await _context.Users.FindAsync(senderId);

            var notificationTitle = sender.Name + " " + sender.Surname;
            var notificationBody = message;
            await _notificationService.SendNotificationAsync(notificationTitle, notificationBody, receiverId);

            await _chatHub.Clients.User(receiverId.ToString()).SendAsync("ReceiveMessage", message);
        }

        public async Task<List<ChatInfoDto>> GetUserChatsAsync(int userId)
        {
            return await _context.Chats
                .Where(c => c.User1Id == userId || c.User2Id == userId)
                .Select(c => new ChatInfoDto
                {
                    ChatId = c.ChatId,
                    OtherUserId = c.User1Id == userId ? c.User2Id : c.User1Id,
                    OtherUsername = c.User1Id == userId ? c.User2.Email : c.User1.Email
                })
                .ToListAsync();
        }

        private async Task<int> EnsureChatSessionAsync(int user1Id, int user2Id)
        {
            var user1 = await _context.Users.FindAsync(user1Id);
            var user2 = await _context.Users.FindAsync(user2Id);
            
            if (user1 == null || user2 == null)
            {
                throw new ArgumentException("User not found.");
            }

            var chat = await _context.Chats.FirstOrDefaultAsync(c => (c.User1Id == user1Id && c.User2Id == user2Id) ||
                                                               (c.User1Id == user2Id && c.User2Id == user1Id));

            if (chat == null)
            {
                chat = new Chat
                {
                    User1Id = user1Id,
                    User2Id = user2Id
                };

                _context.Chats.Add(chat);
                await _context.SaveChangesAsync();
            }

            return chat.ChatId;
        }
        
        private async Task SaveMessageAsync(int chatId, int senderId, string message)
        {
            var chatMessage = new ChatMessage
            {
                Content = message,
                DateTime = DateTime.UtcNow,
                SenderId = senderId,
                ChatId = chatId
            };

            await _context.ChatMessages.AddAsync(chatMessage);
            
            await _context.SaveChangesAsync();
        }
    }
}
