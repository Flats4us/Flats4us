using Flats4us.Entities;
using Microsoft.EntityFrameworkCore;

namespace Flats4us.Services
{
    public class TestService : ITestService
    {
        public readonly Flats4usContext _context;

        public TestService(Flats4usContext context)
        {
            _context = context;
        }

        public async Task<IList<Student>> GetAllStudentsAsync()
        {
            return await _context.Students.ToListAsync();
        }


        public async Task<SurveyStudent> GetSurveyOfStudentById(int id)
        {
            var survey =  _context.Students.Where(x => x.UserId == id).Select(x => x.SurveyStudent).FirstOrDefault();
            

            return survey;
            
        }
    }
}
