using System.Reflection.Metadata;
using ControleContatos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleContatos.Data.Map
{
    public class ContatoMap : IEntityTypeConfiguration<ContatoModel>
    {
        public void Configure(EntityTypeBuilder<ContatoModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Usuario);
        }

        // Exemplo 
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Blog>()
        //        .HasMany(e => e.Posts)
        //        .WithOne(e => e.Blog)
        //        .HasForeignKey(e => e.BlogId)
        //        .HasPrincipalKey(e => e.Id);
        //}
    }
}
