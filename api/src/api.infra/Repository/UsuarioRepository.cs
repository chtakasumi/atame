using api.domain.Entity;
using api.domain.Interfaces;
using api.domain.Services.DTO;
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
            return base.Query(usuario => usuario.Id == usuarioId, u=>u.Id)
                .Include(usuario => usuario.GruposUsuarios)
                .ThenInclude(gruposUsuarios => gruposUsuarios.Grupo).ToList();
        }

        public ICollection<GrupoUsuario> BuscarGrupos(int id)
        {
            Usuario usuario = base.Query(x => x.Id == id, x => x.Id).Include(u => u.GruposUsuarios).ThenInclude(gruposUsuarios => gruposUsuarios.Grupo).Single();

            return usuario.GruposUsuarios;

        }

        public IEnumerable<Usuario> Listar(UsuarioDTO dto)
        {
            return base.Query(x =>
                (x.Id == dto.Id || !dto.Id.HasValue)&&    
                (EF.Functions.Like(x.Login, "%" + dto.Login + "%") || string.IsNullOrEmpty(dto.Login)) &&
                (x.Vendedor.Id == dto.VendedorId || !dto.VendedorId.HasValue) &&
                (x.Ativo == dto.Ativo || string.IsNullOrEmpty(dto.Ativo)),x=>x.Id).Include(x=>x.Vendedor).ToList();
        }
    }
}
