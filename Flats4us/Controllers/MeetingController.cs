using Flats4us.Entities.Dto;
using Flats4us.Helpers;
using Flats4us.Helpers.Exceptions;
using Flats4us.Services;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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

        // GEt: api/Meeting
        [HttpGet]
        [Authorize(Policy = "VerifiedOwnerOrStudent")]
        [SwaggerOperation(
            Summary = "Returns list of meetings for current user",
            Description = "Requires verified owner or verified student privileges"
        )]
        public async Task<IActionResult> GetMeetingsForCurrentUser()
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                var meetings = await _meetingService.GetMeetingsForCurrentUserAsync(requestUserId);
                _logger.LogInformation($"Getting meetings for current user: {requestUserId}");
                return Ok(meetings);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"FAILED: Getting properties for current user");
                return BadRequest($"An error occurred: {ex.Message} | {ex.InnerException?.Message}");
            }
        }

        // POST: api/Meeting
        [HttpPost]
        [Authorize(Policy = "VerifiedOwner")]
        [SwaggerOperation(
            Summary = "Adds a new meeting",
            Description = "Requires verified owner privileges"
        )]
        public async Task<IActionResult> AddMeeting([FromBody] AddMeetingDto input)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                await _meetingService.AddMeetingAsync(input, requestUserId);
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
