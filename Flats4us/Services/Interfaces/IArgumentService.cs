using Flats4us.Entities.Dto;
using Flats4us.Entities;

namespace Flats4us.Services.Interfaces
{
    public interface IArgumentService
    {
        public Task<List<Argument>> GetAllArgumentsAsync();
        public Task<Argument> GetArgumentById(int id);
        public Task AddArgumentAsync(ArgumentDto input);

        public Task<List<ArgumentIntervention>> GetAllInterventionsAsync();
        public Task<ArgumentIntervention> GetInterventionById(int id);
        public Task AddInterventionAsync(ArgumentInterventionDto input);
    }
}
