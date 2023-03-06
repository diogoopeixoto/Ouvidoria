namespace OuvidoriaAPI.DTO
{
    public enum AcaoResposta
    {
        ResponderManifestante = 0,
        EncaminharSetor = 1
    }

    public class ResponderManifestacaoDTO
    {
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

        public Guid ManifestacaoId { get; set; }
        public string Texto { get; set; }
        public AcaoResposta Acao { get; set; }
        public Guid? SetorIdEncaminhar { get; set; }
        public string EmailSetorEncaminhar { get; set; }
    }
}
