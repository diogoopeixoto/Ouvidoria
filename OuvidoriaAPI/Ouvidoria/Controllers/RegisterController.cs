using Microsoft.AspNetCore.Mvc;

namespace Ouvidoria.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
