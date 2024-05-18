using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FirebaseAdmin.Messaging;
using Flats4us.Services.Interfaces;

namespace Flats4us.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost]
        public async Task<IActionResult> SendNotification([FromBody] NotificationRequest request, int userId)
        {

            var success = await _notificationService.SendNotificationAsync(request.Title, request.Body, userId);

            if (success)
                return Ok("Notification sent successfully.");
            else
                return BadRequest("Failed to send notification.");
        }

        public class NotificationRequest
        {
            public string Title { get; set; }
            public string Body { get; set; }
        }
    }
}

