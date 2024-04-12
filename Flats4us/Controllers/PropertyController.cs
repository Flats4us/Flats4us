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

        // GET: api/properties/{id}
        [HttpGet("{id}")]
        [Authorize(Policy = "RegisteredUser")]
        [SwaggerOperation(
            Summary = "Returns property by id",
            Description = "Requires registered user privileges"
        )]
        public async Task<IActionResult> GetPropertyById(int id)
        {
            try
            {
                var property = await _propertyService.GetPropertyByIdAsync(id);
                _logger.LogInformation($"Getting property by ID: {id}");
                return Ok(property);
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"FAILED: Getting property by id");
                return BadRequest($"An error occurred: {ex.Message} | {ex.InnerException?.Message}");
            }
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
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                await _propertyService.AddPropertyFilesAsync(input, id, requestUserId);
                return Ok(new OutputDto<string>("Adding property completed"));
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

        // DELETE: api/properties/{id}/files/{fileId}
        [HttpDelete("{id}/files/{fileId}")]
        [Authorize(Policy = "VerifiedOwner")]
        [SwaggerOperation(
            Summary = "Deletes the file with the given id from the property",
            Description = "Requires verified owner privileges"
        )]
        public async Task<ActionResult> DeletePropertyFile(int id, string fileId)
        {
            try
            {
                if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int requestUserId))
                {
                    return BadRequest("Server error: Failed to get user id from request");
                }

                await _propertyService.DeletePropertyFileAsync(id, fileId, requestUserId);
                return Ok(new OutputDto<string>("Deleted property file successfully"));
            }
            catch (ForbiddenException ex)
            {
                return StatusCode(403, ex.Message);
            }
            catch (IOException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
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
                return Ok(new OutputDto<string>("Property updated successfully"));
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
                return Ok(new OutputDto<string>("Property deleted successfully"));
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
