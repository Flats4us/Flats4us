using Flats4us.Entities;
using Flats4us.Entities.Dto;

namespace Flats4us.Services.Interfaces
{
    public interface IGroupChatService
    {
        Task<int> CreateGroupChatAsync(string groupName, IEnumerable<int> userIds);
        Task SendMessageToGroupChatAsync(int groupChatId, int userId, string message);
        Task<GroupChatDto> GetGroupChatInfoAsync(int userId, int groupChatId);
        Task<List<ChatMessageDto>> GetGroupChatHistoryAsync(int userId, int groupChatId);
        Task<List<GroupChatDto>> GetGroupChats(int userId);
        Task AddModeratorToGroupChatAsync(int groupChatId, int newUserId);
    }
}
