﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api.infra;

namespace api.infra.Migrations
{
    [DbContext(typeof(DBContext))]
    partial class DBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("api.domain.Entity.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bairro")
                        .HasColumnName("bairro")
                        .HasColumnType("varchar(60)");

                    b.Property<string>("Celular")
                        .HasColumnName("celular")
                        .HasColumnType("varchar(15)");

                    b.Property<string>("Cep")
                        .HasColumnName("cep")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Complemento")
                        .HasColumnName("complemento")
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("CpfCnpj")
                        .IsRequired()
                        .HasColumnName("cpfCnpj")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Email")
                        .HasColumnName("email")
                        .HasColumnType("varchar(60)");

                    b.Property<string>("Endereco")
                        .HasColumnName("endereco")
                        .HasColumnType("varchar(300)");

                    b.Property<int>("MunicipioId")
                        .HasColumnName("municipioId");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnName("nome")
                        .HasColumnType("varchar(60)");

                    b.Property<string>("Numero")
                        .HasColumnName("numero")
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Telefone")
                        .HasColumnName("telefone")
                        .HasColumnType("varchar(15)");

                    b.Property<int>("UFId")
                        .HasColumnName("ufId");

                    b.HasKey("Id");

                    b.HasIndex("MunicipioId");

                    b.HasIndex("UFId");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("api.domain.Entity.ConteudoProgramatico", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .HasColumnName("descricao")
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Identificacao")
                        .IsRequired()
                        .HasColumnName("nome")
                        .HasColumnType("varchar(40)");

                    b.HasKey("Id");

                    b.ToTable("ConteudoProgramatico");
                });

            modelBuilder.Entity("api.domain.Entity.Curso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .HasColumnName("descricao")
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnName("nome")
                        .HasColumnType("varchar(40)");

                    b.Property<decimal>("Preco")
                        .HasColumnName("preco")
                        .HasColumnType("Decimal(10, 2)");

                    b.Property<int>("TipoCursoId")
                        .HasColumnName("tipoCursoId");

                    b.HasKey("Id");

                    b.HasIndex("TipoCursoId");

                    b.ToTable("Curso");
                });

            modelBuilder.Entity("api.domain.Entity.CursoConteudoProgramatico", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ConteudoProgramaticoId");

                    b.Property<int>("CursoId");

                    b.HasKey("Id");

                    b.HasIndex("ConteudoProgramaticoId");

                    b.HasIndex("CursoId");

                    b.ToTable("CursoConteudoProgramatico");
                });

            modelBuilder.Entity("api.domain.Entity.CursoDocente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CursoId");

                    b.Property<int>("DocenteId");

                    b.HasKey("Id");

                    b.HasIndex("CursoId");

                    b.HasIndex("DocenteId");

                    b.ToTable("CursoDocente");
                });

            modelBuilder.Entity("api.domain.Entity.Docente", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Formacao")
                        .IsRequired()
                        .HasColumnName("formacao")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnName("nome")
                        .HasColumnType("varchar(40)");

                    b.HasKey("Id");

                    b.ToTable("Docente");
                });

            modelBuilder.Entity("api.domain.Entity.Grupo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnName("descricao")
                        .HasMaxLength(40);

                    b.HasKey("Id");

                    b.ToTable("Grupo");
                });

            modelBuilder.Entity("api.domain.Entity.GrupoUsuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GrupoId");

                    b.Property<int>("UsuarioId");

                    b.HasKey("Id");

                    b.HasIndex("GrupoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("GrupoUsuario");
                });

            modelBuilder.Entity("api.domain.Entity.Municipio", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Codigo")
                        .HasColumnName("codigo")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnName("nome")
                        .HasColumnType("varchar(60)");

                    b.Property<int?>("UFId")
                        .IsRequired()
                        .HasColumnName("UFId");

                    b.HasKey("Id");

                    b.HasIndex("UFId");

                    b.ToTable("Municipio");
                });

            modelBuilder.Entity("api.domain.Entity.Perfil", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnName("descricao")
                        .HasMaxLength(40);

                    b.HasKey("Id");

                    b.ToTable("Perfil");
                });

            modelBuilder.Entity("api.domain.Entity.PerfilGrupo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GrupoId");

                    b.Property<int>("PerfilId");

                    b.HasKey("Id");

                    b.HasIndex("GrupoId");

                    b.HasIndex("PerfilId");

                    b.ToTable("PerfilGrupo");
                });

            modelBuilder.Entity("api.domain.Entity.Prospeccao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClienteId")
                        .HasColumnName("clienteId");

                    b.Property<DateTime>("Data")
                        .HasColumnName("data")
                        .HasColumnType("Date");

                    b.Property<string>("Observacao")
                        .HasColumnName("observacao")
                        .HasColumnType("varchar(500)");

                    b.Property<int>("VendedorId")
                        .HasColumnName("vendedorId");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.HasIndex("VendedorId");

                    b.ToTable("Prospeccao");
                });

            modelBuilder.Entity("api.domain.Entity.ProspeccaoInteresse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CursoId")
                        .HasColumnName("cursoId");

                    b.Property<int>("ProspeccaoId")
                        .HasColumnName("prospeccaoId");

                    b.HasKey("Id");

                    b.HasIndex("CursoId");

                    b.HasIndex("ProspeccaoId");

                    b.ToTable("ProspeccaoInteresse");
                });

            modelBuilder.Entity("api.domain.Entity.TipoCurso", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Comissao")
                        .HasColumnName("comissao")
                        .HasColumnType("numeric(10, 3)");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnName("descricao")
                        .HasColumnType("varchar(40)");

                    b.HasKey("Id");

                    b.ToTable("TipoCurso");
                });

            modelBuilder.Entity("api.domain.Entity.Turma", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CursoId")
                        .HasColumnName("cursoId");

                    b.Property<DateTime?>("Fim")
                        .HasColumnName("fimPrevisto")
                        .HasColumnType("Date");

                    b.Property<string>("Identificacao")
                        .IsRequired()
                        .HasColumnName("Identificacao")
                        .HasColumnType("varchar(60)");

                    b.Property<DateTime?>("Inicio")
                        .HasColumnName("inicioPrevisto")
                        .HasColumnType("Date");

                    b.Property<decimal>("Preco")
                        .HasColumnName("preco")
                        .HasColumnType("Decimal(10, 2)");

                    b.HasKey("Id");

                    b.HasIndex("CursoId");

                    b.ToTable("Turma");
                });

            modelBuilder.Entity("api.domain.Entity.TurmaConteudoProgramatico", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ConteudoProgramaticoId");

                    b.Property<int>("TurmaId");

                    b.HasKey("Id");

                    b.HasIndex("ConteudoProgramaticoId");

                    b.HasIndex("TurmaId");

                    b.ToTable("TurmaConteudoProgramatico");
                });

            modelBuilder.Entity("api.domain.Entity.TurmaDocente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DocenteId");

                    b.Property<int>("TurmaId");

                    b.HasKey("Id");

                    b.HasIndex("DocenteId");

                    b.HasIndex("TurmaId");

                    b.ToTable("TurmaDocente");
                });

            modelBuilder.Entity("api.domain.Entity.UF", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnName("nome")
                        .HasColumnType("varchar(60)");

                    b.Property<string>("Sigla")
                        .IsRequired()
                        .HasColumnName("sigla")
                        .HasColumnType("varchar(2)");

                    b.HasKey("Id");

                    b.ToTable("UF");
                });

            modelBuilder.Entity("api.domain.Entity.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Ativo")
                        .IsRequired()
                        .HasColumnName("ativo")
                        .HasColumnType("varchar(1)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnName("login")
                        .HasColumnType("varchar(40)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnName("senha")
                        .HasMaxLength(40);

                    b.HasKey("Id");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("api.domain.Entity.Venda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClienteFinanceiroId")
                        .HasColumnName("clienteFinanceiroId");

                    b.Property<DateTime?>("Data")
                        .IsRequired()
                        .HasColumnName("data")
                        .HasColumnType("Date");

                    b.Property<decimal>("Desconto")
                        .HasColumnName("desconto")
                        .HasColumnType("Decimal(10, 3)");

                    b.Property<int>("Quantidade")
                        .HasColumnName("quantidade");

                    b.Property<int>("TurmaId")
                        .HasColumnName("turmaId");

                    b.Property<decimal>("ValorCurso")
                        .HasColumnName("valorCurso")
                        .HasColumnType("Decimal(10, 2)");

                    b.Property<decimal>("ValorVenda")
                        .HasColumnName("valorVenda")
                        .HasColumnType("Decimal(10, 2)");

                    b.Property<int>("VendedorId")
                        .HasColumnName("vendedorId");

                    b.HasKey("Id");

                    b.HasIndex("ClienteFinanceiroId");

                    b.HasIndex("TurmaId");

                    b.HasIndex("VendedorId");

                    b.ToTable("Venda");
                });

            modelBuilder.Entity("api.domain.Entity.VendaCliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClienteAcademicoId")
                        .HasColumnName("clienteAcademicoId");

                    b.Property<int>("VendaId");

                    b.HasKey("Id");

                    b.HasIndex("ClienteAcademicoId");

                    b.HasIndex("VendaId");

                    b.ToTable("VendaCliente");
                });

            modelBuilder.Entity("api.domain.Entity.Vendedor", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnName("nome")
                        .HasColumnType("varchar(40)");

                    b.HasKey("Id");

                    b.ToTable("Vendedor");
                });

            modelBuilder.Entity("api.domain.Entity.Cliente", b =>
                {
                    b.HasOne("api.domain.Entity.Municipio", "Municipio")
                        .WithMany()
                        .HasForeignKey("MunicipioId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("api.domain.Entity.UF", "UF")
                        .WithMany()
                        .HasForeignKey("UFId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("api.domain.Entity.Curso", b =>
                {
                    b.HasOne("api.domain.Entity.TipoCurso", "TipoCurso")
                        .WithMany()
                        .HasForeignKey("TipoCursoId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("api.domain.Entity.CursoConteudoProgramatico", b =>
                {
                    b.HasOne("api.domain.Entity.ConteudoProgramatico", "ConteudoProgramatico")
                        .WithMany()
                        .HasForeignKey("ConteudoProgramaticoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("api.domain.Entity.Curso", "Curso")
                        .WithMany("ConteudosProgramaticos")
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("api.domain.Entity.CursoDocente", b =>
                {
                    b.HasOne("api.domain.Entity.Curso", "Curso")
                        .WithMany("Docentes")
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("api.domain.Entity.Docente", "Docente")
                        .WithMany()
                        .HasForeignKey("DocenteId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("api.domain.Entity.GrupoUsuario", b =>
                {
                    b.HasOne("api.domain.Entity.Grupo", "Grupo")
                        .WithMany("GruposUsuarios")
                        .HasForeignKey("GrupoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("api.domain.Entity.Usuario", "Usuario")
                        .WithMany("GruposUsuarios")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("api.domain.Entity.Municipio", b =>
                {
                    b.HasOne("api.domain.Entity.UF", "UF")
                        .WithMany("Municipios")
                        .HasForeignKey("UFId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("api.domain.Entity.PerfilGrupo", b =>
                {
                    b.HasOne("api.domain.Entity.Grupo", "Grupo")
                        .WithMany("PerfisGrupos")
                        .HasForeignKey("GrupoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("api.domain.Entity.Perfil", "Perfil")
                        .WithMany("PerfisGrupos")
                        .HasForeignKey("PerfilId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("api.domain.Entity.Prospeccao", b =>
                {
                    b.HasOne("api.domain.Entity.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("api.domain.Entity.Vendedor", "Vendedor")
                        .WithMany()
                        .HasForeignKey("VendedorId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("api.domain.Entity.ProspeccaoInteresse", b =>
                {
                    b.HasOne("api.domain.Entity.Curso", "Curso")
                        .WithMany()
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("api.domain.Entity.Prospeccao", "Prospeccao")
                        .WithMany("Interesses")
                        .HasForeignKey("ProspeccaoId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("api.domain.Entity.Turma", b =>
                {
                    b.HasOne("api.domain.Entity.Curso", "Curso")
                        .WithMany()
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("api.domain.Entity.TurmaConteudoProgramatico", b =>
                {
                    b.HasOne("api.domain.Entity.ConteudoProgramatico", "ConteudoProgramatico")
                        .WithMany()
                        .HasForeignKey("ConteudoProgramaticoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("api.domain.Entity.Turma", "Turma")
                        .WithMany("ConteudosProgramaticos")
                        .HasForeignKey("TurmaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("api.domain.Entity.TurmaDocente", b =>
                {
                    b.HasOne("api.domain.Entity.Docente", "Docente")
                        .WithMany()
                        .HasForeignKey("DocenteId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("api.domain.Entity.Turma", "Turma")
                        .WithMany("Docentes")
                        .HasForeignKey("TurmaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("api.domain.Entity.Venda", b =>
                {
                    b.HasOne("api.domain.Entity.Cliente", "ClienteFinanceiro")
                        .WithMany()
                        .HasForeignKey("ClienteFinanceiroId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("api.domain.Entity.Turma", "Turma")
                        .WithMany()
                        .HasForeignKey("TurmaId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("api.domain.Entity.Vendedor", "Vendedor")
                        .WithMany()
                        .HasForeignKey("VendedorId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("api.domain.Entity.VendaCliente", b =>
                {
                    b.HasOne("api.domain.Entity.Cliente", "ClienteAcademico")
                        .WithMany()
                        .HasForeignKey("ClienteAcademicoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("api.domain.Entity.Venda", "Venda")
                        .WithMany("ClientesAcademicos")
                        .HasForeignKey("VendaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
