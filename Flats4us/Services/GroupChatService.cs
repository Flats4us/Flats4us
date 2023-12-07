using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Flats4us.Services
{
    public class GroupChatService : IGroupChatService
    {
        private readonly Flats4usContext _context; // Replace with your actual DbContext

        public GroupChatService(Flats4usContext context)
        {
            _context = context;
        }

        public async Task<GroupChat> CreateGroupChatAsync(string groupName, IEnumerable<int> userIds)
        {
            var groupChat = new GroupChat { Name = groupName };
            _context.GroupChats.Add(groupChat);
            await _context.SaveChangesAsync();

            foreach (var userId in userIds)
            {
                var userGroupChat = new UserGroupChat
                {
                    UserId = userId,
                    GroupChatId = groupChat.GroupChatId
                };
                _context.UserGroupChats.Add(userGroupChat);
            }

            await _context.SaveChangesAsync();
            return groupChat;
        }

        public async Task AddUserToGroupChatAsync(int groupChatId, int userId)
        {
            var userGroupChat = new UserGroupChat
            {
                UserId = userId,
                GroupChatId = groupChatId
            };
            _context.UserGroupChats.Add(userGroupChat);
            await _context.SaveChangesAsync();
        }
        
        public async Task<GroupChatDto> GetGroupChatAsync(int groupChatId)
        {
            var groupChat = await _context.GroupChats
                .Include(gc => gc.UserGroupChats)
                .ThenInclude(ugc => ugc.User)
                .FirstOrDefaultAsync(gc => gc.GroupChatId == groupChatId);

            if (groupChat == null)
                return null;

            return new GroupChatDto
            {
                GroupChatId = groupChat.GroupChatId,
                Name = groupChat.Name,
                Users = groupChat.UserGroupChats
                         .Select(ugc => new UserInfoDto
                         {
                             Id = ugc.User.UserId,
                             Username = ugc.User.Username
                             // Add other properties as needed
                         })
                         .ToList()
            };
        }
        public async Task<IEnumerable<ChatMessageDto>> GetGroupChatMessagesAsync(int groupChatId)
        {
            var messages = await _context.ChatMessages
                .Where(msg => msg.GroupChatId == groupChatId)
                .Include(msg => msg.Sender) // If you need sender info
                .Select(msg => new ChatMessageDto
                {
                    Content = msg.Content,
                    DateTime = msg.DateTime,
                    SenderUsername = msg.Sender.Username 
                })
                .ToListAsync();

            return messages;
        }

        public async Task SendMessageToGroupChatAsync(int groupChatId, int userId, string message)
        {
            var groupChat = await _context.GroupChats.FindAsync(groupChatId);
            if (groupChat == null)
            {
                throw new ArgumentException("Group chat not found.");
            }
            if (groupChatId == null)
            {
                throw new ArgumentException("A message must be associated with a chat.");
            }

            var chatMessage = new ChatMessage
            {
                // Set properties for the message, like sender ID, content, etc.
                SenderUserId = userId,
                Content = message,
                GroupChatId = groupChatId,
                DateTime = DateTime.UtcNow,

                // Set other necessary properties
            };

            _context.ChatMessages.Add(chatMessage);
            await _context.SaveChangesAsync();
        }

        public async Task SetGroupNameAsync(int groupChatId, string newName)
        {
            var groupChat = await _context.GroupChats.FindAsync(groupChatId);
            if (groupChat == null)
            {
                throw new ArgumentException("Group chat not found.");
            }

            groupChat.Name = newName;
            await _context.SaveChangesAsync();
        }

        

        // Additional implementation for other methods...
    }

}
