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
                return BadRequest($"An error occurred: {ex.InnerException.Message}");

            }
        }

        [HttpPut("acceptArgument")]
        [Authorize(Policy = "VerifiedOwner")]
        [SwaggerOperation(
            Summary = "Accepting argument on Owner side",
            Description = "Requires VerifiedOwner privileges"
        )]
        public async Task<IActionResult> AcceptingArgumentByOwner(int id)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }
                _logger.LogInformation("Accepting argument on Owner side");
                await _argumentService.AcceptArgument(id, requestUserId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.InnerException.Message}");
            }
        }

        [HttpPut("askingForIntervention/{id}")]
        [Authorize(Policy = "VerifiedOwnerOrStudent")]
        [SwaggerOperation(
            Summary = "Asking moderator for intervention",
            Description = "Requires VerifiedOwnerOrStudent privileges"
        )]
        public async Task<IActionResult> AskingForIntervention(int id)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }
                _logger.LogInformation("Accepting argument on Owner side");
                await _argumentService.AskForIntervention(id, requestUserId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.InnerException.Message}");
            }
        }
    }
}
