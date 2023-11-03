using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Flats4us.Services
{
    public class EquipmentService : IEquipmentService
    {
        public readonly Flats4usContext _context;

        public EquipmentService(Flats4usContext context)
        {
            _context = context;
        }

        public async Task<List<EquipmentDto>> GetAll()
        {
            var result = await _context.Equipment
                .Select(e => new EquipmentDto
                {
                    EquipmentId = e.EquipmentId,
                    Name = e.Name
                })
                .ToListAsync();

            return result;
        }
    }
}
