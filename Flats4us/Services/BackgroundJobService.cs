using Flats4us.Services.Interfaces;

namespace Flats4us.Services
{
    public class BackgroundJobService : IBackgroundJobService
    {
        public void Test1()
        {
            Console.WriteLine("Test job1 executed at: " + DateTime.Now);
            // Tutaj możesz dodać logikę, którą chcesz wykonywać w zadaniu
        }

        public void Test2()
        {
            Console.WriteLine("Test job2 executed at: " + DateTime.Now);
            // Tutaj możesz dodać logikę, którą chcesz wykonywać w zadaniu
        }

        public void Test3()
        {
            Console.WriteLine("Test job3 executed at: " + DateTime.Now);
            // Tutaj możesz dodać logikę, którą chcesz wykonywać w zadaniu
        }
    }
}
