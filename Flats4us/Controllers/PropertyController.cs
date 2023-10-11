using Flats4us.Entities.Dto;
using Flats4us.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Flats4us.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;
        private readonly ILogger<PropertyController> _logger;

        public PropertyController(
            IPropertyService propertyService,
            ILogger<PropertyController> logger)
        {
            _propertyService = propertyService;
            _logger = logger;
        }

        // INSERT
        [HttpPost]
        public async Task<IActionResult> AddProperty([FromForm] NewPropertyDto input)
        {
            try
            {
                await _propertyService.AddPropertyAsync(input);
                _logger.LogInformation($"Adding property - body: {input}");
                return Ok("Property added successfully");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"FAILED: Adding property - body: {input}");
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProperty(int id)
        {
            try
            {
                await _propertyService.DeletePropertyAsync(id);
                _logger.LogInformation($"Property with ID {id} has been deleted.");
                return Ok("Property deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"FAILED: Deleting property with ID {id}");
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
    }
}
