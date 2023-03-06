using Microsoft.AspNetCore.Mvc;

namespace Ouvidoria.Controllers
{
    public class DirecionarManifestacaoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
