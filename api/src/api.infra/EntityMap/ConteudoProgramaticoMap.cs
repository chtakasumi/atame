using api.domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.infra.EntityMap
{
    public class ConteudoProgramaticoMap : IEntityTypeConfiguration<ConteudoProgramatico>
    {     
        public void Configure(EntityTypeBuilder<ConteudoProgramatico> builder)
        {
            builder.ToTable("ConteudoProgramatico");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Identificacao)
                .HasColumnName("nome")
                .HasColumnType("varchar(40)")
                .IsRequired();

            builder.Property(c => c.Descricao)
               .HasColumnName("descricao")
               .HasColumnType("varchar(500)");           

        }
       
    }
}
