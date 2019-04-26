using api.domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.infra.EntityMap
{
    public class DescontoMap : IEntityTypeConfiguration<Desconto>
    {     
        public void Configure(EntityTypeBuilder<Desconto> builder)
        {
            builder.ToTable("Desconto");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Descricao)
                .HasColumnName("descricao")
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(c => c.Percentual)
              .HasColumnName("percentual")
              .HasColumnType("Decimal(10, 3)")
              .IsRequired();
        }
       
    }
}
