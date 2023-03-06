using System.ComponentModel.DataAnnotations;
using OuvidoriaAPI.Enum;

namespace OuvidoriaAPI.Domain
{
    public class RespostaManifestacao
    {
        public Guid Id { get; private set; }
        public DateTime Data { get; private set; }

        public string Texto { get; private set; }
        public byte[]? Anexo { get; private set; }

        public AcaoResposta Acao { get; private set; }
        public Guid? SetorIdEncaminhar { get; set; }
        public string EmailSetorEncaminhar { get; set; }

        public Guid OuvidorId { get; private set; }
        public virtual Ouvidor Ouvidor { get; private set; }

        public bool EmailEnviado { get; set; }

        

        public RespostaManifestacao()
        { }

        internal RespostaManifestacao(Manifestacao manifestacao,
            AcaoResposta acao,

            string texto,
            Guid? setorIdEncaminhar = null,
            string emailEncaminhar = null,
            byte[] anexo = null)
        {
            Acao = acao;
            SetorIdEncaminhar = setorIdEncaminhar;
            EmailSetorEncaminhar = emailEncaminhar;

            if (manifestacao == null)
                throw new ArgumentException("A manifestação não foi informada");
            if (!manifestacao.Visualizada())
                throw new InvalidOperationException("Não é possível responder a uma manifestação que não foi visualizada;");

            if (string.IsNullOrEmpty(texto))
                throw new ArgumentException("O informe um texto de resposta");
            if (texto.Length < 20)
                throw new ArgumentException("O texto de resposta é muito curto; mínimo 20 caracteres");
            if (texto.Length > 1000)
                throw new ArgumentException("O texto de resposta é muito longo; máximo 1000 caracteres");

            if (anexo != null)
                if (anexo.Length > 10240000) // ~10mb
                    throw new ArgumentException("O anexo é grande demais; tamanho máx. 10mb");

            Id = Guid.NewGuid();
            Data = DateTime.Now;
            Texto = texto;
            OuvidorId = manifestacao.VisOuvidorId.Value;
            Anexo = anexo;
        }

    }
}
