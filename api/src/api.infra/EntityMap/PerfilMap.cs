using api.domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.infra.EntityMap
{
    public class PerfilMap : IEntityTypeConfiguration<Perfil>
    {
        public void Configure(EntityTypeBuilder<Perfil> builder)
        {
            builder.ToTable("Perfil");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Descricao)
                .HasColumnName("descricao")               
                .HasMaxLength(40)
                .IsRequired();
        }
    }
}
