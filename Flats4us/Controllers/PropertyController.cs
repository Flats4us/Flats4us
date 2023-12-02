using Flats4us.Entities.Dto;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        // POST: api/Property
        [HttpPost]
        public async Task<IActionResult> AddProperty([FromForm] AddEditPropertyDto input)
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

        // TODO: Add GetMyProperties

        // PUT: api/Property/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProperty(int id, [FromForm] AddEditPropertyDto input)
        {
            try
            {
                await _propertyService.UpdatePropertyAsync(id, input);
                _logger.LogInformation($"Updating property - id: {id}");
                return Ok("Property updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"FAILED: Updating property - id: {id}");
                return BadRequest($"An error occurred: {ex.Message}");
            }

        }

        // DELETE: api/Property/{id}
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
