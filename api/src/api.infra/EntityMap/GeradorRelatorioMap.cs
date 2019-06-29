using api.domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.infra.EntityMap
{
    public class GeradorRelatorioMap : IEntityTypeConfiguration<GeradorRelatorio>
    {     
        public void Configure(EntityTypeBuilder<GeradorRelatorio> builder)
        {
            builder.ToTable("GeradorRelatorio");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .HasColumnName("nome")
                .HasColumnType("varchar(100)")
                .IsRequired();
            
            builder.Property(c => c.Alias)
                .HasColumnName("alias")
                .HasColumnType("varchar(40)")
                .IsRequired();
            
            builder.Property(c => c.Descricao)
               .HasColumnName("descricao")
               .HasColumnType("varchar(1000)")
               .IsRequired();            

            builder.Property(c => c.Query)
               .HasColumnName("query")
               .HasColumnType("text")
               .IsRequired();
        }
       
    }
}
