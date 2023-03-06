using Microsoft.AspNetCore.Mvc;
using OuvidoriaAPI.DTO;

namespace Ouvidoria.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Login(LoginDTO login)
        {
            HttpClient api = new HttpClient();
            HttpResponseMessage resp = await api.PostAsJsonAsync(
                ApiUrl.Str("ouvidor/login"),
                login);

            if (resp.IsSuccessStatusCode)
            {
                string token = await resp.Content.ReadAsStringAsync();
                HttpContext.Session.SetString("token", token);
                return Redirect("/Ouvidor");
            }
            string msg = await resp.Content.ReadAsStringAsync();
            return View("/Login");
        }
    }
}
