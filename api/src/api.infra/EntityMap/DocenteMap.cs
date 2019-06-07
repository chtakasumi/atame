using api.domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.infra.EntityMap
{
    public class DocenteMap : IEntityTypeConfiguration<Docente>
    {
        public void Configure(EntityTypeBuilder<Docente> builder)
        {
            builder.ToTable("Docente");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .HasColumnName("nome")
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(c => c.Formacao)
               .HasColumnName("formacao")
               .HasColumnType("varchar(100)")
               .IsRequired();

            builder.Property(c => c.Foto)
              .HasColumnName("foto")
              .HasColumnType("text");

            builder.Property(c => c.Nascimento)
              .HasColumnName("nascimento")
              .HasColumnType("date")
              .IsRequired();

            builder.Property(c => c.Cpf)
              .HasColumnName("cpf")
              .HasColumnType("varchar(30)")
              .IsRequired();

            builder.Property(c => c.Rg)
              .HasColumnName("rg")
              .HasColumnType("varchar(30)")
              .IsRequired();
            
            builder.Property(c => c.Rg)
             .HasColumnName("rg")
             .HasColumnType("varchar(30)")
             .IsRequired();

            builder.Property(c => c.UfExpedicaoId)
               .HasColumnName("ufExpedicaoId");

            builder.HasOne(c => c.UfExpedicao)
                .WithMany()
                .HasForeignKey(c => c.UfExpedicaoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(c => c.OrgaoExpedicaoSiglaDescricao)
                .HasColumnName("orgaoExpedicaoSiglaDescricao")
                .HasColumnType("varchar(255)")
                .IsRequired();
            
            builder.Property(c => c.Telefone)
              .HasColumnName("telefone")
              .HasColumnType("varchar(15)");

            builder.Property(c => c.Celular)
                .HasColumnName("celular")
                .HasColumnType("varchar(15)");

            builder.Property(c => c.Email)
                .HasColumnName("email")
                .HasColumnType("varchar(60)");

            builder.Property(c => c.BancoId)
              .HasColumnName("bancoId");

            builder.HasOne(c => c.Banco)
                .WithMany()
                .HasForeignKey(c => c.BancoId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Property(c => c.Agencia)
              .HasColumnName("agencia")
              .HasColumnType("varchar(100)");

            builder.Property(c => c.Conta)
                       .HasColumnName("Conta")
                       .HasColumnType("varchar(100)");


            builder.Property(c => c.Titularizacao)
                     .HasColumnName("titularizacao")
                     .HasColumnType("varchar(100)");


            builder.Property(c => c.TipoContaDescricao)
                   .HasColumnName("tipoContaDescricao")
                   .HasColumnType("varchar(100)");

            builder.Ignore(c => c.orgaoExpedicao);
            builder.Ignore(c => c.TipoConta);
        }

    }
}
