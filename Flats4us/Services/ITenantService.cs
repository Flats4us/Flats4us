using Flats4us.Entities;
using Flats4us.Entities.Dto;

namespace Flats4us.Services
{
    public interface ITenantService
    {
        Task AddTenantAsync(TenantDTO body); // Doesn't return a value
        Task DeleteTenantAsync(int id); // Doesn't return a value
        Task<IEnumerable<Tenant>> GetAllTenantsAsync(); // Returns IEnumerable<Tenant>
        Task<Tenant> GetTenantByIdAsync(int id); // Returns Tenant
        Task UpdateTenantAsync(int id, TenantDTO body); // Doesn't return a value
    }

}