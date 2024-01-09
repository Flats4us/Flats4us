using Flats4us.Entities.Dto;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("get")]
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

        [HttpPut("put")]
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

        [HttpPost("post")]
        public async Task<IActionResult> Post(int id)
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

        //[HttpDelete("delete")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    try
        //    {
        //        _logger.LogInformation("Posting Technical Problems");
        //        await _technicalProblemService.Delete(id);
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogInformation($"FAILED: Editing argument");
        //        return BadRequest($"An error occurred: {ex.InnerException.Message}");
        //    }
        //}






    }
}
