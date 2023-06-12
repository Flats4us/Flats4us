using Flats4us.Entities;
using Microsoft.EntityFrameworkCore;

namespace Flats4us.Services
{
    public class TestService : ITestService
    {
        public readonly Flats4usContext _context;

        public TestService(Flats4usContext context)
        {
            _context = context;
        }
    }
}
