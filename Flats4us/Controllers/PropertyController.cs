using Flats4us.Entities.Dto;
using Flats4us.Services.Interfaces;
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

        // GET: api/Property/NotVerified
        [HttpGet("NotVerified")]
        public async Task<IActionResult> GetNotVerifiedProperties()
        {
            try
            {
                var notVerifiedProperties = await _propertyService.GetNotVerifiedPropertiesAsync();
                return Ok(notVerifiedProperties);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        // POST: api/Property
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

        //// PUT: api/Property
        //public async Task<IActionResult> UpdateProperty([FromForm] NewPropertyDto input)
        //{

        //}

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
