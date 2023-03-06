namespace OuvidoriaAPI.DTO
{
    public class SessaoTokenDTO
    {
        public string Token { get; private set; }
        public OuvidorDTO Ouvidor { get; private set; }
        public DateTime DtExpirar { get; private set; }


        public SessaoTokenDTO(string token, OuvidorDTO ouvidor, DateTime dtExpirar)
        {
            Token = token;
            Ouvidor = ouvidor;
            DtExpirar = dtExpirar;
        }
    }
}
