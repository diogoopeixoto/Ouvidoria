using System;
using System.Text;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;
using OuvidoriaAPI.Data;
using OuvidoriaAPI.Domain;
using OuvidoriaAPI.DTO;

namespace OuvidoriaAPI.Service
{
    public class ManifestacaoService
    {

        public ManifestacaoService()
        {
            CkDir();
        }

        public ResultadoAcao Responder(ResponderManifestacaoDTO resp)
        {
            Func<byte[]> base64ToArray = new Func<byte[]>(() =>
            {
                try
                {
                    if (string.IsNullOrEmpty(resp.AnexoBase64)) return null;
                    return Encoding.ASCII.GetBytes(resp.AnexoBase64);
                }
                catch { return null; }
            });

            try
            {
                byte[] anexoRaw = base64ToArray();

                using (OuvidoriaContext oc = new OuvidoriaContext())
                {
                    Manifestacao man = oc.Manifestacoes.Find(resp.ManifestacaoId);
                    if (man == null) return new ResultadoAcao(message: "Manifestação não encontrada", status: StatusRetorno.NotFound);

                    RespostaManifestacao resposta = man.Responder(resp.Texto,
                        resp.Acao,
                        resp.SetorIdEncaminhar,
                        resp.EmailSetorEncaminhar,
                        anexoRaw);

                    oc.Respostas.Add(resposta);
                    oc.Manifestacoes.Update(man);
                    oc.SaveChanges();

                    //   Task.Run(() =>
                    //    {
                    if (resp.Acao == Enum.AcaoResposta.ResponderManifestante)
                    {
                        EnviaEmailMan(man);
                    }
                    else
                    {
                        EnviaEmailSetor(man);
                    }//  });

                    return new ResultadoAcao("Resposta adicionada");
                }
            }
            catch (Exception ex)
            {
                return new ResultadoAcao(ex);
            }
        }

        private void CkDir()
        {
            if (!Directory.Exists(@"C:\Temp\"))
                Directory.CreateDirectory(@"C:\Temp\");
        }

        private bool EnviaEmailMan(Manifestacao man)
        {
            var tmpFile = $@"C:\Temp\{Guid.NewGuid()}";
            if (man.Resposta.Anexo != null)
                File.WriteAllBytes(tmpFile, man.Resposta.Anexo);

            EmailService es = new EmailService();
            bool env = es.EnviaEmail(man.Email,
                  man.Assunto,
                  man.Resposta.Texto,
                   new string[] { tmpFile });

            if (File.Exists(tmpFile))
                File.Delete(tmpFile);

            return env;
        }

        private bool EnviaEmailSetor(Manifestacao man)
        {
            if (string.IsNullOrEmpty(man.Resposta.EmailSetorEncaminhar))
                return false;

            var tmpFile = $@"C:\Temp\{Guid.NewGuid()}";
            if (man.Resposta.Anexo != null)
                File.WriteAllBytes(tmpFile, man.Resposta.Anexo);

            EmailService es = new EmailService();
           var env =   es.EnviaEmail(man.Resposta.EmailSetorEncaminhar,
                man.Assunto,
                man.Resposta.Texto,
                 new string[] { tmpFile });

            if (File.Exists(tmpFile))
                File.Delete(tmpFile);

            return env;
        }

        public ResultadoAcao Visualizar(Guid manifestacaoId, Guid ouvidorId)
        {
            try
            {
                using (OuvidoriaContext oc = new OuvidoriaContext())
                {

                    Manifestacao man = oc.Manifestacoes.Find(manifestacaoId);
                    if (man == null) return new ResultadoAcao(message: "Manifestação não encontrada", status: StatusRetorno.NotFound);

                    Ouvidor o = oc.Ouvidores.Find(ouvidorId);
                    man.Visualizar(o);

                    oc.SaveChanges();
                    return new ResultadoAcao("Visualizado com sucesso",
                        data: man);
                }
            }
            catch (Exception ex)
            {
                return new ResultadoAcao(ex);
            }
        }

        public ResultadoAcao Criar(CriarManifestacaoDTO criar)
        {
            try
            {
                using (OuvidoriaContext oc = new OuvidoriaContext())
                {
                    Setor setor = oc.Setores.Find(criar.SetorId);
                    if (setor == null) throw new Exception("O setor não foi informado");

                    Manifestacao man = new Manifestacao(
                          setor,
                          criar.Perfil,
                          criar.TipoSolicitacao,
                          criar.Nome,
                          criar.Email,
                          criar.Celular,
                          criar.Campus,
                          criar.Curso,
                          criar.Assunto,
                          criar.Conteudo,
                          criar.AnexoBase64
                        );

                    oc.Manifestacoes.Add(man);
                    oc.SaveChanges();

                    return new ResultadoAcao(data: man.Id, message: "Registrado com sucesso");
                }
            }
            catch (Exception ex)
            {
                return new ResultadoAcao(ex);
            }
        }

        internal ResultadoAcao NaoRespondidos(string busca)
        {
            try
            {
                if ("*".Equals(busca)) busca = "";
                using (OuvidoriaContext oc = new OuvidoriaContext())
                {
                    List<Manifestacao> mans = oc.Manifestacoes
                        .Where(x => x.Resposta == null && x.Excluido == false &&

                            (
                                x.Nome.Contains(busca) ||
                                x.Email.Contains(busca) ||
                                x.Setor.Nome.Contains(busca) ||
                                x.Assunto.Contains(busca) ||
                                x.Conteudo.Contains(busca) ||
                                x.Resposta.Texto.Contains(busca)
                            )

                         )
                        .Include(x => x.Resposta)
                            .ThenInclude(r => r.Ouvidor)
                        .Include(x => x.VisOuvidor)
                        .Include(x => x.Setor)
                        .ToList();

                    List<ManifestacaoDTO> dtos = new List<ManifestacaoDTO>();
                    mans.ForEach(x => dtos.Add(new ManifestacaoDTO(x)));

                    return new ResultadoAcao("Visualizado com sucesso",
                        data: dtos);
                }
            }
            catch (Exception ex)
            {
                return new ResultadoAcao(ex);
            }
        }
        public ResultadoAcao Excluir(Guid manifestacaoId, Guid ouvidorId)
        {
            try
            {
                using (OuvidoriaContext oc = new OuvidoriaContext())
                {

                    Manifestacao man = oc.Manifestacoes.Find(manifestacaoId);
                    if (man == null) return new ResultadoAcao(message: "Manifestação não encontrada", status: StatusRetorno.NotFound);
                    
                    Ouvidor o = oc.Ouvidores.Find(ouvidorId);
                    man.Excluir(o);

                    oc.Manifestacoes.Update(man);
                    oc.SaveChanges();

                    return new ResultadoAcao("Excluir com sucesso",
                        data: man);
                }
            }
            catch (Exception ex)
            {
                return new ResultadoAcao(ex);
            }
        }

        internal ResultadoAcao Estatiticas()
        {
            try
            {
                using (OuvidoriaContext oc = new OuvidoriaContext())
                {
                    EstatisticaDTO s = new EstatisticaDTO()
                    {
                        Total = oc.Manifestacoes.Count(),
                        Pendentes = oc.Manifestacoes.Count(x => x.Resposta == null),
                        Excluidas = oc.Manifestacoes.Count(x => x.Excluido),
                        Vencidas = 0,
                        Respondidas = oc.Manifestacoes.Count(x => x.Resposta != null)
                    };
                    return new ResultadoAcao("Ok", data: s);
                }
            }
            catch (Exception ex)
            {
                return new ResultadoAcao(ex);
            }
        }
    }
}
