using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Helpers.Enums;
using Flats4us.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace Flats4us.Services
{
    public class ArgumentService : IArgumentService
    {
        public readonly Flats4usContext _context;
        public readonly GroupChatService _groupChatService;

        public ArgumentService(Flats4usContext context, GroupChatService groupChatService)
        {
            _context = context;
            _groupChatService = groupChatService;
        }
        public async Task<List<Argument>> GetAllArgumentsAsync()
        {
            return await _context.Arguments.ToListAsync();
        }
        public async Task<Argument> GetArgumentById(int id)
        {
            return await _context.Arguments.FirstAsync(x => x.ArgumentId == id);
        }

        public async Task AddArgumentAsync(ArgumentDto input, int studentId)
        {
            var property = _context.Properties
                .FirstOrDefault(r => r.PropertyId == input.OfferId);

            if (property is null) throw new ArgumentException($"Property associated with Offer ID {input.OfferId} not found.");

            //var owner = await _context.Owners.FindAsync(property.OwnerId);

            //if (property is null) throw new ArgumentException($"Owner associated with Property ID {property.OwnerId} not found.");

            var argument = new Argument
            {
                StartDate = DateTime.Now,
                ArgumentStatus = ArgumentStatus.Ongoing,
                Description = input.Description,
                RentId = input.OfferId,
                InterventionNeed = input.InterventionNeed,
                StudentId = studentId
            };

            await _groupChatService.CreateGroupChatAsync(
                "Argument pomiędzy: student " + studentId + 
                ", oraz właściciel: " + property.OwnerId, 
                new int[] {studentId, property.OwnerId });

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
                ModeratorId = input.ModeratorId
            };
            await _context.ArgumentInterventions.AddAsync(argumentIntervention);
            await _context.SaveChangesAsync();
        }
    }
}
