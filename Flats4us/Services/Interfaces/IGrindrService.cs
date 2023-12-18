using Flats4us.Entities;

namespace Flats4us.Services.Interfaces
{
    public interface IGrindrService
    {
        public Task<List<Grindr>> GetAllMatches();
        public Task<List<Student>> GetPotentialRoommate(int studentId);

        public Task AcceptStudent(int student1Id, int student2Id, bool isAccept);
    }
}
