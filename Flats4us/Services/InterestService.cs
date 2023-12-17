using AutoMapper;
using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Flats4us.Services
{
    public class InterestService : IInterestService
    {
        public readonly Flats4usContext _context;
        private readonly IMapper _mapper;

        public InterestService(
            Flats4usContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<InterestDto>> GetAll(string? name = null)
        {
            var query = _context.Interests.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(e => e.Name.ToLower().Contains(name.ToLower()));
            }

            var result = await query
                .Select(i => _mapper.Map<InterestDto>(i))
                .ToListAsync();

            return result;
        }
    }
}
