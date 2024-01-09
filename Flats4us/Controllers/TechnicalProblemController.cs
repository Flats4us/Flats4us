using Flats4us.Entities.Dto;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Flats4us.Controllers
{
    [Route("api/[controller]")]
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
                _logger.LogInformation($"FAILED: Editing argument");
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        [HttpPost]
        [Authorize(Policy = "RegisteredUser")]
        [SwaggerOperation(
            Summary = "Adding new technical problem",
            Description = "Requires registered user privileges"
        )]
        public async Task<IActionResult> Post(TechnicalProblemDto input)
        {
            try
            {
                _logger.LogInformation("Posting Technical Problems");
                await _technicalProblemService.PostAsync(input);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"FAILED: Editing argument");
                return BadRequest($"An error occurred: {ex.InnerException.Message}");
            }
        }

        [HttpPut]
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
                _logger.LogInformation($"FAILED: Editing argument");
                return BadRequest($"An error occurred: {ex.InnerException.Message}");
            }
        }
    }
}
