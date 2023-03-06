namespace OuvidoriaAPI.Domain
{
    public class Ouvidor
    {
        public Ouvidor()
        {
           
        }
        
        internal Ouvidor(string nome, 
            string email,
            string senha)
        {
            if (string.IsNullOrEmpty(nome))
                throw new ArgumentException("O nome é obrigatório");

            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("O email é obrigatório");

            if (string.IsNullOrEmpty(senha))
                throw new ArgumentException("A senha é obrigatória");

            Id = Guid.NewGuid();
            Nome = nome;
            Email = email;
            Senha = senha;
        }
        
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
