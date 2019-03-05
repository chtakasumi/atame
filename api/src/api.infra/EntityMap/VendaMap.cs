using api.domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.infra.EntityConfig
{
    public class VendaMap : IEntityTypeConfiguration<Venda>
    {
        public void Configure(EntityTypeBuilder<Venda> builder)
        {
            builder.ToTable("Venda");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Data)
                .HasColumnName("data")
                .HasColumnType("Date")
                .IsRequired();

            builder.Property(c => c.Quantidade)
                .HasColumnName("quantidade")
                .IsRequired();

            builder.Property(c => c.ValorCurso)
               .HasColumnName("valorCurso")
                .HasColumnType("Decimal(10, 2)")
               .IsRequired();
            
            builder.Property(c => c.Desconto)
                .HasColumnName("desconto")
                .HasColumnType("Decimal(10, 3)");
            
            builder.Property(c => c.ValorVenda)
                .HasColumnName("valorVenda")
                .HasColumnType("Decimal(10, 2)")
                .IsRequired();
            
            builder.Property(c => c.TurmaId)
                .HasColumnName("turmaId")
                .IsRequired();

            builder.HasOne(c => c.Turma).WithMany()
                  .HasForeignKey(c => c.TurmaId)
                  .HasPrincipalKey(x => x.Id)
                  .OnDelete(DeleteBehavior.Restrict);
            
            builder.Property(c => c.ClienteFinanceiroId)
                .HasColumnName("clienteFinanceiroId")
                .IsRequired();

            builder.HasOne(c => c.ClienteFinanceiro).WithMany()
                .HasForeignKey(c => c.ClienteFinanceiroId)
                .HasPrincipalKey(x => x.Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(c => c.VendedorId)
                .HasColumnName("vendedorId")
                .IsRequired();

            builder.HasOne(c => c.Vendedor).WithMany()
                .HasForeignKey(c => c.VendedorId)
                .HasPrincipalKey(x => x.Id)
                .OnDelete(DeleteBehavior.Restrict);

            

            builder.HasMany(c => c.ClientesAcademicos)
                 .WithOne(cp => cp.Venda)
                .HasForeignKey(cp => cp.VendaId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
