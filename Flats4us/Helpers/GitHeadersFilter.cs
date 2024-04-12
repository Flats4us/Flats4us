using Microsoft.AspNetCore.Mvc.Filters;

namespace Flats4us.Helpers
{
    public class GitHeadersFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            context.HttpContext.Response.Headers.Add("COMMIT_HASH", "test");
            context.HttpContext.Response.Headers.Add("COMMIT_DATE", "test");
        }
    }
}
