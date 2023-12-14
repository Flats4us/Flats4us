using Flats4us.Entities;

namespace Flats4us.Services.Interfaces
{
    public interface IGrindrService
    {

        public List<Student> GetPotentialRoommate(int studentId);

        public void AcceptStudent(int student1Id, int student2Id, bool isAccept);
    }
}
