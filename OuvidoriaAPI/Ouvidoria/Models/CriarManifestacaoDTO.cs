using System.Text;
using OuvidoriaAPI.Enum;

namespace OuvidoriaAPI.DTO
{
    public class CriarManifestacaoDTO
    {
        public Guid Id { get; set; }

 

        public string Nome { get; set; }
        public string Email { get; set; }
        public string Celular { get; set; }
        public PerfilManifestacao Perfil { get; set; }
        public string Campus { get; set; }
        public string Curso { get; set; }
        public TipoSolicitacao TipoSolicitacao { get; set; }
        public string Assunto { get; set; }
        public string Conteudo { get; set; }


        public IFormFile AnexoUp { get; set; }
        private string _base64;
        public string AnexoBase64
        {
            get
            {

                if (!string.IsNullOrEmpty(_base64))
                    return _base64;

                if (AnexoUp != null)
                {
                    using (Stream st = AnexoUp.OpenReadStream())
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            st.CopyTo(ms);
                            byte[] buffer = new byte[ms.Length];
                            ms.Read(buffer, 0, buffer.Length);
                            _base64 = Convert.ToBase64String(buffer);
                        }
                    }
                }

                return _base64;
              
            }
        }

        public Guid SetorId { get; set; }
    }
}
