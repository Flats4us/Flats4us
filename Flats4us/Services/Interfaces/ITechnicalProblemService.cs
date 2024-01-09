using Flats4us.Entities;
using Flats4us.Entities.Dto;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;

namespace Flats4us.Services.Interfaces
{
    public interface ITechnicalProblemService
    {
        public Task<CountedListDto<TechnicalProblemForMapperDto>> GetAllAsync(PaginatorDto input);
        public Task PostAsync(TechnicalProblemDto input);
        public  Task PutAsync(int id);
        //public Task Delete(int Id);

    }
}
