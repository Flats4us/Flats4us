using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Helpers.Enums;
using Flats4us.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Flats4us.Services
{
    public class ChatService: IChatService
    {
        public readonly Flats4usContext _context;

        public ChatService(Flats4usContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ChatMessageDto>> GetChatHistory(int chatId)
        {
            return await _context.ChatMessages
                       .Include(cm => cm.Sender) // Include the sender to access its properties
                       .Where(cm => cm.Chat.ChatId == chatId)
                       .OrderBy(cm => cm.DateTime)
                       .Select(cm => new ChatMessageDto
                       {
                           Message = cm.Content,
                           DateTime = cm.DateTime,
                           Sender = cm.Sender.UserId.ToString(), 
                           SenderEmail = cm.Sender.Email 
                       })
                       .ToListAsync();
        }


        public async Task SaveMessage(ChatMessage chatMessage)
        {
            _context.ChatMessages.Add(chatMessage);
            await _context.SaveChangesAsync();
        }

        public async Task<Chat> EnsureChatSession(int user1Id, int user2Id)
        {
            var user1 = await _context.Students.FindAsync(user1Id);
            var user2 = await _context.Students.FindAsync(user2Id);



            

            // Check if a chat session already exists (regardless of who initiated it)
            var chat = await _context.Chats
                                     .FirstOrDefaultAsync(c => (c.StudentId == user1Id && c.OwnerId == user2Id) ||
                                                               (c.StudentId == user2Id && c.OwnerId == user1Id));

            // If the chat session doesn't exist, create a new one
            if (chat == null)
            {
                if (user1 == null)
                {
                    throw new InvalidOperationException("User1 isn't a student");
                }
                if (user2 == null)
                {
                    var potentiallyOwner = await _context.Owners.FindAsync(user2Id);
                    if (potentiallyOwner == null)
                    {
                        throw new InvalidOperationException("Second user isn't avalible");
                    }
                }

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
