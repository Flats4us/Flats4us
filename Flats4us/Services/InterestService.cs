using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Flats4us.Services
{
    public class InterestService : IInterestService
    {
        public readonly Flats4usContext _context;

        public InterestService(Flats4usContext context)
        {
            _context = context;
        }

        public async Task<List<InterestDto>> GetAll()
        {
            var result = await _context.Interests
                .Select(e => new InterestDto
                {
                    InterestId = e.InterestId,
                    Name = e.Name
                })
                .ToListAsync();

            return result;
        }
    }
}
