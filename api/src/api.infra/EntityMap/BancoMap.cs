using api.domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.infra.EntityMap
{
    public class BancoMap : IEntityTypeConfiguration<Banco>
    {     
        public void Configure(EntityTypeBuilder<Banco> builder)
        {
            builder.ToTable("Banco");

            builder.HasKey(c => c.Id);
            
            builder.Property(c => c.Codigo)
             .HasColumnName("codigo")
             .HasColumnType("varchar(40)");

            builder.Property(c => c.Nome)
                .HasColumnName("nome")
                .HasColumnType("varchar(100)")
                .IsRequired();
        }
       
    }
}
