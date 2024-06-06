using Flats4us.Entities.Dto;
using Flats4us.Helpers.Enums;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Flats4us.Controllers
{
    [Route("api/arguments")]
    [ApiController]
    public class ArgumentController : ControllerBase
    {
        private readonly ILogger<ArgumentController> _logger;
        private readonly IArgumentService _argumentService;

        public ArgumentController(
            ILogger<ArgumentController> logger,
            IArgumentService argumentService
        )
        {
            _logger = logger;
            _argumentService = argumentService;
        }

        [HttpGet]
        [Authorize(Policy = "VerifiedOwnerOrStudent")]
        [SwaggerOperation(
            Summary = "Getting all your arguments with interventions",
            Description = "Requires VerifiedOwnerOrStudent privileges"
        )]
        public async Task<IActionResult> GetYourArguments(ArgumentStatus argumentStatus)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }
                _logger.LogInformation("Getting your arguments");
                var arguments = await _argumentService.GetYourArgumentsAsync(requestUserId, argumentStatus);
                return Ok(arguments);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"FAILED: Getting arguments");
                return BadRequest($"An error occurred: {ex.Message} | {ex.InnerException?.Message}");

            }
        }

        [HttpPost]
        [Authorize(Policy = "VerifiedOwnerOrStudent")]
        [SwaggerOperation(
            Summary = "Adding a new agrument",
            Description = "Requires VerifiedOwnerOrStudent privileges"
        )]
        public async Task<IActionResult> PostArgument(AddArgumentDto input)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }
                _logger.LogInformation("Posting Argument");
                await _argumentService.AddArgumentAsync(input, requestUserId);
                return Ok("Argument Added");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"FAILED: Adding argument - body: {input}");
                return BadRequest($"An error occurred: {ex.Message} | {ex.InnerException?.Message}");

            }
        }

        [HttpPut("{argumentId}/owner-accept")]
        [Authorize(Policy = "VerifiedOwner")]
        [SwaggerOperation(
            Summary = "Accepting argument on Owner side",
            Description = "Requires VerifiedOwner privileges"
        )]
        public async Task<IActionResult> AcceptingArgumentByOwner(int argumentId)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }
                _logger.LogInformation("Accepting argument on Owner side");
                await _argumentService.OwnerAcceptArgument(argumentId, requestUserId);
                return Ok("Argument accepted from Owner side");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message} | {ex.InnerException?.Message}");
            }
        }

        [HttpPut("{argumentId}/student-accept")]
        [Authorize(Policy = "VerifiedStudent")]
        [SwaggerOperation(
            Summary = "Accepting argument on Student side",
            Description = "Requires VerifiedStudent privileges"
        )]
        public async Task<IActionResult> AcceptingArgumentByStudent(int argumentId)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }
                _logger.LogInformation("Accepting argument on Student side");
                await _argumentService.StudentAcceptArgument(argumentId, requestUserId);
                return Ok("Argument accepted from Student side");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message} | {ex.InnerException?.Message}");
            }
        }

        [HttpPut("{argumentId}/asking-for-intervention")]
        [Authorize(Policy = "VerifiedOwnerOrStudent")]
        [SwaggerOperation(
            Summary = "Asking moderator for intervention",
            Description = "Requires VerifiedOwnerOrStudent privileges"
        )]
        public async Task<IActionResult> AskingForIntervention(int argumentId)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }
                _logger.LogInformation("Asked moderator for intervention");
                await _argumentService.AskForIntervention(argumentId, requestUserId);
                return Ok("Asked Moderator for intervention");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message} | {ex.InnerException?.Message}");
            }
        }
    }
}
