using api.domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.infra.EntityConfig
{
    public class TurmaConteudoProgramaticoMap : IEntityTypeConfiguration<TurmaConteudoProgramatico>
    {     
        public void Configure(EntityTypeBuilder<TurmaConteudoProgramatico> builder)
        {
            builder.ToTable("TurmaConteudoProgramatico");

            builder.HasKey(c => c.Id);
            
            builder.HasOne(cd => cd.ConteudoProgramatico).WithMany()                 
                .OnDelete(DeleteBehavior.Restrict);


        }
       
    }
}
