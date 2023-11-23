using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Helpers.Enums;
using Flats4us.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Flats4us.Services
{
    public class ArgumentService : IArgumentService
    {
        public readonly Flats4usContext _context;

        public ArgumentService(Flats4usContext context)
        {
            _context = context;
        }

        public async Task<List<Argument>> GetAllArgumentsAsync()
        {
            return await _context.Arguments.ToListAsync();
        }

        public async Task<Argument> GetArgumentById(int id)
        {
            return await _context.Arguments.FirstAsync(x => x.ArgumentId == id);
        }

        public async Task AddArgumentAsync(ArgumentDto input)
        {
            var argument = new Argument
            {
                StartDate = DateTime.Now,
                ArgumentStatus = ArgumentStatus.Ongoing,
                Description = input.Description,
                OfferId = input.OfferId,
                InterventionNeed = input.InterventionNeed,
                StudentId = 5
            };

            await _context.Arguments.AddAsync(argument);
            await _context.SaveChangesAsync();
        }

        
        public async Task EditStatusArgumentAsync(int id, ArgumentStatus status)
        {
            var argument = await _context.Arguments.FirstAsync(x => x.ArgumentId == id);

            if (argument == null)
                return;

            argument.ArgumentStatus = status;

            //await _context.Arguments.UpdateAsync(argument);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Argument>> GetOngoingArgumentsAsync()
        {
            var ongoingArguments = await _context.Arguments
                .Where(x => x.ArgumentStatus == ArgumentStatus.Ongoing)
                .Where(x => x.InterventionNeed == true)
                .ToListAsync();

            return ongoingArguments;
        }








        public async Task<List<ArgumentIntervention>> GetAllInterventionsAsync()
        {
            return await _context.ArgumentInterventions.ToListAsync();
        }

        public async Task<ArgumentIntervention> GetInterventionById(int id)
        {
            return await _context.ArgumentInterventions.FirstAsync(x => x.ArgumentInterventionId == id);
        }

        public async Task AddInterventionAsync(ArgumentInterventionDto input)
        {
            var argumentIntervention = new ArgumentIntervention
            {
                Date = input.Date,
                Justification = input.Justification,
                ArgumentId = input.ArgumentId,
                ModeratorId =input.ModeratorId
            };
            await _context.ArgumentInterventions.AddAsync(argumentIntervention);
            await _context.SaveChangesAsync();
        }




    }
}
