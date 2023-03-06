using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using OuvidoriaAPI;
using OuvidoriaAPI.Data;
using OuvidoriaAPI.Domain;
using OuvidoriaAPI.DTO;
using OuvidoriaAPI.Service;

namespace Ouvidoria.Tests
{
    [TestClass]
    public class ManifestacaoTests
    {
        private SetorService setorSvc;
        private ManifestacaoService manifestacaoSvc;
        private OuvidorService ouvidorSvc;

        public ManifestacaoTests()
        {
            string json = File.ReadAllText("appsettings.json");
            TestAppSettings s = JsonConvert.DeserializeObject<TestAppSettings>(json);



            setorSvc = new SetorService();
            manifestacaoSvc = new ManifestacaoService();
            ouvidorSvc = new OuvidorService();

            EmailService.Configure(s.ServidorEmail, s.SSL, s.Porta, s.UsuarioEmail, s.SenhaEmail);
            OuvidoriaContext.ConfiguraDb(s.UsrDB, s.SenhaDB);

            FakeDB.Create();

            using (OuvidoriaContext oc = new OuvidoriaContext())
            {
                oc.Database.ExecuteSqlRaw("delete from Manifestacoes; delete from Respostas;");
            }
        }

        [TestMethod]
        public void LOGIN()
        {
            ResultadoAcao resLgn = ouvidorSvc.Login("fernanda@ugb.com", "4321");
            Assert.IsTrue(resLgn.Status == StatusRetorno.OK);
        }


        [TestMethod]
        public void REJEITAR_LOGIN_INVALIDO()
        {
            ResultadoAcao resLgn = ouvidorSvc.Login("inválido", "inv1234");
            Assert.IsTrue(resLgn.Status == StatusRetorno.NotFound);
        }

        [TestMethod]
        public void RESPONDER_MAN()
        {
            SetorDTO setor = setorSvc.ObterPorNome("Biblioteca");
            CriarManifestacaoDTO criar = new CriarManifestacaoDTO
            {
                SetorId = setor.Id,
                Nome = "Jean Silveira",
                Email = "jean.sva@gmail.com",
                Curso = "Engenharia",
                Campus = "Campus Xpto",
                Celular = "(24)98855-2200",
                Assunto = "Livros faltando página",
                Conteudo = "Boa tarde; Os livros X, Y e Z estão faltando páginas; Já reclamamos com o chefe da biblioteca mas nao resolveu"
            };

            //criar manifest
            ResultadoAcao resCriar = manifestacaoSvc.Criar(criar);
            Guid manId = (Guid)resCriar.Data;
            Assert.IsTrue(resCriar.Status == StatusRetorno.OK, resCriar.Message);

            // obter ouvidor
            ResultadoAcao resOuvidor = ouvidorSvc.ObterPorEmail("rafael@ugb.com");
            OuvidorDTO ouvidor = resOuvidor.Data as OuvidorDTO;
            Assert.IsTrue(resOuvidor.Status == StatusRetorno.OK, resOuvidor.Message);

            //visualizar
            ResultadoAcao resVisualizar = manifestacaoSvc.Visualizar(manId, ouvidor.Id);
            Assert.IsTrue(resVisualizar.Status == StatusRetorno.OK, resOuvidor.Message);

            //responder
            ResponderManifestacaoDTO resp = new ResponderManifestacaoDTO()
            {
                ManifestacaoId = (Guid)resCriar.Data,
                Texto = "Boa tarde; Entendemos e sentimos pela frustração, e trabalharemos para substituir ou reparar os itens da biblioteca que foram mencionados. Atenciosamente, ouvidor xpto.",
                AnexoBase64 = Utils.AnexoResposta
            };

            ResultadoAcao resResp = manifestacaoSvc.Responder(resp);
            Assert.IsTrue(resResp.Status == StatusRetorno.OK, resResp.Message);
        }

        [TestMethod]
        public void VISUALIZAR_MAN()
        {
            SetorDTO setor = setorSvc.ObterPorNome("Biblioteca");
            CriarManifestacaoDTO criar = new CriarManifestacaoDTO
            {
                SetorId = setor.Id,
                Nome = "Jean Silveira",
                Email = "jean.sva@gmail.com",
                Curso = "Engenharia",
                Campus = "Campus Xpto",
                Celular = "(24)98855-2200",
                Assunto = "Livros faltando página",
                Conteudo = "Boa tarde; Os livros X, Y e Z estão faltando páginas; Já reclamamos com o chefe da biblioteca mas nao resolveu"
            };

            ResultadoAcao resCriar = manifestacaoSvc.Criar(criar);
            Assert.IsTrue(resCriar.Status == StatusRetorno.OK, resCriar.Message);

            ResultadoAcao resOuvidor = ouvidorSvc.ObterPorEmail("rafael@ugb.com");
            OuvidorDTO ouvidor = resOuvidor.Data as OuvidorDTO;
            Assert.IsTrue(resOuvidor.Status == StatusRetorno.OK, resOuvidor.Message);

            Guid manId = (Guid)resCriar.Data;
            ResultadoAcao resVisualizar = manifestacaoSvc.Visualizar(manId, ouvidor.Id);
            Assert.IsTrue(resVisualizar.Status == StatusRetorno.OK, resOuvidor.Message);
        }


