using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Helpers.Enums;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Flats4us.Services
{
    public class MatcherService : IMatcherService
    {
        public readonly Flats4usContext _context;

        public MatcherService(Flats4usContext context)
        {
            _context = context;
        }

        public async Task<List<Matcher>> GetAllMatches()
        {
            return await _context.Matcher.ToListAsync();
        }


        public async Task<List<StudentDto>> GetPotentialRoommate(int studentId)
        {
            var requestingStudent = _context.Students
                .Include(requesting => requesting.SurveyStudent)
                .FirstOrDefault(requesting => requesting.UserId == studentId);

            if (requestingStudent == null)
            { 
                return null;
            }

            var potentialRoommates = _context.Students
                .Include(potential => potential.SurveyStudent)
                .Where(potential =>
                    potential.SurveyStudent.LookingForRoommate &&
                    potential.UserId != studentId &&
                    potential.SurveyStudent.MaxNumberOfRoommates == requestingStudent.SurveyStudent.MaxNumberOfRoommates &&
                    potential.SurveyStudent.RoommateGender == requestingStudent.SurveyStudent.RoommateGender &&
                    (/*!potential.SurveyStudent.MinRoommateAge.HasValue ||*/ requestingStudent.SurveyStudent.MinRoommateAge <= (DateTime.Now.Year-potential.BirthDate.Year)) &&
                    (/*!potential.SurveyStudent.MaxRoommateAge.HasValue ||*/ requestingStudent.SurveyStudent.MaxRoommateAge >= (DateTime.Now.Year-potential.BirthDate.Year)))
                .Select(potential => new StudentDto
                {
                    Name = potential.Name,
                    Surname = potential.Surname,
                    BirthDate = potential.BirthDate,
                    StudentNumber = potential.StudentNumber,
                    University = potential.University
                    
                })
                .ToListAsync();

            return await potentialRoommates;
        }

        public async Task AcceptStudent(int student1Id, int student2Id, bool isAccept)
        {
            var h = _context.Matcher  
                .Where( x =>(
                 x.Student1Id == student1Id && x.Student2Id == student2Id) || 
                (x.Student1Id == student2Id && x.Student2Id == student1Id))
                .FirstOrDefault();

            if (h == null)
            {
                h = new Matcher
                {
                    Student1Id = Math.Min(student1Id, student2Id),
                    Student2Id = Math.Min(student1Id, student2Id),
                    IsStudent1Interested = true,
                    IsStudent2Interested = null
                };
            }
            else
            {
                h.IsStudent2Interested = isAccept;
                //if (h.Student1Id == student1Id)
                //{
                //    h.IsStudent1Interested = isAccept;
                //}
                //else
                //{
                //    h.IsStudent2Interested = isAccept;
                //}
            }

            await _context.SaveChangesAsync();
        }
    }
}
