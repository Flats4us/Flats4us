using Flats4us.Entities;
using Flats4us.Entities.Dto;

namespace Flats4us.Services.Interfaces
{
    public interface IMatcherService
    {
        public Task<List<Matcher>> GetAllMatches();
        public Task<List<StudentForMatcherDto>> GetMatchByStudentId(int id);
        public Task<List<StudentForMatcherDto>> GetPotentialRoommateAsync(int studentId);
        public Task AcceptStudentAsync(int student1Id, int student2Id, bool isAccept);
    }
}
