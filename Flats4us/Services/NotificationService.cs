using AutoMapper;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Helpers;
using Flats4us.Helpers.Enums;
using Flats4us.Hubs;
using Flats4us.Services.Interfaces;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Flats4us.Services
{
    public class NotificationService : INotificationService
    {
        private readonly FirebaseMessaging _messaging;
        private readonly Flats4usContext _context;
        private readonly IHubContext<NotificationHub> _notificationHub;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public NotificationService(Flats4usContext context,
            IHubContext<NotificationHub> notificationHub,
            IEmailService emailService,
            IMapper mapper,
            IConfiguration configuration,
            FirebaseApp firebaseApp)
        {
            _context = context;
            _notificationHub = notificationHub;
            _emailService = emailService;
            _mapper = mapper;
            _configuration = configuration;
            var serviceAccountKeyPath = _configuration["Firebase:ServiceAccountKeyPath"];

            var credential = GoogleCredential.FromFile(serviceAccountKeyPath);
            
            _messaging = FirebaseMessaging.GetMessaging(firebaseApp);
        }

        public async Task<bool> SendNotificationAsync(string emailTitle, string emailBody, string keyTitle, string keyBody, int userId, bool isChat)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null) throw new ArgumentException("User not found");

            bool shouldSendPush = true;
            bool shouldSendEmail = true;

            if (isChat && !user.PushChatConsent)
            {
                shouldSendPush = false;
            }

            if (isChat && !user.EmailChatConsent)
            {
                shouldSendEmail = false;
            }

            if (!isChat && !user.PushOtherConsent)
            {
                shouldSendPush = false;
            }

            if (!isChat && !user.EmailOtherConsent)
            {
                shouldSendEmail = false;
            }

            var notification = new Entities.Notification
            {
                Title = keyTitle,
                Body = keyBody,
                DateTime = DateTime.UtcNow,
                Read = false,
                UserId = userId,
                IsChat = isChat
            };

            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();

            if (shouldSendEmail)
            {
                await _emailService.SendEmailAsync(userId, emailTitle, emailBody);
            }

            if (shouldSendPush)
            {
                var receiverConnections = await _context.Connections
                    .Where(c => c.UserId == userId && c.HubName == nameof(NotificationHub))
                    .Select(c => c.ContextConnectionId)
                    .ToListAsync();

                if (receiverConnections.Any())
                {
                    foreach (var connectionId in receiverConnections)
                    {
                        await _notificationHub.Clients.Client(connectionId).SendAsync("ReceiveNotification", keyTitle, keyBody, DateTime.UtcNow, isChat, notification.NotificationId);
                    }
                }
                else if (!string.IsNullOrEmpty(user.FcmToken))
                {
                    var fcmMessage = new Message()
                    {
                        Token = user.FcmToken,
                        Notification = new FirebaseAdmin.Messaging.Notification
                        {
                            Title = keyTitle,
                            Body = keyBody
                        },
                        Data = new Dictionary<string, string>()
                        {
                            { "isChat", isChat.ToString().ToLower() }
                        }
                    };

                    await _messaging.SendAsync(fcmMessage);
                }
            }

            return true;
        }

        public async Task<List<NotificationDto>> GetUnreadAlertAsync(int userId)
        {
            var unreadNotifications = await _context.Notifications
                                     .Where(a => a.UserId == userId && !a.Read)
                                     .Select(n => _mapper.Map<NotificationDto>(n))
                                     .ToListAsync();

            return unreadNotifications;
        }

        public async Task<List<NotificationDto>> GetAllAlertAsync(int userId)
        {
            var notifications = await _context.Notifications
                                     .Where(a => a.UserId == userId)
                                     .Select(n => _mapper.Map<NotificationDto>(n))
                                     .ToListAsync();

            return notifications;
        }

        public async Task<bool> ReadAlertAsync(List<int> alertIds, int userId)
        {
            var alerts = await _context.Notifications
                .Where(a => alertIds.Contains(a.NotificationId) && a.UserId == userId)
                .ToListAsync();

            if (alerts == null || !alerts.Any())
            {
                return false;
            }

            foreach (var alert in alerts)
            {
                alert.Read = true;
            }

            await _context.SaveChangesAsync();

            return true;
        }

        // TODO remove after test
        public async Task<List<Connection>> GetConnectionsByUserIdAsync(int userId)
        {
            var connections = await _context.Connections.Where(x => x.UserId == userId).ToListAsync();

            return connections;
        }
    }
}
