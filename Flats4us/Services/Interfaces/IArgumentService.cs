using Flats4us.Entities;
using Flats4us.Entities.Dto;
using Flats4us.Helpers.Enums;

namespace Flats4us.Services.Interfaces
{
    public interface IArgumentService
    {
        public Task<IEnumerable<ArgumentReturnDto>> GetArgumentsAsync(ArgumentStatus argument);
        public Task<ArgumentReturnDto> GetArgumentById(int id);
        public Task AddArgumentAsync(ArgumentDto input, int studentId);
        public Task AcceptArgument(int id);
        public Task AskForIntervention(int id);
        public Task EditStatusArgumentAsync(int id, ArgumentStatus status);
        public Task<IEnumerable<ArgumentInterventionReturnDto>> GetAllInterventionsAsync();
        public Task<ArgumentIntervention> GetInterventionById(int id);
        public Task AddInterventionAsync(ArgumentInterventionDto input);
    }
}
