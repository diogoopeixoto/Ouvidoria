using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OuvidoriaAPI.DTO;
using OuvidoriaAPI.Service;

namespace OuvidoriaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManifestacaoController : ControllerBase
    {
        private readonly ManifestacaoService ms = null;

        public ManifestacaoController()
        {
            ms = new ManifestacaoService();
        }

        [HttpGet("estatisticas")]
        public ActionResult<EstatisticaDTO> Stats()
        {
            ResultadoAcao resMans = ms.Estatiticas();
            return StatusCode((int)resMans.Status, resMans.Data);
        }

        [HttpGet("nao-respondidas/{busca}")]
        public ActionResult<List<ManifestacaoDTO>> Listar(string? busca = "")
        {
            ResultadoAcao resMans = ms.NaoRespondidos(busca);
            return StatusCode((int)resMans.Status, resMans.Data);
        }

        [HttpPost("registrar")]
        public ActionResult RegistrarMan(CriarManifestacaoDTO criar)
        {
            ResultadoAcao resCri = ms.Criar(criar);
            return StatusCode((int)resCri.Status, resCri.Data);
        }

        [HttpGet("ver/{id}")]
        public ActionResult VerMan(Guid id)
        {
            var token = Request.Headers["Authorization"];
            var sessao = OuvidorController.GetSessao(token);
            if (sessao == null) return Unauthorized();
            ResultadoAcao resGet = ms.Visualizar(id, sessao.Ouvidor.Id);
            return StatusCode((int)resGet.Status, resGet.Data);
        }

        [HttpPut("responder")]
        public ActionResult Responder(ResponderManifestacaoDTO resp)
        {
            ResultadoAcao resResp = ms.Responder(resp);
            return StatusCode((int)resResp.Status, resResp.Data);
        }

        [HttpDelete("excluir/{id}")]
        public ActionResult Excluir(Guid id)
        {
            var token = Request.Headers["Authorization"];
            var sessao = OuvidorController.GetSessao(token);
            ResultadoAcao resRem = ms.Excluir(id, sessao.Ouvidor.Id);
            return StatusCode((int)resRem.Status, resRem.Data);
        }
    }
}
