using AutoMapper;
using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Helpers.Exceptions;
using Flats4us.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Flats4us.Services
{
    public class GroupChatService : IGroupChatService
    {
        private readonly Flats4usContext _context;
        private readonly IMapper _mapper;

        public GroupChatService(Flats4usContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreateGroupChatAsync(string groupName, IEnumerable<int> userIds)
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

            return groupChat.GroupChatId;
        }
        
        public async Task<GroupChatDto> GetGroupChatInfoAsync(int userId, int groupChatId)
        {
            var isUserMember = await _context.UserGroupChats.AnyAsync(ugc => ugc.UserId == userId && ugc.GroupChatId == groupChatId);
            
            if (!isUserMember) throw new ForbiddenException("The user is not part of the group chat.");
            
            var groupChat = await _context.GroupChats
                .Include(gc => gc.UserGroupChats)
                    .ThenInclude(ugc => ugc.User)
                .FirstOrDefaultAsync(gc => gc.GroupChatId == groupChatId);

            if (groupChat == null) throw new ArgumentException("Group chat with given id not found");

            return _mapper.Map<GroupChatDto>(groupChat);
        }

        public async Task<List<GroupChatDto>> GetGroupChats(int userId)
        {
            return await _context.UserGroupChats
                .Where(ugc => ugc.UserId == userId)
                .Include(ugc => ugc.GroupChat)
                .ThenInclude(gc => gc.UserGroupChats)
                    .ThenInclude(ugc => ugc.User)
                .Select(ugc => ugc.GroupChat)
                .Select(gc => _mapper.Map<GroupChatDto>(gc))
                .ToListAsync();
        }

        public async Task<List<ChatMessageDto>> GetGroupChatHistoryAsync(int userId,int groupChatId)
        {
            var groupChat = await _context.GroupChats.FindAsync(groupChatId);

            if (groupChat == null) throw new ArgumentException("Group chat not found.");

            var isMember = await _context.UserGroupChats.AnyAsync(ugc => ugc.UserId == userId && ugc.GroupChatId == groupChatId);

            if (!isMember) throw new ForbiddenException("User is not a member of the group chat.");

            return await _context.ChatMessages
                .Where(msg => msg.GroupChatId == groupChatId)
                .OrderBy(cm => cm.DateTime)
                .Select(cm => _mapper.Map<ChatMessageDto>(cm))
                .ToListAsync();
        }

        public async Task SendMessageToGroupChatAsync(int groupChatId, int userId, string message)
        {
            var groupChat = await _context.GroupChats.FindAsync(groupChatId);

            if (groupChat == null) throw new ArgumentException("Group chat not found.");

            var isMember = await _context.UserGroupChats.AnyAsync(ugc => ugc.UserId == userId && ugc.GroupChatId == groupChatId);

            if (!isMember) throw new ForbiddenException("User is not a member of the group chat.");

            var chatMessage = new ChatMessage
            {
                Content = message,
                DateTime = DateTime.UtcNow,
                SenderId = userId,
                GroupChatId = groupChatId,
            };

            _context.ChatMessages.Add(chatMessage);
            await _context.SaveChangesAsync();
        }

        public async Task AddModeratorToGroupChatAsync(int groupChatId, int newUserId)
        {
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
    }

}
