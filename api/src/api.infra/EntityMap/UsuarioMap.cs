using api.domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.infra.EntityConfig
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {     
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Login)
                .HasColumnName("login")
                .HasColumnType("varchar(40)")
                .IsRequired();

            builder.Property(c => c.Senha)
                .HasColumnName("senha")
                .HasMaxLength(40)
                .IsRequired();

            builder.Property(c => c.Ativo)
                 .HasColumnName("ativo")
                .HasColumnType("varchar(1)")
                 .IsRequired();

            builder.HasMany(x => x.GruposUsuarios)
                .WithOne(x=>x.Usuario)
                .HasForeignKey(x=>x.UsuarioId)
                .HasPrincipalKey(x=>x.Id)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
