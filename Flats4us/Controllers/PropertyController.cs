using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Helpers.Exceptions;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

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

        // GET: api/Property
        [HttpGet]
        [Authorize(Policy = "VerifiedOwner")]
        [SwaggerOperation(
            Summary = "Returns list of properties for current owner",
            Description = "Requires verified owner privileges"
        )]
        public async Task<IActionResult> GetPropertiesForCurrentUser()
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                var properties = await _propertyService.GetPropertiesForCurrentUserAsync(requestUserId);
                _logger.LogInformation($"Getting properties for current user: {requestUserId}");
                return Ok(properties);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"FAILED: Getting properties for current user");
                return BadRequest($"An error occurred: {ex.Message} | {ex.InnerException?.Message}");
            }
        }

        // POST: api/Property
        [HttpPost]
        [Authorize(Policy = "VerifiedOwner")]
        [SwaggerOperation(
            Summary = "Adds new property",
            Description = "Requires verified owner privileges"
        )]
        public async Task<IActionResult> AddProperty([FromForm] AddEditPropertyDto input)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                await _propertyService.AddPropertyAsync(input, requestUserId);
                _logger.LogInformation($"Adding property - body: {input}");
                return Ok("Property added successfully");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"FAILED: Adding property - body: {input}");
                return BadRequest($"An error occurred: {ex.Message} | {ex.InnerException?.Message}");
            }
        }

        // PUT: api/Property/{id}
        [HttpPut("{id}")]
        [Authorize(Policy = "VerifiedOwner")]
        [SwaggerOperation(
            Summary = "Updates existing property",
            Description = "Requires verified owner privileges"
        )]
        public async Task<IActionResult> UpdateProperty(int id, [FromForm] AddEditPropertyDto input)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                await _propertyService.UpdatePropertyAsync(id, input, requestUserId);
                _logger.LogInformation($"Updating property - id: {id}");
                return Ok("Property updated successfully");
            }
            catch (ForbiddenException ex)
            {
                return StatusCode(403, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"FAILED: Updating property - id: {id}");
                return BadRequest($"An error occurred: {ex.Message}");
            }

        }

        // DELETE: api/Property/{id}
        [HttpDelete("{id}")]
        [Authorize(Policy = "VerifiedOwner")]
        [SwaggerOperation(
            Summary = "Removes property",
            Description = "Requires verified owner privileges"
        )]
        public async Task<IActionResult> DeleteProperty(int id)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                await _propertyService.DeletePropertyAsync(id, requestUserId);
                _logger.LogInformation($"Property with ID {id} has been deleted.");
                return Ok("Property deleted successfully");
            }
            catch (ForbiddenException ex)
            {
                return StatusCode(403, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"FAILED: Deleting property with ID {id}");
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
    }
}
