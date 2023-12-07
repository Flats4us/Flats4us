using Flats4us.Entities;
using Flats4us.Services.Interfaces;
using Hangfire;

namespace Flats4us.Helpers
{
    public static class HangfireSetup
    {
        public  static void ConfigureJobs(IServiceScopeFactory scopeFactory)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var backgroundJobService = scope.ServiceProvider.GetRequiredService<IBackgroundJobService>();

                //EXAMPLE
                RecurringJob.AddOrUpdate("test-job3", () => backgroundJobService.TestAsync(), "* * * * *");

                RecurringJob.AddOrUpdate("payment", () => backgroundJobService.PaymentAsync(), "0 3 * * *");
            }
        }
    }
}
