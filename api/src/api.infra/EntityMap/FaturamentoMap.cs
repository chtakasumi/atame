using api.domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.infra.EntityMap
{
    public class FaturamentoMap : IEntityTypeConfiguration<Faturamento>
    {
        public void Configure(EntityTypeBuilder<Faturamento> builder)
        {
            builder.ToTable("Faturamento");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.CompetenciaAno)
                .HasColumnName("competenciaAno")
                .IsRequired();

            builder.Property(c => c.CompetenciaMes)
                .HasColumnName("competenciaMes")
                .IsRequired();

            builder.Property(c => c.VendaId)
              .HasColumnName("vendaId")
              .IsRequired();

            builder.HasOne(c => c.Venda).WithMany()
                .HasForeignKey(c => c.VendaId)
                .HasPrincipalKey(x => x.Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(c => c.ParcelaId)
              .HasColumnName("parcelaId")
              .IsRequired();

            builder.HasOne(c => c.Parcela).WithMany()
                .HasForeignKey(c => c.ParcelaId)
                .HasPrincipalKey(x => x.Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(c => c.ValorPago)
            .HasColumnName("valorPago")
            .HasColumnType("Decimal(10, 2)")
            .IsRequired();

            builder.Property(c => c.DataPgto)
            .HasColumnName("dataPgto")
              .HasColumnType("Date")
              .IsRequired();

        }

    }
}
