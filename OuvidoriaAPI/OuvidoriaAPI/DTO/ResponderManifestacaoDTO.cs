using OuvidoriaAPI.Enum;

namespace OuvidoriaAPI.DTO
{

    public class ResponderManifestacaoDTO
    {
        public string AnexoBase64 { get; set; }

        public Guid ManifestacaoId { get; set; }
        public string Texto { get; set; }
        public AcaoResposta Acao { get; set; }
        public Guid? SetorIdEncaminhar { get; set; }
        public string EmailSetorEncaminhar { get; set; }
    }
}
