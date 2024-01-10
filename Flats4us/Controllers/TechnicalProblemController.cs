using Flats4us.Entities.Dto;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Cors;
using Flats4us.Services;
using System.Security.Claims;

namespace Flats4us.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("technical-problems")]
    [ApiController]
    public class TechnicalProblemController : ControllerBase
    {
        private readonly ILogger<TechnicalProblemController> _logger;
        private readonly ITechnicalProblemService _technicalProblemService;

        public TechnicalProblemController(
            ILogger<TechnicalProblemController> logger,     
            ITechnicalProblemService technicalProblemService)
        {
            _logger = logger;
            _technicalProblemService = technicalProblemService;
        }

        [HttpGet]
        [Authorize(Policy = "Moderator")]
        [SwaggerOperation(
            Summary = "Returns list of technical problems",
            Description = "Requires moderator privileges"
        )]
        public async Task<IActionResult> Get([FromQuery] PaginatorDto input)
        {
            try
            {
                _logger.LogInformation("Getting Technical Problems");
                var problems = await _technicalProblemService.GetAllAsync(input);
                return Ok(problems);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"FAILED: Getting Technical Problem");
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        [HttpPost]
        [Authorize(Policy = "RegisteredUser")]
        [SwaggerOperation(
            Summary = "Adding new technical problem",
            Description = "Requires registered user privileges"
        )]
        public async Task<IActionResult> Post(AddTechnicalProblemDto input)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                    {
                        return BadRequest("Server error: Failed to get user id from request");
                    }
                _logger.LogInformation("Posting Technical Problems");
                await _technicalProblemService.PostAsync(input, requestUserId);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"FAILED: Posting Technical Problem");
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "Moderator")]
        [SwaggerOperation(
            Summary = "Updating technical problem to solved",
            Description = "Requires moderator privileges"
        )]
        public async Task<IActionResult> Put(int id)
        {
            try
            {
                _logger.LogInformation("Updating Technical Problems");
                await _technicalProblemService.PutAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"FAILED: Editing Technical Problem");
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
    }
}
