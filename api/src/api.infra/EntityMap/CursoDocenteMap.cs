using api.domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.infra.EntityMap
{
    public class CursoDocenteMap : IEntityTypeConfiguration<CursoDocente>
    {     
        public void Configure(EntityTypeBuilder<CursoDocente> builder)
        {
            builder.ToTable("CursoDocente");

            builder.HasKey(c => c.Id);


            builder.HasOne(cd => cd.Docente).WithMany()                 
                .OnDelete(DeleteBehavior.Restrict);


        }
       
    }
}
