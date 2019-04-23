using api.domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.infra.EntityMap
{
    public class ComissaoMap : IEntityTypeConfiguration<Comissao>
    {
        public void Configure(EntityTypeBuilder<Comissao> builder)
        {
            builder.ToTable("Comissao");

            builder.Property(c => c.FaturamentoId)
                .HasColumnName("faturamentoId")
                .IsRequired();

            builder.HasOne(c => c.Faturamento)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(c => c.Percentual)
                .HasColumnName("percentual")
                .HasColumnType("decimal(10,3)")
                .IsRequired();

            builder.Property(c => c.ValorApagar)
               .HasColumnName("valorApagar")
               .HasColumnType("decimal(10,2)")
               .IsRequired();

            builder.Property(c => c.Status)
              .HasColumnName("status")
              .IsRequired();

            builder.Property(c => c.Gerente)
                .HasColumnName("gerente")
                .IsRequired();

            builder.Property(c => c.ValorPago)
              .HasColumnName("valorPago")
              .HasColumnType("decimal(10,2)");

            builder.Property(c => c.Observacao)
            .HasColumnName("observacao")
              .HasColumnType("varchar(1000)");

            builder.Property(c => c.DataPagamento)
             .HasColumnName("dataPagamento");
        }

    }
}
