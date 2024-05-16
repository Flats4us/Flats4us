using AutoMapper;
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
        private readonly IMapper _mapper;

        public ArgumentService(Flats4usContext context, IGroupChatService groupChatService, IMapper mapper)
        {
            _context = context;
            _groupChatService = groupChatService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ArgumentReturnDto>> GetArgumentsAsync(ArgumentStatus argumentStatus)
        {
            var arguments = await _context.Arguments
                .Where(x => x.ArgumentStatus == argumentStatus)
                .Where(x => x.InterventionNeed == true)
                .OrderBy(x => x.InterventionNeedDate)
                .Select(e => _mapper.Map<ArgumentReturnDto>(e))
                .ToListAsync();

            return arguments;
        }

        public async Task<ArgumentReturnDto> GetArgumentById(int id)                             //dotyczy moderatora
        {
            var argument = await _context.Arguments
                .Where(x => x.ArgumentId == id)
                .Select(e => _mapper.Map<ArgumentReturnDto>(e))
                .FirstOrDefaultAsync()
                ?? throw new ArgumentException($"Argument with ID: {id} not found");
            
            return argument;
        }

        public async Task AddArgumentAsync(ArgumentDto input, int studentId)            //może stworzyć student lub owner
        {
            var rent = _context.Rents
                .Include(r => r.Student)
                .Include(r => r.OtherStudents)
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

        public async Task AcceptArgument(int argumentId)                                            //tylko owner
        {
            var argument = await _context.Arguments
                .FirstAsync(x => x.ArgumentId == argumentId)
                ?? throw new ArgumentException($"Argument with ID: {argumentId} not found");

            argument.OwnerAcceptanceDate = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task AskForIntervention(int argumentId)                                        //student lub owner
        {
            var argument = await _context.Arguments
                .FirstAsync(x => x.ArgumentId == argumentId)
                ?? throw new ArgumentException($"Argument with ID: {argumentId} not found");

            argument.InterventionNeed = true;
            argument.InterventionNeedDate = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task EditStatusArgumentAsync(int argumentId, ArgumentStatus status)            
        {
            var argument = await _context.Arguments
                .FirstAsync(x => x.ArgumentId == argumentId)
                ?? throw new ArgumentException($"Argument with ID: {argumentId} not found");

            argument.ArgumentStatus = status;

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ArgumentInterventionReturnDto>> GetAllInterventionsAsync()            
        {
            var interventions = await _context.ArgumentInterventions
                .Select(e => _mapper.Map<ArgumentInterventionReturnDto>(e))
                .ToListAsync();

            return interventions;
        }

        public async Task<ArgumentIntervention> GetInterventionById(int id)
        {
            var intervention = await _context.ArgumentInterventions
                .FirstAsync(x => x.ArgumentInterventionId == id)
                ?? throw new ArgumentException($"Intervention with ID: {id} not found");

            return intervention;
        }

        public async Task AddInterventionAsync(ArgumentInterventionDto input)
        {
            var argument = await _context.Arguments
                .FirstAsync(x => x.ArgumentId == input.ArgumentId)
                ?? throw new ArgumentException($"Argument with id {input.ArgumentId} not found ");

            var moderator = await _context.Users
                .FirstAsync(x => x.UserId == input.ModeratorId)
                ?? throw new ArgumentException($"Moderator with id {input.ModeratorId} not found ");

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
