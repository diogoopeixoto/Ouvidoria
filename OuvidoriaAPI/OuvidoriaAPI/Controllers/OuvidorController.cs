using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OuvidoriaAPI.DTO;
using OuvidoriaAPI.Service;

namespace OuvidoriaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OuvidorController : ControllerBase
    {
        private static List<SessaoTokenDTO> sessoesOuvidoresLogados = new List<SessaoTokenDTO>();

        public static SessaoTokenDTO GetSessao(string token)
        {
            if (string.IsNullOrEmpty(token)) return null;
            token = token.Replace("Bearer ", "");
            return sessoesOuvidoresLogados.FirstOrDefault(s => s.Token.Equals(token));
        }

        [Authorize]
        [HttpGet("man")]
        public string Man()
        {
            return "Ok foi";
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult Login(LoginDTO login)
        {
            OuvidorService svc = new OuvidorService();
            ResultadoAcao res = svc.Login(login.Email, login.Senha);

            if (res.Status != StatusRetorno.OK)
                return StatusCode((int)res.Status, res.Message);

            SessaoTokenDTO sessao = res.Data as SessaoTokenDTO;
            sessoesOuvidoresLogados.Add(sessao);
       
            return Ok(sessao.Token);
        }


    }
}
