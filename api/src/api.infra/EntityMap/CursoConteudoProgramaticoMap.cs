using api.domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.infra.EntityConfig
{
    public class CursoConteudoProgramaticoMap : IEntityTypeConfiguration<CursoConteudoProgramatico>
    {     
        public void Configure(EntityTypeBuilder<CursoConteudoProgramatico> builder)
        {
            builder.ToTable("CursoConteudoProgramatico");

            builder.HasKey(c => c.Id);


            builder.HasOne(cd => cd.ConteudoProgramatico).WithMany()                 
                .OnDelete(DeleteBehavior.Restrict);


        }
       
    }
}
