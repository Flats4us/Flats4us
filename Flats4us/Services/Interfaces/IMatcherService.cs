using Flats4us.Entities;
using Flats4us.Entities.Dto;

namespace Flats4us.Services.Interfaces
{
    public interface IMatcherService
    {
        public Task<List<Matcher>> GetAllMatches();
        public Task<List<StudentForMatcherDto>> GetPotentialRoommate(int studentId);

        public Task AcceptStudent(int student1Id, int student2Id, bool isAccept);
    }
}
