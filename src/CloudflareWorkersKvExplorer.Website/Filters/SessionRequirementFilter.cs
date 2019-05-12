using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CloudflareWorkersKvExplorer.Website.Filters
{
    public class SessionRequirementFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authenticated =
                context.HttpContext.Session.Keys.Any(x => x == "email")
                && context.HttpContext.Session.Keys.Any(x => x == "key")
                && context.HttpContext.Session.Keys.Any(x => x == "accountId");

            if (!authenticated)
            {
                context.Result = new RedirectToActionResult("Index", "Home", new {});
            }
        }
    }
}
