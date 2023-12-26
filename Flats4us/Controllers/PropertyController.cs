using Flats4us.Entities.Dto;
using Flats4us.Helpers.Exceptions;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace Flats4us.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/properties")]
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

        // GET: api/properties
        [HttpGet]
        [Authorize(Policy = "VerifiedOwner")]
        [SwaggerOperation(
            Summary = "Returns list of properties for current owner",
            Description = "Requires verified owner privileges"
        )]
        public async Task<IActionResult> GetPropertiesForCurrentUser([FromQuery] bool showOnlyVerified = false)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                var properties = await _propertyService.GetPropertiesForCurrentUserAsync(requestUserId, showOnlyVerified);
                _logger.LogInformation($"Getting properties for current user: {requestUserId}");
                return Ok(properties);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"FAILED: Getting properties for current user");
                return BadRequest($"An error occurred: {ex.Message} | {ex.InnerException?.Message}");
            }
        }

        // POST: api/properties
        [HttpPost]
        [Authorize(Policy = "VerifiedOwner")]
        [SwaggerOperation(
            Summary = "Adds new property",
            Description = "Requires verified owner privileges"
        )]
        public async Task<IActionResult> AddProperty([FromBody] AddEditPropertyDto input)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                var id = await _propertyService.AddPropertyAsync(input, requestUserId);
                _logger.LogInformation($"Adding property - body: {input}");
                return Ok(id);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"FAILED: Adding property - body: {input}");
                return BadRequest($"An error occurred: {ex.Message} | {ex.InnerException?.Message}");
            }
        }

        // POST: api/properties/{id}/files
        [HttpPost("{id}/files")]
        [Authorize(Policy = "VerifiedOwner")]
        [SwaggerOperation(
            Summary = "Adds property files for new property",
            Description = "Requires verified owner privileges"
        )]
        public async Task<ActionResult> AddPropertyFiles([FromForm] PropertyFilesDto input, int id)
        {
            try
            {
                await _propertyService.AddPropertyFilesAsync(input, id);
                return Ok("Adding property completed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/properties/{id}/file
        [HttpPost("{id}/files/{type}")]
        [Authorize(Policy = "VerifiedOwner")]
        [SwaggerOperation(
            Summary = "Adds property file",
            Description = "Requires verified owner privileges"
        )]
        public async Task<ActionResult> AddPropertyFile([FromForm] AddFileDto input, int id, string type)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                await _propertyService.AddPropertyFileAsync(input, id, requestUserId, type);
                _logger.LogInformation($"Adding file for property ID: {id}");
                return Ok("File added");
            }
            catch (ForbiddenException ex)
            {
                return StatusCode(403, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        // PUT: api/properties/{id}
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

        // DELETE: api/properties/{id}
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
