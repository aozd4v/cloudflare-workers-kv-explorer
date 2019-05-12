using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudflareWorkersKv.Client.Models.Namespaces;
using CloudflareWorkersKvExplorer.Website.Attributes;
using CloudflareWorkersKvExplorer.Website.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CloudflareWorkersKvExplorer.Website.Controllers
{
    [SessionRequirement]
    public class NamespacesController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = GetClient();
            var namespaces = await client.Namespaces.List();

            return View(namespaces);
        }

        [HttpGet("Namespaces/{id}")]
        public async Task<IActionResult> ViewNamespace(string id)
        {
            var client = GetClient(id);

            IEnumerable<Namespace> namespaces;

            try
            {
                namespaces = await client.Namespaces.List();
            }
            catch
            {
                return RedirectToAction("Index", "Namespaces");
            }

            HttpContext.Session.SetString("namespaceId", id);
            var ns = namespaces.FirstOrDefault(x => x.Id == id);

            if (ns == null)
            {
                return RedirectToAction("Index", "Namespaces");
            }

            HttpContext.Session.SetString("namespaceTitle", ns.Title);
            var keys = new List<string>();
            var result = await client.KeyValues.List();
            keys.AddRange(result.Keys);

            while (!string.IsNullOrWhiteSpace(result.Cursor))
            {
                result = await client.KeyValues.List(result.Cursor);
                keys.AddRange(result.Keys);
            }

            return View(new ViewNamespaceModel(ns.Title, keys));
        }

        [HttpGet("Namespaces/Edit/{id?}")]
        public async Task<IActionResult> Edit(string id = null)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return View(new SaveNamespaceModel(string.Empty, string.Empty));
            }

            var client = GetClient(id);

            IEnumerable<Namespace> namespaces;

            try
            {
                namespaces = await client.Namespaces.List();
            }
            catch
            {
                return RedirectToAction("Index", "Namespaces");
            }

            HttpContext.Session.SetString("namespaceId", id);
            var ns = namespaces.FirstOrDefault(x => x.Id == id);

            if (ns == null)
            {
                return RedirectToAction("Index", "Namespaces");
            }

            return View(new SaveNamespaceModel(ns.Id, ns.Title));
        }

        [HttpPost("Namespaces")]
        public async Task<IActionResult> Save(SaveNamespaceModel model)
        {
            var client = GetClient();

            if (string.IsNullOrWhiteSpace(model.Name))
            {
                return RedirectToAction("Index", "Namespaces");
            }

            try
            {
                if (string.IsNullOrWhiteSpace(model.Id))
                {
                    await client.Namespaces.Create(model.Name);
                }
                else
                {
                    await client.Namespaces.Rename(model.Id, model.Name);
                }
            }
            catch
            {
                return RedirectToAction("Index", "Namespaces");
            }

            return RedirectToAction("Index", "Namespaces");
        }

        [HttpDelete("Namespaces/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest();
            }

            var client = GetClient();

            await client.Namespaces.Delete(id);

            return Ok();
        }
    }
}
