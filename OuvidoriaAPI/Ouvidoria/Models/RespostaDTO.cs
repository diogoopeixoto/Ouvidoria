

namespace OuvidoriaAPI.DTO
{
    public class RespostaDTO
    {
        public RespostaDTO()
        {

        }



        public AcaoResposta Acao { get; set; }
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
