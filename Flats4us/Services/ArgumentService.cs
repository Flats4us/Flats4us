using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Helpers.Enums;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace Flats4us.Services
{
    public class ArgumentService : IArgumentService
    {
        public readonly Flats4usContext _context;
        public readonly IGroupChatService _groupChatService;

        public ArgumentService(Flats4usContext context, IGroupChatService groupChatService)
        {
            _context = context;
            _groupChatService = groupChatService;
        }

        public async Task<IEnumerable<Argument>> GetArgumentsAsync()
        {
            var ongoingArguments = await _context.Arguments
                .Where(x => x.ArgumentStatus == ArgumentStatus.Ongoing)
                .Where(x => x.InterventionNeed == true)
                .OrderBy(x => x.InterventionNeedDate)
                .ToListAsync();

            return ongoingArguments;
        }

        public async Task<Argument> GetArgumentById(int id)
        {
            var argument = await _context.Arguments.FirstAsync(x => x.ArgumentId == id);
            if (argument == null)
                throw new ArgumentException($"Argument with ID: {id} not found");
            
            return argument;
        }

        public async Task AddArgumentAsync(ArgumentDto input, int studentId)
        {
            var rent = _context.Rents
                .FirstOrDefault(x => x.RentId == input.RentId) 
                ?? throw new ArgumentException($"Rent with Id {input.RentId} not found.");

            var offer = await _context.Offers
                .FindAsync(rent.OfferId)
                ?? throw new ArgumentException($"Offer associated with Rent ID {input.RentId} not found.");

            var property = await _context.Properties
                .FindAsync(offer.PropertyId) 
                ?? throw new ArgumentException($"Property associated with Rent ID {input.RentId} not found.");

            var ifStunedtExistsv1 = rent.OtherStudents.Any(s => s.UserId == studentId);
            
            var ifStunedtExistsv2 = rent.Student.UserId == studentId;

            if (!(ifStunedtExistsv1 || ifStunedtExistsv2))
                throw new ArgumentException($"Student with Id {studentId} is not in this Rent");

            var argument = new Argument
            {
                StartDate = DateTime.Now,
                ArgumentStatus = ArgumentStatus.Ongoing,
                Description = input.Description,
                RentId = input.RentId,
                InterventionNeed = false,
                StudentId = studentId
            };

            await _groupChatService.CreateGroupChatAsync(
                "Argument pomiędzy: student " + studentId + 
                ", oraz właściciel: " + property.OwnerId, 
                new int[] {studentId, property.OwnerId });

            await _context.Arguments.AddAsync(argument);
            await _context.SaveChangesAsync();
        }

        public async Task AcceptArgument(int id)
        {
            var argument = await _context.Arguments
                .FirstAsync(x => x.ArgumentId == id)
                ?? throw new ArgumentException($"Argument with ID: {id} not found");

            argument.OwnerAcceptanceDate = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task AskForIntervention(int id)
        {
            var argument = await _context.Arguments
                .FirstAsync(x => x.ArgumentId == id)
                ?? throw new ArgumentException($"Argument with ID: {id} not found");

            argument.InterventionNeed = true;
            argument.InterventionNeedDate = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task EditStatusArgumentAsync(int id, ArgumentStatus status)
        {
            var argument = await _context.Arguments.FirstAsync(x => x.ArgumentId == id);

            if (argument == null)
                throw new ArgumentException($"Argument with ID: {id} not found");

            argument.ArgumentStatus = status;

            await _context.SaveChangesAsync();
        }

        public async Task<List<ArgumentIntervention>> GetAllInterventionsAsync()
        {
            return await _context.ArgumentInterventions.ToListAsync();
        }

        public async Task<ArgumentIntervention> GetInterventionById(int id)
        {
            var intervention = await _context.ArgumentInterventions.FirstAsync(x => x.ArgumentInterventionId == id);
            if (intervention == null) 
                throw new ArgumentException($"Intervention with ID: {id} not found");

            return intervention;
        }

        public async Task AddInterventionAsync(ArgumentInterventionDto input)
        {
            var argument = await _context.Arguments
                .FirstAsync(x => x.ArgumentId == input.ArgumentId)
                ?? throw new ArgumentException($"Argument with id {input.ArgumentId} not found ");

            argument.InterventionNeed = false;
            argument.InterventionNeedDate = null;

            var argumentIntervention = new ArgumentIntervention
            {
                Date = DateTime.Now,
                Justification = input.Justification,
                ArgumentId = input.ArgumentId,
                ModeratorId = input.ModeratorId
            };

            await _context.ArgumentInterventions.AddAsync(argumentIntervention);
            await _context.SaveChangesAsync();
        }


        //DONE 1. Student może dodać Argument do Rent, który go dotyczy(tworzony jest czat) 
        //DONE 2. Właściciel może zaakceptować
        //DONE 3. Student i wlaściciel może poprosić o interwencje

        //DONE4. Moderator wyświetla liste sporów z potrzebą interwencji(pofiltrowane od najstarszych)
        //5. Moderator może dołączyć do czatu
        //DONE 6. Moderator może podjać decyzje

    }
}
