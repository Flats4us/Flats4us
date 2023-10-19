using Flats4us.Entities;
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

        public async Task<SurveyStudent> Post(SurveyStudent surveyStudent)
        {
            await _context.StudentSurveys.AddAsync(surveyStudent);
            await _context.SaveChangesAsync();
            return surveyStudent;
        }

    }
}
