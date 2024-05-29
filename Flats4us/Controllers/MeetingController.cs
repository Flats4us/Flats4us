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
    [Route("api/meetings")]
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

        // GEt: api/meetings
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

        // POST: api/meetings
        [HttpPost]
        [Authorize(Policy = "VerifiedOwnerOrStudent")]
        [SwaggerOperation(
            Summary = "Adds a new meeting",
            Description = "Requires verified owner or student privileges"
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

                return Ok(new OutputDto<string>("Meeting added successfully"));
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

        // PUT: api/meetings/{id}/accept
        [HttpPut("{id}/accept")]
        [Authorize(Policy = "VerifiedOwnerOrStudent")]
        [SwaggerOperation(
            Summary = "Accept or denies meeting",
            Description = "Requires verified owner or student privileges"
        )]
        public async Task<IActionResult> AcceptMeeting([FromBody] AcceptDto input, int id)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                await _meetingService.ConfirmMeetingAsync(input.Decision, requestUserId, id);
                _logger.LogInformation($"Trying to accept meeting ID: {id}");

                return Ok(new OutputDto<string>("Meeting accepted or denied successfully"));
            }
            catch (ForbiddenException ex)
            {
                return StatusCode(403, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }


    }
}
