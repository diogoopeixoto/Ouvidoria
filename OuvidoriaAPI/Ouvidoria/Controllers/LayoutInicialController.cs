using Microsoft.AspNetCore.Mvc;

namespace Ouvidoria.Controllers
{
    public class LayoutInicialController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
