using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Flats4us.Services.Interfaces;


namespace Flats4us.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatcherController : ControllerBase
    {
        private readonly IMatcherService _matcherService;
        private readonly ILogger<MatcherController> _logger;

        public MatcherController(
            IMatcherService matcherService,
            ILogger<MatcherController> logger)
        {
            _matcherService = matcherService;
            _logger = logger;
        }

        [HttpGet("All-matches")]
        public async Task<IActionResult> GetAllMatches()
        {
            try
            {
                _logger.LogInformation("Getting Matches");
                return Ok(await _matcherService.GetAllMatches());
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"FAILED: Adding argument - body: ");
                return BadRequest($"An error occurred: {ex.InnerException.Message}");
            }
        }

        [HttpGet("possible-matches")]
        public async Task<IActionResult> GetMatches(int studentId)
        {
            try
            {
                _logger.LogInformation("Getting Matches");
                return Ok(await _matcherService.GetPotentialRoommate(studentId));
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"FAILED: Adding argument - body: {studentId}");
                return BadRequest($"An error occurred: {ex.InnerException.Message}");
            }
        }

        [HttpPost("match")]
        public async Task<IActionResult> PostMatche(int student1Id, int student2Id, bool isAccept)
        {
            try
            {
                _logger.LogInformation("Posting Argument");
                await _matcherService.AcceptStudent(student1Id, student2Id, isAccept);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"FAILED: Adding argument - body: {student1Id}");
                return BadRequest($"An error occurred: {ex.InnerException.Message}");
            }
        }



    }
}
