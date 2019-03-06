using api.domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.infra.EntityMap
{
    public class GrupoMap : IEntityTypeConfiguration<Grupo>
    {
        public void Configure(EntityTypeBuilder<Grupo> builder)
        {
            builder.ToTable("Grupo");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Descricao)
                .HasColumnName("descricao")
                .HasMaxLength(40)
                .IsRequired();

            builder.HasMany(x => x.GruposUsuarios)
                .WithOne(x=>x.Grupo)
                .HasForeignKey(x=>x.GrupoId)
                .OnDelete(DeleteBehavior.Restrict); ;

            builder.HasMany(x=>x.PerfisGrupos).
                WithOne(x=>x.Grupo)               
                .HasForeignKey(x=>x.GrupoId)
                .HasPrincipalKey(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
