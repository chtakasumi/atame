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
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Perfil> Perfis { get; set; }
        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<GrupoUsuario> GruposUsuarios { get; set; }

    }
}
