using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ouvidoria.Models;
using OuvidoriaAPI.DTO;

namespace Ouvidoria.Controllers
{
    public class ManifestationsController : Controller
    {
        private async Task<ManifestacaoDTO> ObterPorId(Guid id)
        {
            string token = HttpContext.Session.GetString("token");
            HttpClient c = new HttpClient();
            c.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var res = await c.GetAsync(ApiUrl.Str($"Manifestacao/ver/{id}"));

            if (!res.IsSuccessStatusCode)
                return null;

            string json = await res.Content.ReadAsStringAsync();
            ManifestacaoDTO m = JsonConvert.DeserializeObject<ManifestacaoDTO>(json);
            return m;
        }

        private async Task< List<SetorDTO>> ObterSetores()
        {
            HttpClient c = new HttpClient();
            var res = await c.GetAsync(ApiUrl.Str("setor/listar"));
            string json = await res.Content.ReadAsStringAsync();
            List<SetorDTO> sets = JsonConvert.DeserializeObject<List<SetorDTO>>(json);
            return sets;
        }

        private async Task<ViewResult> GetIndex(CriarManifestacaoDTO fail = null, string msgFail = null)
        {
            List<SetorDTO> sets = await ObterSetores();
            var viewModel = new
            {
                Setores = sets,
                DTO = fail ?? new CriarManifestacaoDTO(),
                FailMessage = fail == null ? null : msgFail
            };

            return View("Index", viewModel);
        }

        public async Task<IActionResult> Open(Guid id)
        {
            ManifestacaoDTO m = await ObterPorId(id);
            if (m == null)
                return Redirect("Index");
            return View(m);
        }

        public async Task<IActionResult> Index()
        {
            return await GetIndex();
        }

        public async Task<IActionResult> Register(CriarManifestacaoDTO man)
        {
            HttpClient c = new HttpClient();
            var res = await c.PostAsJsonAsync(ApiUrl.Str("manifestacao/registrar"),
                man);

            if (res.IsSuccessStatusCode)
                return await GetIndex();

            return await GetIndex(man, await res.Content.ReadAsStringAsync());
        }

        public async Task<IActionResult> ResponsePage(Guid id)
        {
            ManifestacaoDTO m = await ObterPorId(id);
            if (m == null)
                return Redirect("Index");
            ViewBag.Setores =  await ObterSetores();
            ViewBag.Id = id;
            return View(m);
        }

        public async Task<IActionResult> SendResponse(ResponderManifestacaoDTO resp)
        {
            string token = HttpContext.Session.GetString("token");
            HttpClient c = new HttpClient();
            c.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var res = await c.PutAsJsonAsync(ApiUrl.Str("manifestacao/responder"),
                resp);

            return Redirect("/ouvidor/");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            string token = HttpContext.Session.GetString("token");
            HttpClient c = new HttpClient();
            c.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var res = await c.DeleteAsync(ApiUrl.Str($"manifestacao/excluir/{id}"));

            return Redirect("/ouvidor/");
        }
    }
}