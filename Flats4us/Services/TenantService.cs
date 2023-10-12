using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Microsoft.EntityFrameworkCore;

namespace Flats4us.Services
{
    public class TenantService : ITenantService
    {
        private readonly Flats4usContext _dbContext;

        public TenantService(Flats4usContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddTenantAsync(TenantDTO tenantDto)
        {
            var tenant = new Tenant
            {
                // Map the properties from DTO to Tenant model
                Id = tenantDto.Id,
                // Other properties...
            };

            _dbContext.Tenants.Add(tenant);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteTenantAsync(int id)
        {
            var tenant = await _dbContext.Tenants.FindAsync(id);
            if (tenant != null)
            {
                _dbContext.Tenants.Remove(tenant);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Tenant>> GetAllTenantsAsync()
        {
            return await _dbContext.Tenants.ToListAsync();
        }
        public async Task<Tenant> GetTenantByIdAsync(int id)
        {
            return await _dbContext.Tenants.FindAsync(id);
        }



        public async Task UpdateTenantAsync(int id, TenantDTO tenantDto)
        {
            var tenant = await _dbContext.Tenants.FindAsync(id);
            if (tenant != null)
            {
                // Map updated properties from the DTO
                tenant.Id = tenantDto.Id;
                // Other properties...

                await _dbContext.SaveChangesAsync();
            }
        }

        
    }
}
