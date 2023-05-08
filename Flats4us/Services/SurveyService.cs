using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Flats4us.Services
{
    public class SurveyService : ISurveyService
    {
        public readonly Flats4usContext _context;

        public SurveyService(Flats4usContext context)
        {
            _context = context;
        }

        public async Task<IList<Survey>> GetAllSurveysAsync()
        {
            return await _context.Surveys.ToListAsync();
        }

        public async Task<Survey?> GetSurveyByIdAsync(int id)
        {
            var survey = await _context.Surveys.FindAsync(id);
            return survey;
        }

        public async Task<int> AddSurveyAsync(SurveyDto body)
        {
            var survey = new Survey
            {
                Smoking = body.Smoking,
                Animal = body.Animal,
                Vegan = body.Vegan,
                Party = body.Party,
                Tidiness = body.Tidiness,
                Loudness = body.Loudness,
                Sociability = body.Sociability,
                Helpfulness = body.Helpfulness,
                Roommate = body.Roommate,
                MaxNumberOfRoommates = body.MaxNumberOfRoommates,
                RoommateGender = body.RoommateGender,
                MinRoommateAge = body.MinRoommateAge,
                MaxRoommateAge = body.MaxRoommateAge
            };
            await _context.Surveys.AddAsync(survey);
            return await _context.SaveChangesAsync();
        }

        public async Task<Survey?> UpdateSurveyAsync(int id, SurveyDto body)
        {
            var survey = await _context.Surveys.FindAsync(id);
            if (survey is null) return null;


            survey.Smoking = body.Smoking;
            survey.Animal = body.Animal;
            survey.Vegan = body.Vegan;
            survey.Party = body.Party;
            survey.Tidiness = body.Tidiness;
            survey.Loudness = body.Loudness;
            survey.Sociability = body.Sociability;
            survey.Helpfulness = body.Helpfulness;
            survey.Roommate = body.Roommate;
            survey.MaxNumberOfRoommates = body.MaxNumberOfRoommates;
            survey.RoommateGender = body.RoommateGender;
            survey.MinRoommateAge = body.MinRoommateAge;
            survey.MaxRoommateAge = body.MaxRoommateAge;

            await _context.SaveChangesAsync();

            return survey;
        }
        public async Task<Survey?> DeleteSurveyAsync(int id)
        {
            var survey = await _context.Surveys.FindAsync(id);
            if (survey is null) return null;

            _context.Surveys.Remove(survey);
            await _context.SaveChangesAsync();

            return survey;
        }
    }
}
