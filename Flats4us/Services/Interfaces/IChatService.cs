using Flats4us.Entities;
using Flats4us.Entities.Dto;

namespace Flats4us.Services.Interfaces
{
    public interface IChatService
    {
        Task<Chat> EnsureChatSession(int studentId, int userId);
        Task<int?> GetChatParticipant(int chatId, int senderUserId);
        Task SaveMessage(ChatMessage chatMessage);
        Task<IEnumerable<ChatMessage>> GetChatHistory(int chatId);
        Task<List<ChatInfoDto>> GetUserChatsAsync(int userId);
    }
}
