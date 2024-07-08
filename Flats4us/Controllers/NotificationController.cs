using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FirebaseAdmin.Messaging;
using Flats4us.Services.Interfaces;
using System.Security.Claims;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using Flats4us.Entities.Dto;

namespace Flats4us.Controllers
{
    [Route("api/notifications")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        // TODO: remove
        [HttpPost]
        [SwaggerOperation(
            Summary = "For testing"
        )]
        public async Task<IActionResult> SendNotification([FromBody] NotificationRequest request, int userId)
        {

            var success = await _notificationService.SendNotificationAsync(request.Title, request.Body, request.Title, request.Body, userId, false);

            if (success)
                return Ok("Notification sent successfully.");
            else
                return BadRequest("Failed to send notification.");
        }

        [HttpGet("unread")]
        [Authorize(Policy = "RegisteredUser")]
        [SwaggerOperation(
            Summary = "Returns a list of unread notifications",
            Description = "Requires registered user privileges"
        )]
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
        [Authorize(Policy = "RegisteredUser")]
        [SwaggerOperation(
            Summary = "Returns a list of notifications",
            Description = "Requires registered user privileges"
        )]
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
        [Authorize(Policy = "RegisteredUser")]
        [SwaggerOperation(
            Summary = "Marks notifications as read",
            Description = "Requires registered user privileges"
        )]
        public async Task<IActionResult> ReadAlerts([FromBody] ReadNotificationsDto input)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
                {
                    return BadRequest("Server error: Failed to get user id from token");
                }

                // Mark the alerts as read
                bool success = await _notificationService.ReadAlertAsync(input.NotificationIds, userId);
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


        // TODO remove after test
        [HttpGet("test/users/{userId}/connections")]
        [SwaggerOperation(
            Summary = "For testing connections"
        )]
        public async Task<IActionResult> TestConnections(int userId)
        {
            try
            {
                
                var connections = await _notificationService.GetConnectionsByUserIdAsync(userId);
                return Ok(connections);
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

