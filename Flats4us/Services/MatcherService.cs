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

        public async Task<List<StudentForMatcherDto>> GetMatchByStudentId(int requestUserId)
        {
            var matchingIds = await _context.Matcher
                .Where(m => (m.Student1Id == requestUserId || m.Student2Id == requestUserId)
                            && m.IsStudent1Interested == true && m.IsStudent2Interested == true)
                .Select(m => m.Student1Id == requestUserId ? m.Student2Id : m.Student1Id)
                .Distinct()
                .ToListAsync();

            var matchingStudents = await _context.Students
                .Include(potential => potential.Interests)
                .Where(s => matchingIds.Contains(s.UserId) && s.UserId != requestUserId)
                .Select(potential => _mapper.Map<StudentForMatcherDto>(potential))
                .ToListAsync();

            return matchingStudents;
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

                    potential.UserId != studentId &&

                    (potential.SurveyStudent.Party >= (requestingStudent.SurveyStudent.Party-2)) && 
                    (potential.SurveyStudent.Party <= (requestingStudent.SurveyStudent.Party + 2)) &&

                    //  Party

                    (potential.SurveyStudent.Tidiness >= (requestingStudent.SurveyStudent.Tidiness - 2)) && 
                    (potential.SurveyStudent.Tidiness <= (requestingStudent.SurveyStudent.Tidiness + 2)) &&

                    //  Tidiness

                    potential.SurveyStudent.Smoking == requestingStudent.SurveyStudent.Smoking &&

                    //  Smoking

                    potential.SurveyStudent.Sociability == requestingStudent.SurveyStudent.Sociability &&

                    //  Sociability

                    potential.SurveyStudent.Animals == requestingStudent.SurveyStudent.Animals &&

                    //  Animals

                    potential.SurveyStudent.Vegan == requestingStudent.SurveyStudent.Vegan &&

                    //  Vegan

                    potential.SurveyStudent.LookingForRoommate == requestingStudent.SurveyStudent.LookingForRoommate &&

                    //  LookingForRoommate

                    potential.SurveyStudent.MaxNumberOfRoommates == requestingStudent.SurveyStudent.MaxNumberOfRoommates &&

                    //  MaxNumberOfRoommates

                    potential.SurveyStudent.RoommateGender == requestingStudent.SurveyStudent.RoommateGender &&

                    //  RoommateGender

                    (requestingStudent.SurveyStudent.MinRoommateAge <= (DateTime.Now.Year - potential.BirthDate.Year)) &&

                    //  MinRoommateAge

                    (requestingStudent.SurveyStudent.MaxRoommateAge >= (DateTime.Now.Year - potential.BirthDate.Year)) &&

                    //  MaxRoommateAge

                    potential.SurveyStudent.City == requestingStudent.SurveyStudent.City &&

                    //  City

                    !_context.Matcher.Any(matcher =>
                        (matcher.Student1Id == studentId && matcher.Student2Id == potential.UserId && matcher.IsStudent1Interested != null) ||
                        (matcher.Student1Id == potential.UserId && matcher.Student2Id == studentId && matcher.IsStudent2Interested != null)
                     ))

                .Select(potential => new
                {
                    PotentialStudent = potential,
                    ConditionsMet = (
                        (potential.SurveyStudent.Party >= (requestingStudent.SurveyStudent.Party - 2)) &&
                        (potential.SurveyStudent.Party <= (requestingStudent.SurveyStudent.Party + 2)) ? 1 : 0) +

                        ((potential.SurveyStudent.Tidiness >= (requestingStudent.SurveyStudent.Tidiness - 2)) &&
                        (potential.SurveyStudent.Tidiness <= (requestingStudent.SurveyStudent.Tidiness + 2)) ? 1 : 0) +

                        (potential.SurveyStudent.Smoking == requestingStudent.SurveyStudent.Smoking ? 1 : 0) +

                        (potential.SurveyStudent.Sociability == requestingStudent.SurveyStudent.Sociability ? 1 : 0) +

                        (potential.SurveyStudent.Animals == requestingStudent.SurveyStudent.Animals ? 1 : 0) +

                        (potential.SurveyStudent.Vegan == requestingStudent.SurveyStudent.Vegan ? 1 : 0) +

                        (potential.SurveyStudent.LookingForRoommate == requestingStudent.SurveyStudent.LookingForRoommate ? 1 : 0) +

                        (potential.SurveyStudent.MaxNumberOfRoommates == requestingStudent.SurveyStudent.MaxNumberOfRoommates ? 1 : 0) +

                        (potential.SurveyStudent.RoommateGender == requestingStudent.SurveyStudent.RoommateGender ? 1 : 0) +

                        ((requestingStudent.SurveyStudent.MinRoommateAge <= (DateTime.Now.Year - potential.BirthDate.Year)) ? 1 : 0) +

                        ((requestingStudent.SurveyStudent.MaxRoommateAge >= (DateTime.Now.Year - potential.BirthDate.Year)) ? 1 : 0) +

                        (potential.SurveyStudent.City == requestingStudent.SurveyStudent.City ? 1 : 0)
                })
                .Where(result => result.ConditionsMet >= 0.8 * 12) 
                .OrderByDescending(result => result.ConditionsMet)
                .Select(potential => _mapper.Map<StudentForMatcherDto>(potential.PotentialStudent))
                //.Take(5)
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
