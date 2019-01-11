using api.domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.infra.EntityConfig
{
    public class GrupoUsuarioMap : IEntityTypeConfiguration<GrupoUsuario>
    {
        public void Configure(EntityTypeBuilder<GrupoUsuario> builder)
        {
            builder.ToTable("gruposusuario");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Usuario)
                .WithMany(x => x.GruposUsuarios)
                .HasForeignKey(x => x.UsuarioId)
                .HasPrincipalKey(x => x.Id);

            builder.HasOne(x => x.Grupo)
                .WithMany(x => x.GruposUsuarios)
                .HasForeignKey(x => x.GrupoId);               

        }

        
    }
}
