using Flats4us.Services.Interfaces;
using Hangfire;

namespace Flats4us.Helpers
{
    public static class HangfireSetup
    {
        public static void ConfigureJobs(IServiceScopeFactory scopeFactory)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var _backgroundJobService = scope.ServiceProvider.GetRequiredService<IBackgroundJobService>();

                RecurringJob.AddOrUpdate("GeneratePayments", () => _backgroundJobService.GeneratePaymentsAsync(), "0 3 * * *");
            }
        }
    }
}
