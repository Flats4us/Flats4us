using Flats4us.Entities;
using Flats4us.Helpers.Enums;
using Flats4us.Services;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using System.Security.Claims;

namespace Flats4us.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatService _chatService;
        private readonly IGroupChatService _groupChatService;
        public readonly Flats4usContext _context;
        private readonly INotificationService _notificationService;

        private readonly static ConcurrentDictionary<int, List<string>> _connections = new ConcurrentDictionary<int, List<string>>();

        public ChatHub(IChatService chatService,
            IGroupChatService groupChatService,
            Flats4usContext context,
            INotificationService notificationService)
        {
            _chatService = chatService;
            _groupChatService = groupChatService;
            _context = context;
            _notificationService = notificationService;
        }

        public async Task SendPrivateMessage(int receiverUserId, string message)
        {
            var senderUserId = GetUserId();
            if (!senderUserId.HasValue) return;

            await _chatService.SendMessageAsync(senderUserId.Value, receiverUserId, message);


            if (_connections.TryGetValue(receiverUserId, out var receiverConnectionIds) && receiverConnectionIds.Any())
            {
                await Clients.User(receiverUserId.ToString()).SendAsync("ReceivePrivateMessage", senderUserId, message, DateTime.UtcNow);
            }
            else
            {
                var sender = await _context.Users.FindAsync(senderUserId.Value);
                if (sender == null) return;

                var notificationTitle = sender.Name + " " + sender.Surname;
                var notificationBody = message;
                await _notificationService.SendNotificationAsync(notificationTitle, notificationBody, notificationTitle, notificationBody, receiverUserId, true);
            }
        }

        public async Task SendGroupChatMessage(int groupChatId, string message)
        {
            var senderId = GetUserId();
            if (!senderId.HasValue) return;

            var isMember = await _context.UserGroupChats.AnyAsync(ugc => ugc.UserId == senderId.Value && ugc.GroupChatId == groupChatId);
            if (!isMember) return;

            var sender = await _context.Users.FindAsync(senderId.Value);
            if (sender is null) return;

            var groupUsers = await _context.UserGroupChats
                .Where(ugc => ugc.GroupChatId == groupChatId &&
                    ugc.UserId != senderId.Value)
                .Select(ugc => ugc.UserId)
                .ToListAsync();

            await _groupChatService.SendMessageToGroupChatAsync(groupChatId, senderId.Value, message);

            foreach (var groupUserId in groupUsers)
            {
                if (_connections.TryGetValue(groupUserId, out var connections) && connections.Any())
                {
                    foreach (var connectionId in connections)
                    {
                        await Clients.Client(connectionId).SendAsync("ReceiveGroupMessage", groupChatId, senderId.Value, message, DateTime.UtcNow);
                    }
                }
                else
                {
                    var notificationTitle = sender.Name + " " + sender.Surname;
                    var notificationBody = message;
                    await _notificationService.SendNotificationAsync(notificationTitle, notificationBody, notificationTitle, notificationBody, groupUserId, true);
                }
            }
        }

        private int? GetUserId()
        {
            if (int.TryParse(Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
            { 
                return userId;
            }
            return null;
        }

        private int? GetUserIdByConnectionId(string connectionId)
        {
            foreach (var kvp in _connections)
            {
                if (kvp.Value.Contains(connectionId))
                {
                    return kvp.Key;
                }
            }

            return null;
        }

        public override async Task OnConnectedAsync()
        {
            var userId = GetUserId();
            if (userId != null)
            {
                if (!_connections.ContainsKey(userId.Value))
                {
                    _connections[userId.Value] = new List<string>();
                }

                _connections[userId.Value].Add(Context.ConnectionId);
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = GetUserId();
            if (userId != null)
            {
                if (_connections.TryGetValue(userId.Value, out var receiverConnectionIds))
                {
                    receiverConnectionIds.Remove(Context.ConnectionId);

                    if (!receiverConnectionIds.Any())
                    {
                        _connections.Remove(userId.Value, out _);
                    }
                }
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}