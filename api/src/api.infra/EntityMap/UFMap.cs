using api.domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.infra.EntityMap
{
    public class UFMap : IEntityTypeConfiguration<UF>
    {     
        public void Configure(EntityTypeBuilder<UF> builder)
        {
            builder.ToTable("UF");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .HasColumnName("nome")
                .HasColumnType("varchar(60)")
                .IsRequired();

            builder.Property(c => c.Sigla)
               .HasColumnName("sigla")
               .HasColumnType("varchar(2)")
               .IsRequired();
            
            builder.HasMany(c => c.Municipios)
               .WithOne(cp => cp.UF)
              .HasForeignKey(cp => cp.UFId)
              .HasPrincipalKey(c => c.Id)
              .OnDelete(DeleteBehavior.Restrict);
        }
       
    }
}
