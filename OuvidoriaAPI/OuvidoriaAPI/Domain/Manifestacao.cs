using System.Text;
using OuvidoriaAPI.Enum;

namespace OuvidoriaAPI.Domain
{
    public class Manifestacao
    {
        public Manifestacao()
        { }

        internal Manifestacao(
            Setor setor,
            PerfilManifestacao perfil,
            TipoSolicitacao tipoSolicitacao,

            string nome,
            string email,
            string celular,

            string campus,
            string curso,

            string assunto,
            string conteudo,

            string anexoBase64)
        {
            Id = Guid.NewGuid();

            if (string.IsNullOrEmpty(nome))
                throw new ArgumentException("Seu nome é obrigatório");

            if (nome.Split(' ').Length < 2)
                throw new ArgumentException("Informe um nome e sobrenome");

            if (string.IsNullOrEmpty(celular))
                throw new ArgumentException("Deixe um número celular para contato");

            if (string.IsNullOrEmpty(campus))
                throw new ArgumentException("Informe o Campus o qual você pertence");

            if (campus.Length < 5)
                throw new ArgumentException("O nome do Campus é muito curto; Informe corretamente o Campus o qual você pertence");

            if (string.IsNullOrEmpty(curso))
                throw new ArgumentException("Informe o seu curso");

            if (curso.Length < 5)
                throw new ArgumentException("O nome do curso é muito curto; Informe corretamente o curso o qual você pertence");

            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("Deixe seu email!");

            if (!email.Contains("@"))
                throw new ArgumentException("O email informado é inválido");

            if (string.IsNullOrEmpty(assunto))
                throw new ArgumentException("O assunto é obrigatório");

            if (assunto.Length < 10)
                throw new ArgumentException("A descrição do assunto é muito curta; mínimo 5 caracteres");

            if (assunto.Length > 50)
                throw new ArgumentException("A descrição do assunto é muito longa; máximo 40 caracteres");

            if (string.IsNullOrEmpty(conteudo))
                throw new ArgumentException("O assunto é obrigatório");

            if (conteudo.Length < 20)
                throw new ArgumentException("O conteúdo da manifestação muito curto; mínimo 5 caracteres");

            if (conteudo.Length > 1000)
                throw new ArgumentException("A conteúdo da manifestação é muito longo; máximo 1000 caracteres");

            if (setor == null)
                throw new ArgumentException("O setor não foi informado");

            DataCriacao = DateTime.Now;
            Nome = nome;
            Email = email;
            Celular = celular;
            Perfil = perfil;
            Campus = campus;
            Curso = curso;
            TipoSolicitacao = tipoSolicitacao;
            Assunto = assunto;
            Conteudo = conteudo;
            SetorId = setor.Id;
            if (!string.IsNullOrEmpty(anexoBase64))
                Anexo = Convert.FromBase64String(anexoBase64);
        }

        public void Visualizar(Ouvidor quemVisualizou)
        {
            if (Visualizada()) return;

            if (quemVisualizou == null) throw new ArgumentException("Ouvidor não foi informado");
            DataVisualizacao = DateTime.Now;
            VisOuvidorId = quemVisualizou.Id;
        }

        public bool Visualizada()
        {
            return DataVisualizacao.HasValue;
        }

        public bool Respondida()
        {
            return Resposta != null;
        }

        public RespostaManifestacao Responder(string texto,
            AcaoResposta acao,
            Guid? setorIdEncaminhar = null,
            string emailSetorEncaminhar = null,
            byte[] anexo = null)
        {
            if (!Visualizada()) throw new InvalidOperationException("Esta manifestação ainda não foi visualizada");
            if (Respondida()) throw new InvalidOperationException("Esta manifestação já foi respondida");

            Resposta = new RespostaManifestacao(this,
                acao,
                texto,
                setorIdEncaminhar,
                emailSetorEncaminhar,
                anexo);

            if (setorIdEncaminhar.HasValue)
                SetorId = setorIdEncaminhar.Value;

            RespostaId = Resposta.Id;
            return Resposta;
        }

        internal void Excluir(Ouvidor quemExcluiu)
        {
            if (Excluido) return;
            if (!Visualizada()) throw new InvalidOperationException("Não é possível excluir uma manifestação que ainda não foi visualizada");
            if (Respondida()) throw new InvalidOperationException("Não é possível excluir uma manifestação que já foi respondida");

            Excluido = true;
            DataExclusao = DateTime.Now;
            OuvidorIdExclusao = quemExcluiu.Id;
        }

        public DateTime? DataVisualizacao { get; private set; }

        public Guid Id { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public PerfilManifestacao Perfil { get; private set; }
        public TipoSolicitacao TipoSolicitacao { get; private set; }


        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Celular { get; private set; }

        public string Campus { get; private set; }
        public string Curso { get; private set; }

        public string Assunto { get; private set; }
        public string Conteudo { get; private set; }

        public byte[]? Anexo { get; private set; }

        public bool Excluido { get; private set; }
        public DateTime? DataExclusao { get; private set; }
        public Guid? OuvidorIdExclusao { get; private set; }

        public Guid SetorId { get; private set; }
        public virtual Setor Setor { get; private set; }

        public Guid? RespostaId { get; private set; }
        public virtual RespostaManifestacao Resposta { get; private set; }

        public Guid? VisOuvidorId { get; private set; }
        public virtual Ouvidor VisOuvidor { get; private set; }
    }
}