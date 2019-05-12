using System.Threading.Tasks;
using CloudflareWorkersKvExplorer.Website.Attributes;
using CloudflareWorkersKvExplorer.Website.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CloudflareWorkersKvExplorer.Website.Controllers
{
    [SessionRequirement]
    public class KeyValuesController : BaseController
    {
        [HttpGet("KeyValues/{key?}")]
        public async Task<IActionResult> Index(string key = null)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return View(new KeyValueModel(key, "{}"));
            }

            var namespaceId = HttpContext.Session.GetString("namespaceId");
            var client = GetClient(namespaceId);

            string json;

            try
            {
                var obj = await client.KeyValues.Read(key);
                json = JsonConvert.SerializeObject(obj);
            }
            catch
            {
                return RedirectToAction("ViewNamespace", "Namespaces", new {Id = namespaceId});
            }

            return View(new KeyValueModel(key, json));
        }

        [HttpPost("KeyValues")]
        public async Task<IActionResult> Save(KeyValueModel model)
        {
            var namespaceId = HttpContext.Session.GetString("namespaceId");
            var client = GetClient(namespaceId);

            if (string.IsNullOrWhiteSpace(model.Key))
            {
                return RedirectToAction("ViewNamespace", "Namespaces", new {Id = namespaceId});
            }

            object obj;

            try
            {
                obj = JsonConvert.DeserializeObject(model.Json);
            }
            catch
            {
                return RedirectToAction("Index", "KeyValues", new {model.Key});
            }

            await client.KeyValues.Write(model.Key, obj);

            return RedirectToAction("Index", "KeyValues", new {model.Key});
        }

        [HttpDelete("KeyValues/{key?}")]
        public async Task<IActionResult> Delete(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return BadRequest();
            }

            var namespaceId = HttpContext.Session.GetString("namespaceId");
            var client = GetClient(namespaceId);

            await client.KeyValues.Delete(key);

            return Ok();
        }
    }
}
