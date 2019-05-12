using CloudflareWorkersKvExplorer.Website.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CloudflareWorkersKvExplorer.Website.Attributes
{
    public class SessionRequirementAttribute : TypeFilterAttribute
    {
        public SessionRequirementAttribute() : base(typeof(SessionRequirementFilter))
        {
        }
    }
}
