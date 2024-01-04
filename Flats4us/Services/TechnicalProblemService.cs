using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Helpers.Enums;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Flats4us.Services
{
    public class TechnicalProblemService : ITechnicalProblemService
    {
        public readonly Flats4usContext _context;

        public TechnicalProblemService(Flats4usContext context)
        {
            _context = context;
        }

        public async Task<List<TechnicalProblem>> GetAllAsync()
        {
            return await _context.TechnicalProblems.ToListAsync();
        }

        public async Task PostAsync(TechnicalProblemDto input)
        {
            var problem = new TechnicalProblem
            {
                Kind = input.Kind,
                Description = input.Description,
                Date = DateTime.Now,
                UserId = input.UserId
            };

            await _context.TechnicalProblems.AddAsync(problem);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var problem = await _context.TechnicalProblems.FindAsync(id);

            _context.TechnicalProblems.Remove(problem);
            await _context.SaveChangesAsync();
        }
    }
}
