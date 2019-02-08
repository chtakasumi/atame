using api.domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.infra.EntityConfig
{
    public class DocenteMap : IEntityTypeConfiguration<Docente>
    {     
        public void Configure(EntityTypeBuilder<Docente> builder)
        {
            builder.ToTable("Docente");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .HasColumnName("nome")
                .HasColumnType("varchar(40)")
                .IsRequired();

            builder.Property(c => c.Formacao)
               .HasColumnName("formacao")
               .HasColumnType("varchar(100)").IsRequired();           

        }
       
    }
}
