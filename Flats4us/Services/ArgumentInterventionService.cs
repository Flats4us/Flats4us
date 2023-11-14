using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Flats4us.Services
{
    public class ArgumentInterventionService : IArgumentInterventionService
    {
        public readonly Flats4usContext _context;

        public ArgumentInterventionService(Flats4usContext context)
        {
            _context = context;
        }

        public async Task<List<ArgumentIntervention>> GetAllAsync()
        {
            return await _context.ArgumentInterventions.ToListAsync();
        }

        public async Task<ArgumentIntervention> GetById(int id)
        {
            return await _context.ArgumentInterventions.FirstAsync(x=>x.ArgumentInterventionId == id);
        }

        public async Task AddArgumentInterventionAsync(ArgumentInterventionDto input)
        {
            var argumentIntervention = new ArgumentIntervention
            {
                ArgumentInterventionId = input.ArgumentInterventionId,
                Date = input.Date,
                Justification = input.Justification
            };
            await _context.ArgumentInterventions.AddAsync(argumentIntervention);
            await _context.SaveChangesAsync();
        }
    }
}
