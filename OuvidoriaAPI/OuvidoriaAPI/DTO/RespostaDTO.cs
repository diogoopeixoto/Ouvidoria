using OuvidoriaAPI.Domain;
using OuvidoriaAPI.Enum;

namespace OuvidoriaAPI.DTO
{
    public class RespostaDTO
    {
        public RespostaDTO()
        {

        }

        public RespostaDTO(RespostaManifestacao r)
        {
            Id = r.Id;
            Data = r.Data;
            Texto = r.Texto;
            Anexo = r.Anexo;
            OuvidorId = r.OuvidorId;
            Ouvidor = new OuvidorDTO(r.Ouvidor);
            Acao = r.Acao;
            SetorIdEncaminhar = r.SetorIdEncaminhar;
            EmailSetorEncaminhar = r.EmailSetorEncaminhar;
        }

        public AcaoResposta Acao { get; private set; }
        public Guid? SetorIdEncaminhar { get; set; }
        public string EmailSetorEncaminhar { get; set; }

        public Guid Id { get; set; }
        public DateTime Data { get; set; }

        public string Texto { get; set; }
        public byte[]? Anexo { get; set; }

        public Guid OuvidorId { get; set; }
        public virtual OuvidorDTO Ouvidor { get; set; }
    }
}
