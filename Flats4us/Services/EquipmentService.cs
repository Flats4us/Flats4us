using AutoMapper;
using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Flats4us.Services
{
    public class EquipmentService : IEquipmentService
    {
        public readonly Flats4usContext _context;
        private readonly IMapper _mapper;

        public EquipmentService(
            Flats4usContext context, 
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<EquipmentDto>> GetAll(string? name = null)
        {
            var query = _context.Equipment.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(e => e.Name.ToLower().Contains(name.ToLower()));
            }

            var result = await query
                .Select(e => _mapper.Map<EquipmentDto>(e))
                .ToListAsync();

            return result;
        }
    }
}
