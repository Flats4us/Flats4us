using Flats4us.Entities;
using Flats4us.Entities.Dto;


namespace Flats4us.Services.Interfaces
{
    public interface IArgumentInterventionService
    {
        public Task<List<ArgumentIntervention>> GetAllAsync();
        public Task<ArgumentIntervention> GetById(int id);
        public Task AddArgumentInterventionAsync(ArgumentInterventionDto input);
    }
}
