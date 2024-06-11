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
        public readonly Flats4usContext _context;
        private readonly INotificationService _notificationService;

        private readonly static ConcurrentDictionary<int, List<string>> _connections = new ConcurrentDictionary<int, List<string>>();

        public ChatHub(IChatService chatService, 
            Flats4usContext context,
            INotificationService notificationService)
        {
            _chatService = chatService;
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
                await Clients.User(receiverUserId.ToString()).SendAsync("ReceivePrivateMessage", senderUserId, message, DateTime.Now);
            }
            else
            {
                var sender = await _context.Users.FindAsync(senderUserId.Value);
                if (sender == null) return;

                var notificationTitle = sender.Name + " " + sender.Surname;
                var notificationBody = message;
                await _notificationService.SendNotificationAsync(notificationTitle, notificationBody, receiverUserId);
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

        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has joined the group {groupName}.");
        }

        public async Task SendMessageToGroup(string groupName, string message)
        {
            await Clients.Group(groupName).SendAsync("ReceiveMessage", message);
        }

        // This method sends a message to a specific user.
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

        public async Task JoinGroupChat(int groupChatId)
        {
            //var userId = GetUserId();
            var userId = GetUserIdByConnectionId(Context.ConnectionId);

            // var userId = (Context.User.FindFirst(ClaimTypes.NameIdentifier).Value);


            if (userId == null) return;

            // Optionally, check if the user is a member of the group chat in the database
            var isMember = await _context.UserGroupChats.AnyAsync(ugc => ugc.UserId == userId && ugc.GroupChatId == groupChatId);
            if (!isMember) return;

            // Join the SignalR group
            await Groups.AddToGroupAsync(Context.ConnectionId, $"GroupChat-{groupChatId}");
        }

        public async Task SendGroupMessage(int groupChatId, string message)
        {
            var userId = GetUserId();
            if (!userId.HasValue) return;
            var isMember = await _context.UserGroupChats.AnyAsync(ugc => ugc.UserId == userId.Value && ugc.GroupChatId == groupChatId);
            if (!isMember) return;

            // Send message to the SignalR group
            await Clients.Group($"GroupChat-{groupChatId}").SendAsync("ReceiveGroupMessage", groupChatId, userId.Value, message, DateTime.UtcNow);
        }

        public async Task LeaveGroupChat(int groupChatId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"GroupChat-{groupChatId}");
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