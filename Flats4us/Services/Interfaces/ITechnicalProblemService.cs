using Flats4us.Entities;
using Flats4us.Entities.Dto;
using System.Web.Http.ModelBinding;

namespace Flats4us.Services.Interfaces
{
    public interface ITechnicalProblemService
    {
        public Task<List<TechnicalProblem>> GetAllAsync();
        public Task PostAsync(TechnicalProblemDto input);
        Task Delete(int Id);

    }
}
