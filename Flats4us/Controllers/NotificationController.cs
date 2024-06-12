using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FirebaseAdmin.Messaging;
using Flats4us.Services.Interfaces;
using Flats4us.Services;
using System.Security.Claims;

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

        [HttpGet("unread")]
        public async Task<IActionResult> GetUnreadAlert()
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
                {
                    return BadRequest("Server error: Failed to get user id from token");
                }

                var result = await _notificationService.GetUnreadAlertAsync(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAlert()
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
                {
                    return BadRequest("Server error: Failed to get user id from token");
                }

                var result = await _notificationService.GetAllAlertAsync(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("read")]
        public async Task<IActionResult> ReadAlerts([FromBody] List<int> alertIds)
        {
            try
            {
                // Get the user ID from the token
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                {
                    return BadRequest("Server error: Failed to get user id from token");
                }

                
                

                // Mark the alerts as read
                bool success = await _notificationService.ReadAlertAsync(alertIds, userId);
                if (success)
                {
                    return Ok("Alerts marked as read successfully.");
                }
                else
                {
                    return BadRequest("Failed to mark alerts as read.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        public class NotificationRequest
        {
            public string Title { get; set; }
            public string Body { get; set; }
        }
    }
}

