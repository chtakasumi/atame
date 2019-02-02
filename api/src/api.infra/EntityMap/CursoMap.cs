﻿using api.domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.infra.EntityConfig
{
    public class CursoMap : IEntityTypeConfiguration<Curso>
    {     
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.ToTable("Curso");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .HasColumnName("nome")
                .HasColumnType("varchar(40)")
                .IsRequired();

        }
       
    }
}