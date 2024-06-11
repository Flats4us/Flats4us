using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Flats4us.Services
{
    public class ChatService: IChatService
    {
        public readonly Flats4usContext _context;
        private readonly IUserService _userService;
        private readonly INotificationService _notificationService;

        public ChatService(Flats4usContext context, IUserService ownerService, INotificationService notificationService)
        {
            _context = context;
            _userService = ownerService;
            _notificationService = notificationService;
        }

        public async Task<IEnumerable<ChatMessage>> GetChatHistory(int chatId)
        {
            return await _context.ChatMessages
                                 .Where(cm => cm.Chat.ChatId == chatId)
                                 .OrderBy(cm => cm.DateTime)
                                 .ToListAsync();
        }

        public async Task<int?> GetChatParticipant(int chatId, int senderUserId)
        {
            var chat = await _context.Chats.FindAsync(chatId);

            if (chat == null)
            {
                throw new ArgumentException("Chat not found.");
            }

            if (chat.StudentId == senderUserId)
            {
                return chat.StudentId; 
            } 
            else if (chat.OwnerId == senderUserId) 
            {
                return chat.OwnerId; 
            }
            else throw new ArgumentException("Failed to get other user id");
        }


        public async Task SaveMessage(ChatMessage chatMessage)
        {


            await _context.ChatMessages.AddAsync(chatMessage);
            
            await _context.SaveChangesAsync();
            // Notify the user of password change
            var sender = await _context.Users.FindAsync(chatMessage.SenderId);

            var notificationTitle = sender.Name + " " + sender.Surname;
            var notificationBody = chatMessage.Content;
            await _notificationService.SendNotificationAsync(notificationTitle, notificationBody, chatMessage.SenderId);

        }

        public async Task<Chat> EnsureChatSession(int user1Id, int user2Id)
        {
            var user1 = await _context.Students.FindAsync(user1Id);
            var user2 = await _context.Students.FindAsync(user2Id);
            if (user1 == null || user2 == null)
            {
                // Handle scenario where one or both users don't exist
                throw new ArgumentException("User not found.");
            }

            var potentiallyOwner = await _context.Owners.FindAsync(user1Id);
            if (potentiallyOwner != null)
            {
                throw new ArgumentException("Owner can't start conversations.");
            }

            // Check if a chat session already exists (regardless of who initiated it)
            var chat = await _context.Chats
                                     .FirstOrDefaultAsync(c => (c.StudentId == user1Id && c.OwnerId == user2Id) ||
                                                               (c.StudentId == user2Id && c.OwnerId == user1Id));

            // If the chat session doesn't exist, create a new one
            if (chat == null)
            {
                chat = new Chat
                {
                    StudentId = user1Id,
                    OwnerId = user2Id
                };

                _context.Chats.Add(chat);
                await _context.SaveChangesAsync();
            }

            return chat;
        }

        public async Task<List<ChatInfoDto>> GetUserChatsAsync(int userId)
        {
            var userChats = new List<ChatInfoDto>();

            // Fetch private chats
            var privateChats = await _context.Chats
                .Where(c => c.StudentId == userId || c.OwnerId == userId)
                .ToListAsync();

            foreach (var chat in privateChats)
            {
                var otherUserId = 0;
                if (chat.OwnerId == userId) { otherUserId = chat.StudentId; } else { otherUserId = chat.OwnerId; }
                var otherUser = _context.Users.SingleOrDefault(o => o.UserId == otherUserId);
                
                userChats.Add(new ChatInfoDto
                {
                    ChatId = chat.ChatId,
                    OtherUserId = otherUser.UserId,
                    OtherUsername = otherUser.Email
                });
            }

            
            return userChats;
        }



    }
}
