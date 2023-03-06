using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OuvidoriaAPI.Data;

namespace OuvidoriaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {

        [HttpGet("")]
        public async Task<ActionResult> Status()
        {
            try
            {
                using (OuvidoriaContext db = new OuvidoriaContext())
                {
                    await db.Setores.CountAsync();
                    await db.Ouvidores.CountAsync();
                }
                return Ok($"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}] Serviço totalmente operante");
            }
            catch (Exception ex)
            {
                return Ok($"[{DateTime.Now.ToString("dd/MM/yyyy HH:mm")}] Serviço parcialmente operante: {ex.Message}");
            }
        }
    }
}