        [TestMethod]
        public void CRIAR_MAN()
        {
            SetorDTO setor = setorSvc.ObterPorNome("Biblioteca");
            CriarManifestacaoDTO criar = new CriarManifestacaoDTO
            {
                SetorId = setor.Id,
                Nome = "Jean Silveira",
                Email = "jean.sva@gmail.com",
                Curso = "Engenharia",
                Campus = "Campus Xpto",
                Celular = "(24)98855-2200",
                Assunto = "Livros faltando página",
                Conteudo = "Boa tarde; Os livros X, Y e Z estão faltando páginas; Já reclamamos com o chefe da biblioteca mas nao resolveu"
            };

            ResultadoAcao res = manifestacaoSvc.Criar(criar);
            Assert.IsTrue(res.Status == StatusRetorno.OK, res.Message);
        }

        [TestMethod]
        public void REIJEITAR_NOME_INVALIDO()
        {
            SetorDTO setor = setorSvc.ObterPorNome("Biblioteca");
            CriarManifestacaoDTO criar = new CriarManifestacaoDTO
            {
                SetorId = setor.Id,
                Nome = "Jean",
                Email = "jean.sva@gmail.com",
                Curso = "Engenharia",
                Campus = "Campus Xpto",
                Celular = "(24)98855-2200",
                Assunto = "Livros faltando página",
                Conteudo = "Boa tarde; Os livros X, Y e Z estão faltando páginas; Já reclamamos com o chefe da biblioteca mas nao resolveu"
            };

            ResultadoAcao res = manifestacaoSvc.Criar(criar);
            Assert.IsTrue(res.Status == StatusRetorno.BadRequest, res.Message);
        }


        [TestMethod]
        public void REIJEITAR_EMAIL_INVALIDO()
        {
            SetorDTO setor = setorSvc.ObterPorNome("Biblioteca");
            CriarManifestacaoDTO criar = new CriarManifestacaoDTO
            {
                SetorId = setor.Id,
                Nome = "Jean Silveira",
                Email = "jean.sva#invalido.com",
                Curso = "Engenharia",
                Campus = "Campus Xpto",
                Celular = "(24)98855-2200",
                Assunto = "Livros faltando página",
                Conteudo = "Boa tarde; Os livros X, Y e Z estão faltando páginas; Já reclamamos com o chefe da biblioteca mas nao resolveu"
            };

            ResultadoAcao res = manifestacaoSvc.Criar(criar);
            Assert.IsTrue(res.Status == StatusRetorno.BadRequest, res.Message);
        }

        [TestMethod]
        public void REIJEITAR_CONTEUDO_INVALIDO()
        {
            SetorDTO setor = setorSvc.ObterPorNome("Biblioteca");
            CriarManifestacaoDTO criar = new CriarManifestacaoDTO
            {
                SetorId = setor.Id,
                Nome = "Jean Silveira",
                Email = "jean.sva@gmail.com",
                Curso = "Engenharia",
                Campus = "Campus Xpto",
                Celular = "(24)98855-2200",
                Assunto = "Título válido sobre um assunto",
                Conteudo = "To de brinks"
            };

            ResultadoAcao res = manifestacaoSvc.Criar(criar);
            Assert.IsTrue(res.Status == StatusRetorno.BadRequest, res.Message);
        }

        [TestMethod]
        public void REIJEITAR_TITULO_INVALIDO()
        {
            SetorDTO setor = setorSvc.ObterPorNome("Biblioteca");
            CriarManifestacaoDTO criar = new CriarManifestacaoDTO
            {
                SetorId = setor.Id,
                Nome = "Jean Silveira",
                Email = "jean.sva@gmail.com",
                Curso = "Engenharia",
                Campus = "Campus Xpto",
                Celular = "(24)98855-2200",
                Assunto = "Trollei",
                Conteudo = "O formato do conteúdo é válido e aborda um determinado assunto"
            };

            ResultadoAcao res = manifestacaoSvc.Criar(criar);
            Assert.IsTrue(res.Status == StatusRetorno.BadRequest, res.Message);
        }

        [TestMethod]
        public void REIJEITAR_CAMPUS_INVALIDO()
        {
            SetorDTO setor = setorSvc.ObterPorNome("Biblioteca");
            CriarManifestacaoDTO criar = new CriarManifestacaoDTO
            {
                SetorId = setor.Id,
                Nome = "Jean Silveira",
                Email = "jean.sva@gmail.com",
                Curso = "Engenharia",
                Campus = "Sem",
                Celular = "(24)98855-2200",
                Assunto = "Título válido sobre o tema",
                Conteudo = "O formato do conteúdo é válido e aborda um determinado assunto"
            };

            ResultadoAcao res = manifestacaoSvc.Criar(criar);
            Assert.IsTrue(res.Status == StatusRetorno.BadRequest, res.Message);
        }


        [TestMethod]
        public void REIJEITAR_CURSO_INVALIDO()
        {
            SetorDTO setor = setorSvc.ObterPorNome("Biblioteca");
            CriarManifestacaoDTO criar = new CriarManifestacaoDTO
            {
                SetorId = setor.Id,
                Nome = "Jean Silveira",
                Email = "jean.sva@gmail.com",
                Curso = "Sem",
                Campus = "Campus XPTO",
                Celular = "(24)98855-2200",
                Assunto = "Título válido sobre o tema",
                Conteudo = "O formato do conteúdo é válido e aborda um determinado assunto"
            };

            ResultadoAcao res = manifestacaoSvc.Criar(criar);
            Assert.IsTrue(res.Status == StatusRetorno.BadRequest, res.Message);
        }
    }
}