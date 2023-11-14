using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Flats4us.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgrumentInterventionController : ControllerBase
    {
        private readonly ILogger<AgrumentInterventionController> _logger;
        private readonly IArgumentInterventionService _argumentInterventionService;

        public AgrumentInterventionController(
            ILogger<AgrumentInterventionController> logger, 
            IArgumentInterventionService argumentInterventionService)
        {
            _logger = logger;
            _argumentInterventionService = argumentInterventionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetArgumentInterventions()
        {
            _logger.LogInformation("Getting ArgumentInterventions");
            var ArgumentInterventions = await _argumentInterventionService.GetAllAsync();

            return Ok(ArgumentInterventions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetArgumentInterventionById(int id)
        {
            _logger.LogInformation("Getting ArgumentIntervention by ID");
            var argumentIntervention = await _argumentInterventionService.GetById(id);

            return Ok(argumentIntervention);
        }

        [HttpPost]
        public async Task<IActionResult> PostArgumentIntervention(ArgumentInterventionDto input)
        {

            try
            {
                _logger.LogInformation("Adding ArgumentIntervention");
                await _argumentInterventionService.AddArgumentInterventionAsync(input);
                return Ok("dodano interwencję");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"FAILED: Adding offar - body: {input}");
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }








    }
}
