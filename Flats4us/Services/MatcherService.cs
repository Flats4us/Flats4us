using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Helpers.Enums;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Flats4us.Helpers.AutoMapperResolvers;
using AutoMapper;

namespace Flats4us.Services
{
    public class MatcherService : IMatcherService
    {
        public readonly Flats4usContext _context;
        private readonly IMapper _mapper;

        public MatcherService(
            Flats4usContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        //public async Task<List<Matcher>> GetAllMatches()
        //{
        //    return await _context.Matcher.ToListAsync();
        //}

        public async Task<List<StudentDto>> GetMatchByStudentId(int id)
        {
            var matchers = await _context.Students
                .Include(m => m.Matchers)
                .Where(m =>( m.Matchers.Any(x => (x.Student1Id == id || x.Student2Id == id)
                        && ( x.IsStudent1Interested == true && x.IsStudent2Interested == true))))
                .Select(potential => _mapper.Map<StudentDto>(potential))
                .ToListAsync();

            return matchers;
        }

        public async Task<List<StudentForMatcherDto>> GetPotentialRoommateAsync(int studentId)
        {
            var requestingStudent = await _context.Students
                .Include(requesting => requesting.SurveyStudent)
                .FirstOrDefaultAsync(requesting => requesting.UserId == studentId);

            if (requestingStudent == null)
            {
                return null;
            }

            var potentialRoommates = await _context.Students
                .Include(potential => potential.SurveyStudent)
                .Include(potential => potential.Interests)
                .Where(potential =>
                    potential.SurveyStudent.LookingForRoommate &&
                    potential.UserId != studentId &&
                    potential.SurveyStudent.MaxNumberOfRoommates == requestingStudent.SurveyStudent.MaxNumberOfRoommates &&
                    potential.SurveyStudent.RoommateGender == requestingStudent.SurveyStudent.RoommateGender &&
                    (requestingStudent.SurveyStudent.MinRoommateAge <= (DateTime.Now.Year - potential.BirthDate.Year)) &&
                    (requestingStudent.SurveyStudent.MaxRoommateAge >= (DateTime.Now.Year - potential.BirthDate.Year)))
                .Select(potential => _mapper.Map<StudentForMatcherDto>(potential))
                .Take(5)
                .ToListAsync();

            return potentialRoommates;
        }

        public async Task AcceptStudentAsync(int student1Id, int student2Id, bool isAccept)
        {
            var match = await _context.Matcher
                .Where(x => (x.Student1Id == student1Id && x.Student2Id == student2Id) || 
                            (x.Student1Id == student2Id && x.Student2Id == student1Id))
                .FirstOrDefaultAsync();

            if (match == null)
            {
                match = new Matcher
                {
                    Student1Id = Math.Min(student1Id, student2Id),
                    Student2Id = Math.Max(student1Id, student2Id)
                };
                await _context.Matcher.AddAsync(match);
            }
            
            if(student1Id==match.Student1Id)
            {
                match.IsStudent1Interested = isAccept;
            }
            else
            {
                match.IsStudent2Interested = isAccept;
            }

            await _context.SaveChangesAsync();
        }
    }
}
