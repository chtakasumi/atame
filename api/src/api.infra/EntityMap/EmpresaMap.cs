﻿using api.domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.infra.EntityMap
{
    public class EmpresaMap : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.ToTable("empresa");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.RazaoSocial)
                .HasColumnName("razaoSocial")
                .HasColumnType("varchar(60)")
                .IsRequired();
            
            builder.Property(c => c.Cnpj)
               .HasColumnName("cnpj")
               .HasColumnType("varchar(20)")
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

            builder.Property(c => c.MunicipioId)
                .HasColumnName("municipioId")
                .IsRequired();

            builder.HasOne(c => c.Municipio)
                .WithMany()
                .HasForeignKey(c => c.MunicipioId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(c => c.UFId)
                .HasColumnName("ufId")
                .IsRequired();

            builder.HasOne(c => c.UF)
                .WithMany()
                .HasForeignKey(c => c.UFId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(c => c.Endereco)
                .HasColumnName("endereco")
                .HasColumnType("varchar(300)");

            builder.Property(c => c.Numero)
                .HasColumnName("numero")
                .HasColumnType("varchar(10)");

            builder.Property(c => c.Complemento)
                .HasColumnName("complemento")
                .HasColumnType("varchar(1000)");
            
            builder.Property(c => c.Bairro)
                .HasColumnName("bairro")
                .HasColumnType("varchar(60)");

            builder.Property(c => c.Cep)
                .HasColumnName("cep")
                .HasColumnType("varchar(20)");

        }
    }
}
