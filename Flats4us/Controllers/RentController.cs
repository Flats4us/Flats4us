using Flats4us.Entities.Dto;
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
    [Route("api/rent")]
    [ApiController]
    public class RentController : ControllerBase
    {
        private readonly ILogger<RentController> _logger;
        private readonly IRentService _rentService;

        public RentController(
            ILogger<RentController> logger,
            IRentService technicalProblemService)
        {
            _logger = logger;
            _rentService = technicalProblemService;
        }

        [HttpGet]
        [Authorize(Policy = "VerifiedOwnerOrStudent")]
        [SwaggerOperation(
            Summary = "Returns list of rent for current user",
            Description = "Requires verified owner or student privileges"
        )]
        public async Task<IActionResult> Get([FromQuery] OptionalPaginatorDto input)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                _logger.LogInformation($"Getting Rents for user: {requestUserId}");
                var problems = await _rentService.GetRentsForCurrentUserAsync(requestUserId, input.PageSize, input.PageNumber);
                return Ok(problems);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"FAILED: Getting Rents for current user");
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "VerifiedUser")]
        [SwaggerOperation(
            Summary = "Returns rent by id",
            Description = "Requires verified owner or student privileges"
        )]
        public async Task<IActionResult> GetRentById(int id)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                _logger.LogInformation($"Getting rent id: {id}");
                var rent = await _rentService.GetRentByIdAsync(id, requestUserId);
                return Ok(rent);
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

        [HttpGet("{rentId}/proposition")]
        [Authorize(Policy = "VerifiedOwner")]
        [SwaggerOperation(
            Summary = "Returns rent proposition by id",
            Description = "Requires verified ownerprivileges"
        )]
        public async Task<IActionResult> GetProposition(int rentId)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                _logger.LogInformation($"Getting rent proposition id: {rentId}");
                var proposition = await _rentService.GetRentPropositionAsync(rentId, requestUserId);
                return Ok(proposition);
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
