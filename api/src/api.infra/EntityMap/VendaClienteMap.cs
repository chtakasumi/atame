using api.domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.infra.EntityMap
{
    public class VendaClienteMap : IEntityTypeConfiguration<VendaCliente>
    {     
        public void Configure(EntityTypeBuilder<VendaCliente> builder)
        {
            builder.ToTable("VendaCliente");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.ClienteAcademicoId)
               .HasColumnName("clienteAcademicoId")           
               .IsRequired();

            builder.HasOne(cd => cd.ClienteAcademico).WithMany()                 
                .OnDelete(DeleteBehavior.Restrict);

        }
       
    }
}
