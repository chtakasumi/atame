using api.domain.Entity;
using api.infra.EntityConfig;
using Microsoft.EntityFrameworkCore;

namespace api.infra
{
    public class DBContext:DbContext
    {
        public DBContext(DbContextOptions<DBContext> option):base(option)
        {
                
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {           
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new PerfilMap());
            modelBuilder.ApplyConfiguration(new GrupoMap());
            modelBuilder.ApplyConfiguration(new CursoMap());
            modelBuilder.ApplyConfiguration(new TipoCursoMap());
            modelBuilder.ApplyConfiguration(new DocenteMap());
            modelBuilder.ApplyConfiguration(new ConteudoProgramaticoMap());
            modelBuilder.ApplyConfiguration(new VendedorMap());

        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Perfil> Perfis { get; set; }
        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<TipoCurso> TipoCursos { get; set; }
        public DbSet<Docente> Docentes { get; set; }
        public DbSet<ConteudoProgramatico> ConteudoProgramaticos { get; set; }
        public DbSet<Vendedor> Vendedores { get; set; }     

    }
}
