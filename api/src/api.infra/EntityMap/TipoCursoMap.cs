using api.domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.infra.EntityConfig
{
    public class TipoCursoMap : IEntityTypeConfiguration<TipoCurso>
    {     
        public void Configure(EntityTypeBuilder<TipoCurso> builder)
        {
            builder.ToTable("TipoCurso");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Descricao)
                .HasColumnName("descricao")
                .HasColumnType("varchar(40)")
                .IsRequired();


            builder.Property(c => c.Comissao)
               .HasColumnName("comissao")
               .HasColumnType("numeric(10, 3)")
               .IsRequired();
        }
       
    }
}
