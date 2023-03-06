using OuvidoriaAPI.Domain;

namespace OuvidoriaAPI.DTO
{
    public class OuvidorDTO
    {
        public OuvidorDTO(Ouvidor o)
        {
            Id = o.Id;
            Nome = o.Nome;
            Email = o.Email;
        }

        public OuvidorDTO()
        {

        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}
