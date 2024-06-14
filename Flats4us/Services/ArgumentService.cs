using AutoMapper;
using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Helpers.Enums;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using System;
using System.Linq.Dynamic.Core;

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

        public async Task<IEnumerable<ArgumentDto>> GetYourArgumentsAsync(int userId)
        {
            var user = await _context.Users
                .FindAsync(userId);

            if (user is Student)
            {
                var arguments = await _context.Arguments
                .Include(x => x.ArgumentInterventions)
                .Include(x => x.Student)
                .Where(x => x.Student.UserId == userId)
                .OrderBy(x=>x.StartDate)
                .Select(x => _mapper.Map<ArgumentDto>(x))
                .ToListAsync();

                return arguments;
            }
            else if (user is Owner)
            {
                var arguments = await _context.Arguments
                    .Include(x => x.ArgumentInterventions)
                    .Include(x => x.Rent)
                        .ThenInclude(x => x.Offer)
                            .ThenInclude(x => x.Property)
                    .Include(x => x.Student)
                    .Where(x => x.Rent.Offer.Property.OwnerId == userId)
                    .OrderBy(x => x.StartDate)
                    .Select(x => _mapper.Map<ArgumentDto>(x))
                    .ToListAsync();

                return arguments;
            }
            else
                throw new ArgumentException($"User with Id: {userId} is not Student or Owner");
        }

        public async Task<IEnumerable<ArgumentDto>> GetArgumentsAsync()
        {
            var arguments = await _context.Arguments
                .Include(a=>a.Rent)
                    .ThenInclude(r=>r.Offer)
                        .ThenInclude(o=>o.Property)
                            .ThenInclude(p=>p.Owner)
                                .ThenInclude(x => x.ProfilePicture)
                .Include(x => x.Student)
                    .ThenInclude(x => x.ProfilePicture)
                .Include(x => x.ArgumentInterventions)
                .Where(x => x.InterventionNeed == true)
                .OrderBy(x => x.InterventionNeedDate)
                .Select(e => _mapper.Map<ArgumentDto>(e))
                .ToListAsync();

            return arguments;
        }

        public async Task<ArgumentDto> GetArgumentById(int id)
        {
            var argument = await _context.Arguments
                .Where(x => x.ArgumentId == id)
                .Include(a => a.Rent)
                    .ThenInclude(r => r.Offer)
                        .ThenInclude(o => o.Property)
                            .ThenInclude(p => p.Owner)
                                .ThenInclude(x => x.ProfilePicture)
                .Include(x => x.Student)
                    .ThenInclude(x => x.ProfilePicture)
                .Include(x => x.ArgumentInterventions)
                .Select(e => _mapper.Map<ArgumentDto>(e))
                .FirstOrDefaultAsync()
                ?? throw new ArgumentException($"Argument with ID: {id} not found");

            return argument;
        }

        public async Task AddArgumentAsync(AddArgumentDto input, int userId)
        {
            var rent = await _context.Rents
                .Include(r => r.Student)
                .Include(r => r.OtherStudents)
                .Include(r => r.Offer)
                    .ThenInclude(o => o.Property)
                        .ThenInclude(ow => ow.Owner)
                .FirstOrDefaultAsync(x => x.RentId == input.RentId)
                ?? throw new ArgumentException($"Rent with Id {input.RentId} not found.");

            var user = await _context.Users.FindAsync(userId);

            if (user == null) throw new ArgumentException("Cannot find user with given id");

            var property = rent.Offer?.Property
                ?? throw new ArgumentException($"Property associated with Rent ID {input.RentId} not found.");

            var owner = rent.Offer?.Property?.Owner
                ?? throw new ArgumentException($"Owner with this Id not found");

            var ifStudentExistsv1 = rent.OtherStudents.Any(s => s.UserId == userId);

            var ifStudentExistsv2 = rent.Student.UserId == userId;

            if (!(ifStudentExistsv1 || ifStudentExistsv2) && !(owner.UserId == userId))
                throw new ArgumentException($"User with Id {userId} is not in this Rent");
                
            int studentId;

            if (user is Owner)
            {
                studentId = rent.StudentId;
            } 
            else
            {
                studentId = userId;
            }

            List<int> usersIds = new List<int> { studentId, property.OwnerId };

            var chatId = await _groupChatService.CreateGroupChatAsync(
                "Spór pomiędzy: student: " + rent.Student.Name +
                ", oraz właściciel: " + owner.Name,
                usersIds);

            var argument = new Argument
            {
                Title = input.Title,
                Description = input.Description,
                StartDate = DateTime.Now,
                OwnerAcceptanceDate = null,
                StudentAccceptanceDate = null,
                ArgumentStatus = ArgumentStatus.Ongoing,
                InterventionNeed = false,
                InterventionNeedDate = null,
                ArgumentCreatedByUserId = userId,
                RentId = input.RentId,
                StudentId = studentId,
                GroupChatId = chatId
            };

            await _context.Arguments.AddAsync(argument);
            await _context.SaveChangesAsync();
        }

        public async Task OwnerAcceptArgument(int argumentId, int ownerId)
        {
            var argument = await _context.Arguments
                .Include(r => r.Rent)
                    .ThenInclude(o => o.Offer)
                        .ThenInclude(p => p.Property)
                            .ThenInclude(ow => ow.Owner)
                .FirstAsync(x => x.ArgumentId == argumentId)
                ?? throw new ArgumentException($"Argument with ID: {argumentId} not found");

            if (argument.Rent.Offer.Property.OwnerId != ownerId)
                throw new ArgumentException($"You are not the part of this Argument");

            argument.OwnerAcceptanceDate = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task StudentAcceptArgument(int argumentId, int studentId)
        {
            var argument = await _context.Arguments
                .FirstAsync(x => x.ArgumentId == argumentId)
                ?? throw new ArgumentException($"Argument with ID: {argumentId} not found");

            if (argument.StudentId != studentId)
                throw new ArgumentException($"You are not the part of this Argument");

            argument.StudentAccceptanceDate = DateTime.Now;

            await _context.SaveChangesAsync();
        }   

        public async Task AskForIntervention(int argumentId, int userId)
        {
            var argument = await _context.Arguments
                .Include(r => r.Rent)
                    .ThenInclude(o => o.Offer)
                        .ThenInclude(p => p.Property)
                            .ThenInclude(ow => ow.Owner)
                .FirstAsync(x => x.ArgumentId == argumentId)
                ?? throw new ArgumentException($"Argument with ID: {argumentId} not found");

            if (argument.Rent.Offer.Property.Owner.UserId != userId && argument.StudentId != userId)
                throw new ArgumentException($"You are not the part of this Argument");

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

        public async Task<IEnumerable<ArgumentInterventionDto>> GetAllInterventionsAsync()
        {
            var interventions = await _context.ArgumentInterventions
                .Select(e => _mapper.Map<ArgumentInterventionDto>(e))
                .ToListAsync();

            return interventions;
        }

        public async Task<ArgumentInterventionDto> GetInterventionById(int id)
        {
            var intervention = await _context.ArgumentInterventions
                .Where(x => x.ArgumentInterventionId == id)
                .Select(e => _mapper.Map<ArgumentInterventionDto>(e))
                .FirstOrDefaultAsync()
                ?? throw new ArgumentException($"Intervention with ID: {id} not found");

            return intervention;
        }

        public async Task AddInterventionAsync(AddArgumentInterventionDto input, int moderatorId)
        {
            var argument = await _context.Arguments
                .FirstAsync(x => x.ArgumentId == input.ArgumentId)
                ?? throw new ArgumentException($"Argument with id {input.ArgumentId} not found ");

            var moderator = await _context.Users
                .FirstAsync(x => x.UserId == moderatorId)
                ?? throw new ArgumentException($"Moderator with id {moderatorId} not found ");

            argument.InterventionNeed = false;
            argument.InterventionNeedDate = null;

            var argumentIntervention = new ArgumentIntervention
            {
                Date = DateTime.Now,
                Justification = input.Justification,
                ArgumentId = input.ArgumentId,
                ModeratorId = moderatorId
            };

            await _context.ArgumentInterventions.AddAsync(argumentIntervention);
            await _context.SaveChangesAsync();
        }
    }
}
