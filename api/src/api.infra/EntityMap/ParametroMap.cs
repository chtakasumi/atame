using api.domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.infra.EntityMap
{
    public class ParametroMap : IEntityTypeConfiguration<Parametro>
    {     
        public void Configure(EntityTypeBuilder<Parametro> builder)
        {
            builder.ToTable("Parametro");

            builder.HasKey(c => c.Id);


            builder.HasIndex(e => e.Chave).IsUnique();

            builder.Property(c => c.Chave)
                .HasColumnName("chave")
                .HasColumnType("varchar(100)")
                .IsRequired();


            builder.Property(c => c.valor)
                .HasColumnName("valor")
                .HasColumnType("varchar(100)")
                .IsRequired();


            builder.Property(c => c.Descricao)
               .HasColumnName("descricao")
               .HasColumnType("varchar(1000)");
               
        }
       
    }
}
