using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Helpers.Enums;

namespace Flats4us.Services.Interfaces
{
    public interface IArgumentService
    {
        public Task<IEnumerable<ArgumentDto>> GetArgumentsAsync(ArgumentStatus argument);
        public Task<ArgumentDto> GetArgumentById(int argumentId);
        public Task AddArgumentAsync(AddArgumentDto input, int studentId);
        public Task AcceptArgument(int argumentId, int ownerId);
        public Task AskForIntervention(int argumentId, int userId);
        public Task EditStatusArgumentAsync(int argumentId, ArgumentStatus status);
        public Task<IEnumerable<ArgumentInterventionDto>> GetAllInterventionsAsync();
        public Task<ArgumentIntervention> GetInterventionById(int id);
        public Task AddInterventionAsync(AddArgumentInterventionDto input);
    }
}
