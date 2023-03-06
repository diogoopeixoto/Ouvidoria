using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using OuvidoriaAPI.Data;
using OuvidoriaAPI.Domain;
using OuvidoriaAPI.DTO;

namespace OuvidoriaAPI.Service
{
    public class OuvidorService
    {
        public ResultadoAcao Login(string email, string senha)
        {
            try
            {
                using (OuvidoriaContext ctx = new OuvidoriaContext())
                {
                    Ouvidor o = ctx.Ouvidores.FirstOrDefault(o =>
                        o.Email.Equals(email) &&
                        o.Senha.Equals(senha)
                    );

                    if (o == null)
                        return new ResultadoAcao(status: StatusRetorno.NotFound, message: "Usuário ou senha inválidos");

                    Tuple<string, DateTime> token = JwtToken(o);
                    SessaoTokenDTO sessao = new SessaoTokenDTO(
                        token: token.Item1,
                        dtExpirar: token.Item2,
                        ouvidor: new OuvidorDTO(o));

                    return new ResultadoAcao(
                            data: sessao);
                }
            }
            catch (Exception ex)
            {
                return new ResultadoAcao(ex);
            }
        }

        public ResultadoAcao ObterPorEmail(string email)
        {
            try
            {
                using (OuvidoriaContext oc = new OuvidoriaContext())
                {
                    Ouvidor o = oc.Ouvidores.FirstOrDefault(x =>
                        x.Email.Equals(email));
                    if (o == null)
                        return new ResultadoAcao(status: StatusRetorno.NotFound,
                            message: $"Ouvidor não localizado pelo email '{email}'");

                    return new ResultadoAcao(data: new OuvidorDTO(o));
                }
            }
            catch (Exception ex)
            {
                return new ResultadoAcao(ex);
            }
        }

        private Tuple<string, DateTime> JwtToken(Ouvidor ouv)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes("O98I7UYWE9R8TY4HG5F6DS3X2CV1BHJ9H8GF5TG2YH");
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Email", ouv.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenStr = tokenHandler.WriteToken(token);

            return new Tuple<string, DateTime>(tokenStr, tokenDescriptor.Expires.Value);
        }
    }
}
