using Microsoft.EntityFrameworkCore;
using Ouvidoria.Models.ViewModels;

namespace Ouvidoria.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {
        }

        public DbSet<Users> users { get; set; }
    }
}