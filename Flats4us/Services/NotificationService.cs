using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Flats4us.Entities;
using Flats4us.Services.Interfaces;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Flats4us.Services
{
    public class NotificationService : INotificationService
    {
        private readonly FirebaseMessaging _messaging;
        private readonly Flats4usContext _context;
        private readonly IConfiguration _configuration;

        public NotificationService(Flats4usContext context, IConfiguration configuration, FirebaseApp firebaseApp)
        {
            _context = context;
            _configuration = configuration;
            var serviceAccountKeyPath = _configuration["Firebase:ServiceAccountKeyPath"];

            var credential = GoogleCredential.FromFile(serviceAccountKeyPath);
            
            _messaging = FirebaseMessaging.GetMessaging(firebaseApp);
        }
        public async Task<bool> SendNotificationAsync(string title, string body, int userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null || string.IsNullOrEmpty(user.FcmToken))
                return false; // User not found or FCM token not available

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
