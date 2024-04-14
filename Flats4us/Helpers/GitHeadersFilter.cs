using Microsoft.AspNetCore.Mvc.Filters;

namespace Flats4us.Helpers
{
    public class GitHeadersFilter : IActionFilter
    {
        private readonly AppInfo _appInfo;

        public GitHeadersFilter(AppInfo appInfo)
        {
            _appInfo = appInfo;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            context.HttpContext.Response.Headers.Add("COMMIT_HASH", _appInfo.CommitHash);
            context.HttpContext.Response.Headers.Add("COMMIT_DATE", _appInfo.CommitDate);
        }
    }
}
