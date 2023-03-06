using OuvidoriaAPI.Domain;

namespace OuvidoriaAPI.DTO
{
    public class SetorDTO
    {
        public override string ToString()
        {
            return $"{Id.ToString().Split('-')[0]} - {Nome}";
        }

        public SetorDTO()
        {
                
        }

        internal SetorDTO(Setor s)
        {
            Id = s.Id;
            Nome = s.Nome;
            Inativo = s.Inativo;
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public bool Inativo { get;  set; }
    }
}
