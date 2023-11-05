using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Microsoft.EntityFrameworkCore;

namespace Flats4us.Services
{
    public class SurveyStudentService : ISurveyStudentService
    {

        public readonly Flats4usContext _context;

        public SurveyStudentService(Flats4usContext context)
        {
            _context = context;
        }

        public async Task<List<SurveyStudent>> GetAllAsync()
        {
            return await _context.StudentSurveys.ToListAsync();
        }

        public async Task<SurveyStudent> GetById(int id)
        {
            return await _context.StudentSurveys.FirstAsync(x => x.SurveyStudentId == id);
        }

        public async Task AddSurveyStudentAsync(SurveyStudentPost input)
        {
            //var property = await _context.Properties.FindAsync(input.SurveyStudentId);
            //if (property is null) throw new ArgumentException
            //var student = await _context.Properties.FindAsync(input.Student.UserId);

            var surveyStudent = new SurveyStudent
            {
                SurveyStudentId = input.SurveyStudentId,
                Party = input.Party,
                Tidiness = input.Tidiness,
                Smoking = input.Smoking,
                Sociability = input.Sociability,
                Animals = input.Animals,
                Vegan = input.Vegan,
                LookingForRoommate = input.LookingForRoommate,
                MaxNumberOfRoommates = input.MaxNumberOfRoommates,
                RoommateGender = input.RoommateGender,
                MinRoommateAge = input.MinRoommateAge,
                //Student = null
            };

            await _context.StudentSurveys.AddAsync(surveyStudent);
            await _context.SaveChangesAsync(); 
        }

    }
}
