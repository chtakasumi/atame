using api.domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.infra.EntityMap
{
    public class CursoMap : IEntityTypeConfiguration<Curso>
    {     
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.ToTable("Curso");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .HasColumnName("nome")
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(c => c.Preco)
               .HasColumnName("preco")
               .HasColumnType("Decimal(10, 2)")
               .IsRequired();
            
            builder.Property(c => c.Descricao)
              .HasColumnName("descricao")
              .HasColumnType("varchar(1000)");


            builder.Property(c => c.Parcela)
               .HasColumnName("parcela")
               .IsRequired();

            builder.Property(c => c.ValorParcela)
               .HasColumnName("valorParcela")
                .HasColumnType("Decimal(10, 2)")
               .IsRequired();
            

            builder.Property(c => c.TipoCursoId)
                .HasColumnName("tipoCursoId");
            
            builder.HasOne(c => c.TipoCurso).WithMany()
                .HasForeignKey(c => c.TipoCursoId)   
               .HasPrincipalKey(x=>x.Id)             
               .OnDelete(DeleteBehavior.Restrict);
         
            builder.HasMany(c => c.Docentes)
                .WithOne(cd => cd.Curso)
                .HasForeignKey(cd=>cd.CursoId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasMany(c => c.ConteudosProgramaticos)
                .WithOne(cp => cp.Curso)
               .HasForeignKey(cp => cp.CursoId)
               .HasPrincipalKey(c => c.Id)
               .OnDelete(DeleteBehavior.Cascade);

        }
       
    }
}
