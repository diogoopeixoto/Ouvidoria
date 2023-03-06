using System.Reflection.Metadata;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OuvidoriaAPI.Domain;

namespace OuvidoriaAPI.Data
{
    public class OuvidoriaContext : DbContext
    {
        public virtual DbSet<Manifestacao> Manifestacoes { get; set; }
        public virtual DbSet<RespostaManifestacao> Respostas { get; set; }
        public virtual DbSet<Ouvidor> Ouvidores { get; set; }
        public virtual DbSet<Setor> Setores { get; set; }


        public static string UsrDb { get; private set; }
        public static string SenhaDb { get; private set; }

        public static void ConfiguraDb(string usr, string senha)
        {
            UsrDb = usr;
            SenhaDb = senha;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder();
            sb.DataSource = "localhost";
            sb.UserID = UsrDb;
            sb.Password = SenhaDb;
            sb.InitialCatalog = "OuvidoriaAPI";

            optionsBuilder.UseSqlServer(sb.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            ModelMap.All(mb);
        }
    }
}
