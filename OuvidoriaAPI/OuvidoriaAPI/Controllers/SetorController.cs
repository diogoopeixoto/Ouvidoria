using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OuvidoriaAPI.DTO;
using OuvidoriaAPI.Service;

namespace OuvidoriaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SetorController : ControllerBase
    {
        [HttpGet("listar")]
        public ActionResult<List<SetorDTO>> Listar()
        {
            SetorService svc = new SetorService();
            ResultadoAcao resSet = svc.Listar();
            return StatusCode((int)resSet.Status, resSet.Data);
        }
    }
}
