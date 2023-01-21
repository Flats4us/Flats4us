using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Flats4us.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/tenant")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly ITenantService _tenantService;

        public TenantController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        // GET
        [HttpGet]
        public async Task<IActionResult> GetTenantsList()
        {
            var tenants = await _tenantService.GetAllTenantsAsync();

            return Ok(tenants);
        }

        // GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTenantById(int id)
        {
            var tenant = await _tenantService.GetTenantByIdAsync(id);
            if (tenant == null) 
                return BadRequest("Tenant not found");
            else
                return Ok(tenant);
        }
        
        // INSERT
        [HttpPost]
        public async Task<IActionResult> AddTenant(TenantDTO body)
        {
            var result = await _tenantService.AddTenantAsync(body);
            if (result == 1)
                return Created("", "");
            else
                return Conflict();
        }
        
        // UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTenant(int id, [FromBody] TenantDTO body)
        {
            var tenant = await _tenantService.UpdateTenantAsync(id, body);
            if (tenant is null)
                return BadRequest("Tenant not found");
            else
                return Ok();
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTenant(int id)
        {
            var tenant = await _tenantService.DeleteTenantAsync(id);
            if (tenant is null)
                return BadRequest("Tenant not found");
            else
                return Ok();
        }
    }
}
