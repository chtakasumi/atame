using api.domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.infra.EntityConfig
{
    public class PerfilMap : IEntityTypeConfiguration<Perfil>
    {
        public void Configure(EntityTypeBuilder<Perfil> builder)
        {
            builder.ToTable("perfil");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Descricao)
                .HasColumnName("descricao")               
                .HasMaxLength(40)
                .IsRequired();
          
            builder.HasMany(x=>x.PerfisGrupos).
                WithOne(x=>x.Perfil)
                .HasForeignKey(x=>x.PerfilId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
