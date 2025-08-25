using Microsoft.EntityFrameworkCore;
using ControleContatos.Models;
using ControleContatos.Data.Map;

namespace ControleContatos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
        public DbSet<ContatoModel> Contato { get; set; }
        public DbSet<UsuarioModel> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContatoMap());
            base.OnModelCreating(modelBuilder);
        }

    }
}
