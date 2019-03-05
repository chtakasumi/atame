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
            modelBuilder.ApplyConfiguration(new TurmaMap());
            modelBuilder.ApplyConfiguration(new CursoDocenteMap());
            modelBuilder.ApplyConfiguration(new CursoConteudoProgramaticoMap());
            modelBuilder.ApplyConfiguration(new TurmaDocenteMap());            
            modelBuilder.ApplyConfiguration(new TurmaConteudoProgramaticoMap());
            modelBuilder.ApplyConfiguration(new MunicipioMap());
            modelBuilder.ApplyConfiguration(new UFMap());
            modelBuilder.ApplyConfiguration(new ClienteMap());
            modelBuilder.ApplyConfiguration(new VendaMap());
            modelBuilder.ApplyConfiguration(new VendaClienteMap());

        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Perfil> Perfis { get; set; }
        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<TipoCurso> TipoCursos { get; set; }
        public DbSet<Docente> Docentes { get; set; }
        public DbSet<ConteudoProgramatico> ConteudoProgramaticos { get; set; }
        public DbSet<Vendedor> Vendedores { get; set; }
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<Municipio> Municipios { get; set; }
        public DbSet<UF> UFs { get; set; }
        public DbSet<Cliente> Cleintes { get; set; }
        public DbSet<Venda> Vendas { get; set; }

    }
}
