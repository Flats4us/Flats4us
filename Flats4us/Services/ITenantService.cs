using Flats4us.Entities;
using Flats4us.Entities.Dto;

namespace Flats4us.Services
{
    public interface ITenantService
    {
        Task<IList<Tenant>> GetAllTenantsAsync();
        Task<Tenant?> GetTenantByIdAsync(int id);
        Task<int> AddTenantAsync(TenantDTO tenant);
        Task<Tenant?> DeleteTenantAsync(int id);
        Task<Tenant?> UpdateTenantAsync(int id, TenantDTO tenant);
        
    }
}
