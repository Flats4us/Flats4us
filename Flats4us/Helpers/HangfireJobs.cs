using Flats4us.Services.Interfaces;
using Hangfire;

namespace Flats4us.Helpers
{
    public static class HangfireJobs
    {
        public static void ConfigureJobs(IBackgroundJobService backgroundJobService)
        {
            RecurringJob.AddOrUpdate("test-job1", () => backgroundJobService.Test1(), "* * * * *");
            RecurringJob.AddOrUpdate("test-job2", () => backgroundJobService.Test2(), "*/2 * * * *");
            RecurringJob.AddOrUpdate("test-job3", () => backgroundJobService.Test3(), "*/3 * * * *");
        }
    }
}
