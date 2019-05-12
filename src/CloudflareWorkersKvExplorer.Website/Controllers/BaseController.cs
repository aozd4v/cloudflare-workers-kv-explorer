using CloudflareWorkersKv.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CloudflareWorkersKvExplorer.Website.Controllers
{
    public class BaseController : Controller
    {
        protected CloudflareWorkersKvClient<object> GetClient(string namespaceId = null)
        {
            if (string.IsNullOrWhiteSpace(namespaceId))
            {
                namespaceId = null;
            }

            return new CloudflareWorkersKvClient<object>(
                HttpContext.Session.GetString("email"),
                HttpContext.Session.GetString("key"),
                HttpContext.Session.GetString("accountId"),
                namespaceId);
        }
    }
}
