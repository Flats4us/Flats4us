using FirebaseAdmin.Messaging;
using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Services;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Security.Claims;

namespace Flats4us.Hubs
{
    public class NotificationHub : Hub
    {
        public readonly Flats4usContext _context;
        private readonly INotificationService _notificationService;

        private readonly static ConcurrentDictionary<int, List<string>> _connections = new ConcurrentDictionary<int, List<string>>();

        public NotificationHub(Flats4usContext context,
            INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }

        public async Task<bool> SendNotification(int receiverUserId, string keyTitle, string keyBody)
        {
            if (_connections.TryGetValue(receiverUserId, out var receiverConnectionIds) && receiverConnectionIds.Any())
            {
                await Clients.User(receiverUserId.ToString()).SendAsync("ReceiveNotification", keyTitle, keyBody);
                return true;
            }
            else return false;
        }

        private int? GetUserId()
        {
            if (int.TryParse(Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
            {
                return userId;
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
