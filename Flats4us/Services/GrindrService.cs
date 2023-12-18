using Flats4us.Entities;
using Flats4us.Helpers.Enums;
using Flats4us.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Flats4us.Services
{
    public class GrindrService : IGrindrService
    {
        public readonly Flats4usContext _context;

        public GrindrService(Flats4usContext context)
        {
            _context = context;
        }

        public async Task<List<Grindr>> GetAllMatches()
        {
            return await _context.Grindr.ToListAsync();
        }


        public async Task<List<Student>> GetPotentialRoommate(int studentId)
        {
            var requestingStudent = _context.Students
                .Include(s => s.SurveyStudent)
                .FirstOrDefault(s => s.UserId == studentId);

            if (requestingStudent == null)
            { 
                return null;
            }

            var potentialRoommates = _context.Students
                .Include(s => s.SurveyStudent)
                .Where(s =>
                    s.SurveyStudent.LookingForRoommate &&
                    s.UserId != studentId && 
                    s.SurveyStudent.MaxNumberOfRoommates == requestingStudent.SurveyStudent.MaxNumberOfRoommates &&
                    s.SurveyStudent.RoommateGender == requestingStudent.SurveyStudent.RoommateGender &&
                    (!s.SurveyStudent.MinRoommateAge.HasValue || s.SurveyStudent.MinRoommateAge >= requestingStudent.SurveyStudent.MinRoommateAge) &&
                    (!s.SurveyStudent.MaxRoommateAge.HasValue || s.SurveyStudent.MaxRoommateAge <= requestingStudent.SurveyStudent.MaxRoommateAge))
                .ToListAsync();

            return await potentialRoommates;
        }

        public async Task AcceptStudent(int student1Id, int student2Id, bool isAccept)
        {
            var h = _context.Grindr  
                .Where( x =>(
                x.Student1Id == student1Id && 
                x.Student2Id == student2Id) || 
                (x.Student1Id == student2Id && 
                x.Student2Id == student1Id))
                .FirstOrDefault();

            if (h == null)
            {
                h = new Grindr
                {
                    Student1Id = student1Id,
                    Student2Id = student2Id,
                    isStudent1Interested = isAccept,
                    isStudent2Interested = null
                };
            }
            else
            {
                if (h.Student1Id == student1Id)
                {
                    h.isStudent1Interested = isAccept;
                }
                else
                {
                    h.isStudent2Interested = isAccept;
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
