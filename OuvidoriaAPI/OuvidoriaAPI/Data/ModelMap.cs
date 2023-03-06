using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using OuvidoriaAPI.Domain;
using OuvidoriaAPI.Enum;

namespace OuvidoriaAPI.Data
{
    public class ModelMap
    {
        internal static void All(ModelBuilder mb)
        {
            MapSetor(mb);
            MapManifestacao(mb);
            MapOuvidor(mb);
            MapResp(mb);
    
        }

        private static void MapResp(ModelBuilder mb)
        {
            EntityTypeBuilder<RespostaManifestacao> tb = mb.Entity<RespostaManifestacao>();
            tb.HasKey(x => x.Id);
            tb.Property(x => x.EmailSetorEncaminhar).IsRequired(false).HasMaxLength(60);
            tb.Property(x => x.Acao).IsRequired(true).HasDefaultValue(AcaoResposta.ResponderManifestante);
            tb.Property(x => x.Texto).IsRequired(true).HasMaxLength(1000);
        }

        private static void MapSetor(ModelBuilder mb)
        {
            EntityTypeBuilder<Setor> tb = mb.Entity<Setor>();
            tb.HasKey(x => x.Id);
            tb.Property(x => x.Nome).IsRequired().HasMaxLength(50);
        }

        private static void MapOuvidor(ModelBuilder mb)
        {
            EntityTypeBuilder<Ouvidor> tb = mb.Entity<Ouvidor>();
            tb.Property(x => x.Nome).IsRequired().HasMaxLength(60);
            tb.Property(x => x.Email).IsRequired().HasMaxLength(50);
            tb.Property(x => x.Senha).IsRequired().HasMaxLength(255);
        }

        private static void MapManifestacao(ModelBuilder mb)
        {
            EntityTypeBuilder<Manifestacao> tb = mb.Entity<Manifestacao>();
            tb.Property(x => x.Nome).IsRequired().HasMaxLength(60);
            tb.Property(x => x.Email).IsRequired().HasMaxLength(100);
            tb.Property(x => x.Celular).IsRequired().HasMaxLength(15);
            tb.Property(x => x.Perfil).IsRequired();
            tb.Property(x => x.Campus).IsRequired().HasMaxLength(60);
            tb.Property(x => x.Curso).IsRequired().HasMaxLength(60);
            tb.Property(x => x.TipoSolicitacao).IsRequired();
            tb.Property(x => x.Assunto).IsRequired().HasMaxLength(60);
            tb.Property(x => x.Conteudo).IsRequired().HasMaxLength(1000);
            tb.Property(x => x.Anexo).IsRequired(false);
        }
    }
}
