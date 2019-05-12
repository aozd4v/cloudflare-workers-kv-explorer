using System.Threading.Tasks;
using CloudflareWorkersKv.Client;
using CloudflareWorkersKvExplorer.Website.Attributes;
using CloudflareWorkersKvExplorer.Website.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CloudflareWorkersKvExplorer.Website.Controllers
{
    public class HomeController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Test")]
        [SessionRequirement]
        public IActionResult IndexAuth()
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitCredentials(CloudflareCredentialsModel model)
        {
            try
            {
                var client = new CloudflareWorkersKvClient<object>(model.Email, model.ApiKey,
                    model.AccountId);

                await client.Namespaces.List();
            }
            catch
            {
                return RedirectToAction("Index");
            }

            HttpContext.Session.SetString("email", model.Email);
            HttpContext.Session.SetString("key", model.ApiKey);
            HttpContext.Session.SetString("accountId", model.AccountId);

            return RedirectToAction("Index", "Namespaces");
        }
    }
}
