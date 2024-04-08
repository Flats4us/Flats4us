using Flats4us.Entities.Dto;
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
        public async Task<IActionResult> Get([FromQuery] PaginatorDto input)
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
    }
}
