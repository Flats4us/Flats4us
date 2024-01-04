using Flats4us.Entities.Dto;
using Flats4us.Services;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Flats4us.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/equipment")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentService _equipmentService;
        private readonly ILogger<EquipmentController> _logger;

        public EquipmentController(
            IEquipmentService equipmentService,
            ILogger<EquipmentController> logger)
        {
            _equipmentService = equipmentService;
            _logger = logger;
        }

        // GET: api/equipment
        [HttpGet]
        [SwaggerOperation(
            Summary = "Returns list of equipment"
        )]
        public async Task<IActionResult> GetAll([FromQuery] string? name = null)
        {
            try
            {
                var equipment = await _equipmentService.GetAll(name);
                return Ok(equipment);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
    }
}
