﻿using Flats4us.Entities.Dto;
using Flats4us.Entities;

namespace Flats4us.Services.Interfaces
{
    public interface ISurveyService
    {
        public Task<string> MakingSurvey(Type type, string title, string lang);

        public Task<List<SurveyStudent>> GetAllSurveyStudentsAsync();
        public Task<SurveyStudent> GetSurveyStudentById(int id);
        public Task AddSurveyStudentAsync(SurveyStudentDto input);
    }
}
