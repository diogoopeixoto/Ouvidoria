using OuvidoriaAPI.DTO;

namespace OuvidoriaAPI.DTO
{
    public class OuvidorIndexDTO
    {
        public  IReadOnlyCollection<ManifestacaoDTO> Manifestacoes { get; private set; }
        public EstatisticaDTO Estatistica { get; private set; }

        public string TermoBusca { get; private set; }

        public OuvidorIndexDTO(IReadOnlyCollection<ManifestacaoDTO> manifestacoes, EstatisticaDTO estatistica,
            string termoBusca)
        {
            Manifestacoes = manifestacoes;
            Estatistica = estatistica;
            TermoBusca = termoBusca;
            if ("*".Equals(TermoBusca))
                TermoBusca = "";
        }
    }
}
