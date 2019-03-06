using api.domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.infra.EntityMap
{
    public class TurmaMap : IEntityTypeConfiguration<Turma>
    {     
        public void Configure(EntityTypeBuilder<Turma> builder)
        {
            builder.ToTable("Turma");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Identificacao)
                .HasColumnName("Identificacao")
                .HasColumnType("varchar(60)")
                .IsRequired();

            builder.Property(c => c.CursoId)
             .HasColumnName("cursoId")
             .IsRequired();
            
            builder.Property(c => c.Preco)
               .HasColumnName("preco")
               .HasColumnType("Decimal(10, 2)")
               .IsRequired();
            
            builder.Property(c => c.Inicio)
              .HasColumnName("inicioPrevisto")
              .HasColumnType("Date");

            builder.Property(c => c.Fim)
              .HasColumnName("fimPrevisto")
              .HasColumnType("Date");
            
            builder.HasOne(c => c.Curso).WithMany()
                .HasForeignKey(c => c.CursoId)
               .HasPrincipalKey(x => x.Id)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Docentes)
                .WithOne(cd => cd.Turma)
                .HasForeignKey(cd=>cd.TurmaId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasMany(c => c.ConteudosProgramaticos)
               .WithOne(cp => cp.Turma)
               .HasForeignKey(cp => cp.TurmaId)
               .HasPrincipalKey(c => c.Id)
               .OnDelete(DeleteBehavior.Cascade);
        }       
    }
}
