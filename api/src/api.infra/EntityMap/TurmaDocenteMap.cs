using api.domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.infra.EntityMap
{
    public class TurmaDocenteMap : IEntityTypeConfiguration<TurmaDocente>
    {     
        public void Configure(EntityTypeBuilder<TurmaDocente> builder)
        {
            builder.ToTable("TurmaDocente");

            builder.HasKey(c => c.Id);
            
            builder.HasOne(cd => cd.Docente).WithMany()                 
                .OnDelete(DeleteBehavior.Restrict);

        }
       
    }
}
