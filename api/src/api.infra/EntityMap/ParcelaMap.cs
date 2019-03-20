using api.domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.infra.EntityMap
{
    public class ParcelaMap : IEntityTypeConfiguration<Parcela>
    {     
        public void Configure(EntityTypeBuilder<Parcela> builder)
        {
            builder.ToTable("Parcela");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Numero)
                .HasColumnName("numero")               
                .IsRequired();

            builder.Property(c => c.PrevisaoPgto)
               .HasColumnName("previsaoPgto")
               .HasColumnType("Date")
               .IsRequired();

            builder.Property(c => c.Preco)
                        .HasColumnName("preco")
                          .HasColumnType("Decimal(10, 2)")
                        .IsRequired();

               

            builder.Property(c => c.Status)
               .HasColumnName("Status")              
               .IsRequired();

            builder.Property(c => c.VendaId)
               .HasColumnName("vendaId")
               .IsRequired();

            builder.HasOne(c => c.Venda)
               .WithMany(v=>v.Parcelas)
               .HasForeignKey(c => c.VendaId)
               .HasPrincipalKey(x => x.Id)
               .OnDelete(DeleteBehavior.Restrict);
        }       
    }
}
