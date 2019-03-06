using api.domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.infra.EntityMap
{
    public class MunicipioMap : IEntityTypeConfiguration<Municipio>
    {     
        public void Configure(EntityTypeBuilder<Municipio> builder)
        {
            builder.ToTable("Municipio");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .HasColumnName("nome")
                .HasColumnType("varchar(60)")
                .IsRequired();

            builder.Property(c => c.Codigo)
               .HasColumnName("codigo")
               .HasColumnType("varchar(20)");

            builder.Property(c => c.UFId)
               .HasColumnName("UFId")
               .IsRequired();

            builder.HasOne(c => c.UF).WithMany()
               .HasForeignKey(c => c.UFId)
              .HasPrincipalKey(x => x.Id)
              .OnDelete(DeleteBehavior.Restrict);
        }
       
    }
}
