namespace OuvidoriaAPI.Domain
{
    public class Setor
    {
        public Setor()
        {

        }

        internal Setor(string nome)
        {
            Id = Guid.NewGuid();
            Nome = nome;
        }


        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public bool Inativo { get; private set; }
    }
}
