using AutoMapper;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Hubs;
using Flats4us.Services.Interfaces;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Flats4us.Services
{
    public class NotificationService : INotificationService
    {
        private readonly FirebaseMessaging _messaging;
        private readonly Flats4usContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHubContext<ChatHub> _chatHubContext;
        public NotificationService(Flats4usContext context, IConfiguration configuration, FirebaseApp firebaseApp, IHubContext<ChatHub> chatHubContext)
        {
            _context = context;
            _configuration = configuration;
            _chatHubContext = chatHubContext;
            var serviceAccountKeyPath = _configuration["Firebase:ServiceAccountKeyPath"];

            var credential = GoogleCredential.FromFile(serviceAccountKeyPath);
            
            _messaging = FirebaseMessaging.GetMessaging(firebaseApp);
        }

        public async Task<List<AlertDto>> GetUnreadAlertAsync(int userId)
        {
            var unreadAlerts = await _context.Alerts
                                     .Where(a => a.UserId == userId && !a.Read)
                                     .ToListAsync();

            // Convert Alert entities to AlertDto manually
            var unreadAlertDtos = unreadAlerts.Select(alert => new AlertDto
            {
                AlertId = alert.AlertId,
                AlertName = alert.AlertName,
                AlertBody = alert.AlertBody,
                DateTime = alert.DateTime,
                Read = alert.Read,
            }).ToList();

            return unreadAlertDtos;
        }
        public async Task<List<AlertDto>> GetAllAlertAsync(int userId)
        {
            var alerts = await _context.Alerts
                                     .Where(a => a.UserId == userId)
                                     .ToListAsync();

            // Convert Alert entities to AlertDto manually
            var alertDtos = alerts.Select(alert => new AlertDto
            {
                AlertId = alert.AlertId,
                AlertName = alert.AlertName,
                AlertBody = alert.AlertBody,
                DateTime = alert.DateTime,
                Read = alert.Read,
            }).ToList();

            return alertDtos;
        }

        public async Task<bool> ReadAlertAsync(List<int> alertIds, int userId)
        {
            // Retrieve alerts with the provided alertIds from the database
            var alerts = await _context.Alerts
                                .Where(a => alertIds.Contains(a.AlertId) && a.UserId == userId).ToListAsync();

            if (alerts == null || !alerts.Any())
            {
                // No alerts found with the provided alertIds
                return false;
            }

            // Mark the retrieved alerts as read
            foreach (var alert in alerts)
            {
                alert.Read = true;
            }

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Return true to indicate that the alerts were successfully marked as read
            return true;
        }

        public async Task<bool> SendNotificationAsync(string title, string body, int userId, bool chatNotification)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null || string.IsNullOrEmpty(user.FcmToken))
                return false; // User not found or FCM token not available

            if (chatNotification && !user.PushChatConsent)
            {
                return false; // User has not consented to chat notifications
            }

            if (!chatNotification && !user.PushPropertyConsent)
            {
                return false; // User has not consented to property notifications
            }
            if (!chatNotification)
            {
                var alert = new Alert
                {
                    AlertBody = body,
                    AlertName = title,
                    DateTime = DateTime.UtcNow,
                    Read = false,
                    UserId = userId
                };
                var alertDto = new AlertDto
                {
                    AlertBody = body,
                    AlertName = title,
                    DateTime = DateTime.UtcNow,
                    Read = false,
                    
                };

                _context.Alerts.Add(alert);
                await _chatHubContext.Clients.User(userId.ToString()).SendAsync("ReceiveNotification", alertDto);
                await _context.SaveChangesAsync();
                return true;
            }


            var message = new Message()
            {
                Token = user.FcmToken,
                Notification = new Notification
                {
                    Title = title,
                    Body = body
                }
            };


            try
            {
                var response = await _messaging.SendAsync(message);
                return true; // Notification sent successfully
            }
            catch (FirebaseAdmin.Messaging.FirebaseMessagingException)
            {
                return false; // Failed to send notification
            }
        }
    }
    


}
