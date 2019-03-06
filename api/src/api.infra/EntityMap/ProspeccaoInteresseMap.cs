using api.domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.infra.EntityMap
{
    public class ProspeccaoInteresseMap : IEntityTypeConfiguration<ProspeccaoInteresse>
    {     
        public void Configure(EntityTypeBuilder<ProspeccaoInteresse> builder)
        {
            builder.ToTable("ProspeccaoInteresse");

            builder.HasKey(c => c.Id);
                      

            builder.Property(c => c.ProspeccaoId)
           .HasColumnName("prospeccaoId")
           .IsRequired();

            builder.HasOne(cd => cd.Prospeccao)
                .WithMany(p=> p.Interesses)
              .OnDelete(DeleteBehavior.Restrict);
            
            builder.Property(c => c.CursoId)
            .HasColumnName("cursoId")
            .IsRequired();

            builder.HasOne(cd => cd.Curso)
                .WithMany()
               .OnDelete(DeleteBehavior.Restrict);


        }
       
    }
}
