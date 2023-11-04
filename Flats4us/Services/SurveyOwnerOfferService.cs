using Flats4us.Entities;
using Microsoft.EntityFrameworkCore;

namespace Flats4us.Services
{
    public class SurveyOwnerOfferService : ISurveyOwnerOfferService
    {
        public readonly Flats4usContext _context;

        public SurveyOwnerOfferService(Flats4usContext context)
        {
            _context = context;
        }
        /*
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
        */
        public async Task<List<SurveyOwnerOffer>> GetAllAsync()
        {
            return await _context.OwnerOfferSurveys.ToListAsync();
        }

        public async Task<SurveyOwnerOffer> GetById(int id)
        {
            return await _context.OwnerOfferSurveys.FirstAsync(x => x.SurveyOwnerOfferId == id);
        }

        public async Task<SurveyOwnerOffer> Post(SurveyOwnerOffer surveyOwnerOffer)
        {
            await _context.OwnerOfferSurveys.AddAsync(surveyOwnerOffer);
            await _context.SaveChangesAsync();
            return surveyOwnerOffer;
        }
    }
}
