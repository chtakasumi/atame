using api.domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.infra.EntityConfig
{
    public class CursoMap : IEntityTypeConfiguration<Curso>
    {     
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.ToTable("Curso");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .HasColumnName("nome")
                .HasColumnType("varchar(40)")
                .IsRequired();

            builder.Property(c => c.Preco)
               .HasColumnName("preco").HasColumnType("Decimal(10, 2)").IsRequired();
            
            builder.Property(c => c.Descricao)
              .HasColumnName("descricao")
              .HasColumnType("varchar(500)");


            builder.Property(c => c.TipoCursoId)
                .HasColumnName("tipoCursoId");
            
            builder.HasOne(c => c.TipoCurso)         
               .WithMany()
               .HasForeignKey(x => x.TipoCursoId)           
               .OnDelete(DeleteBehavior.Restrict);

        }
       
    }
}
