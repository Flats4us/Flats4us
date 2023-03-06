using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Flats4us.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly ITenantService _tenantService;

        public TenantController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        // GET: api/Tenant
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var tenants = await _tenantService.GetAllTenantsAsync();

            return Ok(tenants);
        }

        // GET api/Tenant/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var tenant = await _tenantService.GetTenantByIdAsync(id);
            if (tenant == null) 
                return BadRequest("Tenant not found");
            else
                return Ok(tenant);
        }
        
        // POST api/Tenant
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TenantDTO body)
        {
            var result = await _tenantService.AddTenantAsync(body);
            if (result == 1)
                return Created("", "");
            else
                return Conflict();
        }

        // PUT api/Tenant/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TenantDTO body)
        {
            var tenant = await _tenantService.UpdateTenantAsync(id, body);
            if (tenant is null)
                return BadRequest("Tenant not found");
            else
                return Ok();
        }

        // DELETE api/Tenant/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var tenant = await _tenantService.DeleteTenantAsync(id);
            if (tenant is null)
                return BadRequest("Tenant not found");
            else
                return Ok();
        }
    }
}
