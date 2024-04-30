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
        [Authorize(Policy = "Moderator")]
        [SwaggerOperation(
            Summary = "Returns list of arguments",
            Description = "Requires moderator privileges"
        )]
        public async Task<IActionResult> GetArguments()
        {
            _logger.LogInformation("Geting Arguments");
            var arguments = await _argumentService.GetAllArgumentsAsync();

            return Ok(arguments);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "Moderator")]
        [SwaggerOperation(
            Summary = "Returns argument by id",
            Description = "Requires moderator privileges"
        )]
        public async Task<IActionResult> GetArgumentById(int id)
        {
            _logger.LogInformation("Geting Argument by Id");
            var argument = await _argumentService.GetArgumentById(id);

            return Ok(argument);
        }

        [HttpPost]
        [Authorize(Policy = "VerifiedOwnerOrStudent")]
        [SwaggerOperation(
            Summary = "Adding a new agrument",
            Description = "Requires VerifiedOwnerOrStudent privileges"
        )]
        public async Task<IActionResult> PostArgument(ArgumentDto input)
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

        [HttpPut("status")]
        [Authorize(Policy = "Moderator")]
        [SwaggerOperation(
            Summary = "Updating argument status",
            Description = "Requires Moderator privileges"
        )]
        public async Task<IActionResult> PutArgument(int id, ArgumentStatus status)
        {
            try
            {
                _logger.LogInformation("Put Argument");
                await _argumentService.EditStatusArgumentAsync(id, status);
                return Ok("Argument Updated");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"FAILED: Editing argument");
                return BadRequest($"An error occurred: {ex.InnerException.Message}");
            }
        }

        [HttpGet("ongoing")]
        [Authorize(Policy = "Moderator")]
        [SwaggerOperation(
            Summary = "Returns filteres arguments",
            Description = "Requires Moderator privileges"
        )]
        public async Task<IActionResult> GetFilteredArguments()
        {
            _logger.LogInformation("Geting Filtered Arguments");
            var arguments = await _argumentService.GetOngoingArgumentsAsync();

            return Ok(arguments);
        }

        [HttpGet("interventions")]
        [Authorize(Policy = "Moderator")]
        [SwaggerOperation(
            Summary = "Returns list of interventions",
            Description = "Requires moderator privileges"
        )]
        public async Task<IActionResult> GetInterventions()
        {
            _logger.LogInformation("Geting Interventions");
            var interventions = await _argumentService.GetAllInterventionsAsync();

            return Ok(interventions);
        }

        [HttpGet("intervention/{id}")]
        [Authorize(Policy = "Moderator")]
        [SwaggerOperation(
            Summary = "Returns list of interventions by id",
            Description = "Requires moderator privileges"
        )]
        public async Task<IActionResult> GetInterventionsById(int id)
        {
            _logger.LogInformation("Geting Interventions by Id");
            var interventions = await _argumentService.GetInterventionById(id);

            return Ok(interventions);
        }

        [HttpPost("intervention")]
        [Authorize(Policy = "VerifiedOwnerOrStudent")]
        [SwaggerOperation(
            Summary = "Adding a new agrument",
            Description = "Requires VerifiedOwnerOrStudent privileges"
        )]
        public async Task<IActionResult> PostIntervention(ArgumentInterventionDto input)
        {
            try
            {
                _logger.LogInformation("Posting Intervention");
                await _argumentService.AddInterventionAsync(input);
                return Ok("Intervention Added");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"FAILED: Adding intervention - body: {input}");
                return BadRequest($"An error occurred: {ex.InnerException.Message}");
            }
        }
    }
}
