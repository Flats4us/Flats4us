using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Microsoft.EntityFrameworkCore;

namespace Flats4us.Services
{
    public class TenantService : ITenantService
    {
        public readonly Flats4usContext _context;

        public TenantService(Flats4usContext context)
        {
            _context = context;
        }

        public async Task<IList<Tenant>> GetAllTenantsAsync()
        {
            return await _context.Tenants.ToListAsync();
        }

        public async Task<Tenant?> GetTenantByIdAsync(int id)
        {
            var tenant = await _context.Tenants.FindAsync(id);
            return tenant;
        }

        public async Task<int> AddTenantAsync(TenantDTO body)
        {
            var tenant = new Tenant
            {
                Name = body.Name,
                Surname = body.Surname, 
                AddressLine1 = body.AddressLine1,
                AddressLine2 = body.AddressLine2,
                AddressLine3 = body.AddressLine3,
                Email = body.Email,
                PhoneNumber = body.PhoneNumber
            };
            await _context.Tenants.AddAsync(tenant);
            return await _context.SaveChangesAsync();
        }

        public async Task<Tenant?> UpdateTenantAsync(int id, TenantDTO body)
        {
            var tenant = await _context.Tenants.FindAsync(id);
            if (tenant is null) return null;

            tenant.Name = body.Name;
            tenant.Surname = body.Surname;
            tenant.AddressLine1 = body.AddressLine1;
            tenant.AddressLine2 = body.AddressLine2;
            tenant.AddressLine3 = body.AddressLine3;
            tenant.Email = body.Email;
            tenant.PhoneNumber = body.PhoneNumber;

            await _context.SaveChangesAsync();

            return tenant;
        }

        public async Task<Tenant?> DeleteTenantAsync(int id)
        {
            var tenant = await _context.Tenants.FindAsync(id);
            if (tenant is null) return null;

            _context.Tenants.Remove(tenant);
            await _context.SaveChangesAsync();

            return tenant;
        }

    }
}
