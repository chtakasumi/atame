﻿using api.domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.infra.EntityMap
{
    public class ProspeccaoMap : IEntityTypeConfiguration<Prospeccao>
    {
        public void Configure(EntityTypeBuilder<Prospeccao> builder)
        {
            builder.ToTable("Prospeccao");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Data)
                .HasColumnName("data")
                .HasColumnType("Date")
                .IsRequired();

            builder.Property(c => c.Observacao)
               .HasColumnName("observacao")
               .HasColumnType("varchar(500)");

            builder.Property(c => c.ClienteNome)
              .HasColumnName("nome")
              .HasColumnType("varchar(60)"); 

            builder.Property(c => c.ClienteTelefone)
              .HasColumnName("telefone")
              .HasColumnType("varchar(15)");

            builder.Property(c => c.ClienteCelular)
                .HasColumnName("celular")
                .HasColumnType("varchar(15)");

            builder.Property(c => c.ClienteEmail)
                .HasColumnName("email")
                .HasColumnType("varchar(60)");


            builder.Property(c => c.VendedorId)
           .HasColumnName("vendedorId")
           .IsRequired();

            builder.HasOne(c => c.Vendedor).WithMany()
           .HasForeignKey(c => c.VendedorId)
           .HasPrincipalKey(x => x.Id)
           .OnDelete(DeleteBehavior.Restrict);
            

            builder.HasMany(c => c.Interesses)
                .WithOne(i => i.Prospeccao)
                .HasForeignKey(i => i.ProspeccaoId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
