using OuvidoriaAPI.Enum;

namespace OuvidoriaAPI.DTO
{
    public class CriarManifestacaoDTO
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public PerfilManifestacao Perfil { get; set; }
        public string Campus { get; set; }
        public string Curso { get; set; }
        public TipoSolicitacao TipoSolicitacao { get; set; }
        public string Assunto { get; set; }
        public string Conteudo { get; set; }
        public string? AnexoBase64 { get; set; }
        public Guid SetorId { get; set; }
    }
}
