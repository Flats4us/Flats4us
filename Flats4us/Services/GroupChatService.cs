using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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

        public async Task AddUserToGroupChatAsync(int adderId, int groupChatId, int newUserId)
        {
            var isAdderMember = await _context.UserGroupChats.AnyAsync(ugc => ugc.UserId == adderId && ugc.GroupChatId == groupChatId);
            if (!isAdderMember)
            {
                throw new InvalidOperationException("The user attempting to add a new member is not part of the group chat.");
            }

            // Check if the new user is already a member of the group chat
            var isNewUserAlreadyMember = await _context.UserGroupChats.AnyAsync(ugc => ugc.UserId == newUserId && ugc.GroupChatId == groupChatId);
            if (isNewUserAlreadyMember)
            {
                throw new InvalidOperationException("The user is already a member of the group chat.");
            }

            var userGroupChat = new UserGroupChat
            {
                UserId = newUserId,
                GroupChatId = groupChatId
            };
            _context.UserGroupChats.Add(userGroupChat);
            await _context.SaveChangesAsync();
        }
        
        public async Task<GroupChatDto> GetGroupChatAsync(int userId, int groupChatId)
        {
            var isAdderMember = await _context.UserGroupChats.AnyAsync(ugc => ugc.UserId == userId && ugc.GroupChatId == groupChatId);
            if (!isAdderMember)
            {
                throw new InvalidOperationException("The user is not part of the group chat.");
            }
            
            
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
                             Username = ugc.User.Email
                             // Add other properties as needed
                         })
                         .ToList()
            };
        }

        public async Task<List<GroupChatDto>> GetGroupChats(int userId)
        {

            // Fetch private chats
            var groupChats = await _context.UserGroupChats
        .Include(ugc => ugc.GroupChat)
        .ThenInclude(gc => gc.UserGroupChats)
        .ThenInclude(ugc => ugc.User)
        .Where(ugc => ugc.UserId == userId)
        .Select(ugc => new GroupChatDto
        {
            GroupChatId = ugc.GroupChat.GroupChatId,
            Name = ugc.GroupChat.Name,
            Users = ugc.GroupChat.UserGroupChats.Select(member => new UserInfoDto
            {
                Id = member.User.UserId,
                Username = member.User.Email
            }).ToList()
        })
        .ToListAsync();

            return groupChats;
        }
        public async Task<IEnumerable<ChatMessageDto>> GetGroupChatMessagesAsync(int userId,int groupChatId)
        {
            var groupChat = await _context.GroupChats.FindAsync(groupChatId);
            if (groupChat == null)
            {
                throw new ArgumentException("Group chat not found.");
            }
            var isMember = await _context.UserGroupChats.AnyAsync(ugc => ugc.UserId == userId && ugc.GroupChatId == groupChatId);
            if (!isMember)
            {
                throw new InvalidOperationException("User is not a member of the group chat.");
            }
            var messages = await _context.ChatMessages
                .Where(msg => msg.GroupChatId == groupChatId)
                .Include(msg => msg.Sender) // If you need sender info
                .Select(msg => new ChatMessageDto
                {
                    Content = msg.Content,
                    DateTime = msg.DateTime,
                    SenderUsername = msg.Sender.Email 
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
            var isMember = await _context.UserGroupChats.AnyAsync(ugc => ugc.UserId == userId && ugc.GroupChatId == groupChatId);
            if (!isMember)
            {
                throw new InvalidOperationException("User is not a member of the group chat.");
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

        public async Task SetGroupNameAsync(int userId, int groupChatId, string newName)
        {
            var groupChat = await _context.GroupChats.FindAsync(groupChatId);
            if (groupChat == null)
            {
                throw new ArgumentException("Group chat not found.");
            }
            var isMember = await _context.UserGroupChats.AnyAsync(ugc => ugc.UserId == userId && ugc.GroupChatId == groupChatId);
            if (!isMember)
            {
                throw new InvalidOperationException("User is not a member of the group chat.");
            }

            groupChat.Name = newName;
            await _context.SaveChangesAsync();
        }

        

        // Additional implementation for other methods...
    }

}
