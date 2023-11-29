using Flats4us.Entities.Dto;
using Flats4us.Helpers;
using Flats4us.Helpers.Exceptions;
using Flats4us.Services;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Flats4us.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingController : ControllerBase
    {
        private readonly IMeetingService _meetingService;
        private readonly ILogger<MeetingController> _logger;

        public MeetingController(
            IMeetingService meetingService,
            ILogger<MeetingController> logger)
        {
            _meetingService = meetingService;
            _logger = logger;
        }

        // POST: api/Meeting
        [HttpPost]
        public async Task<IActionResult> AddMeeting([FromForm] AddMeetingDto input)
        {
            try
            {
                var requestUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(requestUserId))
                {
                    return Unauthorized("User not authenticated");
                }

                if (!int.TryParse(requestUserId, out int userId))
                {
                    return BadRequest("Invalid user ID format");
                }

                await _meetingService.AddMeetingAsync(input, userId);
                _logger.LogInformation($"Adding meeting - body: {input}");
                return Ok("Meeting added successfully");
            }
            catch (ForbiddenException ex)
            {
                return StatusCode(403, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"FAILED: Adding meeting - body: {input}");
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }


    }
}
