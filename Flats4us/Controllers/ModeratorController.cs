using Flats4us.Entities.Dto;
using Flats4us.Helpers.Enums;
using Flats4us.Services;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Flats4us.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/moderator")]
    [ApiController]
    public class ModeratorController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPropertyService _propertyService;
        private readonly IArgumentService _argumentService;
        private readonly ILogger<ModeratorController> _logger;


        public ModeratorController(
            IUserService userService,
            IPropertyService propertyService,
            IArgumentService argumentService,
            ILogger<ModeratorController> logger)
        {
            _userService = userService;
            _propertyService = propertyService;
            _argumentService = argumentService;
            _logger = logger;
        }

        // GET: api/moderator/properties
        [HttpGet("properties")]
        [Authorize(Policy = "Moderator")]
        [SwaggerOperation(
            Summary = "Returns a list of properties requiring verification",
            Description = "Requires moderator privileges"
        )]
        public async Task<IActionResult> GetNotVerifiedProperties([FromQuery] PaginatorDto input)
        {
            try
            {
                var notVerifiedProperties = await _propertyService.GetNotVerifiedPropertiesAsync(input);
                return Ok(notVerifiedProperties);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        // PUT: api/moderator/properties/{id}/verify
        [HttpPut("properties/{id}/verify")]
        [Authorize(Policy = "Moderator")]
        [SwaggerOperation(
            Summary = "Verifies property",
            Description = "Requires moderator privileges"
        )]
        public async Task<IActionResult> VerifyProperty(int id, [FromBody] AcceptDto input)
        {
            try
            {
                await _propertyService.VerifyPropertyAsync(id, input.Decision);
                _logger.LogInformation($"Verifying property - id: {id}");
                return Ok(new OutputDto<string>("Property verification status changed successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"FAILED: Verifying property - id: {id}");
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        // GET: api/moderator/users
        [HttpGet("users")]
        [Authorize(Policy = "Moderator")]
        [SwaggerOperation(
            Summary = "Returns a list of users requiring verification",
            Description = "Requires moderator privileges"
        )]
        public async Task<IActionResult> GetNotVerifiedUsers([FromQuery] PaginatorDto input)
        {
            try
            {
                var notVerifiedUsers = await _userService.GetNotVerifiedUsersAsync(input);
                return Ok(notVerifiedUsers);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        // PUT: api/moderator/users/{id}/verify
        [HttpPut("users/{id}/verify")]
        [Authorize(Policy = "Moderator")]
        [SwaggerOperation(
            Summary = "Verifies user",
            Description = "Requires moderator privileges"
        )]
        public async Task<IActionResult> VerifyUser(int id, [FromBody] AcceptDto input)
        {
            try
            {
                await _userService.VerifyUserAsync(id, input.Decision);
                _logger.LogInformation($"Verifying user - id: {id}");
                return Ok(new OutputDto<string>("User verification status changed successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"FAILED: Verifying user - id: {id}");
                return BadRequest($"An error occurred: {ex.Message}");
            }
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
            var arguments = await _argumentService.GetArgumentsAsync();

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
    }
}
