using api.domain.Entity;
using api.domain.Interfaces;
using api.infra.Data;
using api.libs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace api.infra.Repository
{
    public class UsuarioRepository : EfRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(DBContext dbbcontexto) : base(dbbcontexto)
        {
          
        }

        public Usuario Autenticar(string login, string senha)
        {
            return base.Pesquisar(x => x.Login == login && x.Senha == senha).ToEntity();        
        }

        public IEnumerable<Usuario> BuscarComGrupo(int usuarioId)
        {
            return base._dbContexto.Usuarios.Where(usuario => usuario.Id == usuarioId)
                .Include(usuario => usuario.GruposUsuarios)
                .ThenInclude(gruposUsuarios => gruposUsuarios.Grupo).ToList();
        }
    }
}
