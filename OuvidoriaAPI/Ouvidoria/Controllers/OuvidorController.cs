using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OuvidoriaAPI.DTO;

namespace Ouvidoria.Controllers
{
    public class OuvidorController : Controller
    {
        public async Task<IActionResult> Index(string busca)
        {
            if (string.IsNullOrEmpty(busca)) busca = "*";

            HttpClient c = new HttpClient();
            var res =  await c.GetAsync(ApiUrl.Str($"manifestacao/nao-respondidas/{busca}"));

            string json = await res.Content.ReadAsStringAsync();
            List<ManifestacaoDTO> mans = JsonConvert.DeserializeObject<List<ManifestacaoDTO>>(json);

            var resEstats = await c.GetAsync(ApiUrl.Str("manifestacao/estatisticas"));
            string jsonStats = await resEstats.Content.ReadAsStringAsync();
            EstatisticaDTO estatistica = JsonConvert.DeserializeObject<EstatisticaDTO>(jsonStats);

            return View(new OuvidorIndexDTO(mans, estatistica, busca));
        }
    }
}