using api.domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.infra.EntityConfig
{
    public class VendedorMap : IEntityTypeConfiguration<Vendedor>
    {     
        public void Configure(EntityTypeBuilder<Vendedor> builder)
        {
            builder.ToTable("Vendedor");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .HasColumnName("nome")
                .HasColumnType("varchar(40)")
                .IsRequired();
        }
       
    }
}
