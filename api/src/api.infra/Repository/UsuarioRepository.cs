using api.domain.Entity;
using api.domain.Interfaces;
using api.domain.Services.DTO;
using api.infra.Data;
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
            var usuario = Query(x => x.Login == login && x.Senha == senha, x => x.Id)
                .Include(x => x.GruposUsuarios).ThenInclude(x=>x.Grupo)
                .ThenInclude(x=>x.PerfisGrupos).ThenInclude(x=>x.Perfil).ToList();

            return usuario.SingleOrDefault();
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
            var query =  base.Query(x =>
                (x.Id == dto.Id || !dto.Id.HasValue) &&    
                (EF.Functions.Like(x.Login, "%" + dto.Login + "%") || string.IsNullOrEmpty(dto.Login)) &&
                (x.Vendedor.Id == dto.VendedorId || !dto.VendedorId.HasValue) &&
                (x.Ativo == dto.Ativo || string.IsNullOrEmpty(dto.Ativo)),x=>x.Id)
                .Include(x=>x.GruposUsuarios).ThenInclude(g=>g.Grupo)
                .Include(x=>x.Vendedor);


            foreach (var usuario in query)
            {
                usuario.GruposCedidas = usuario.GruposUsuarios.Select(x => x.Grupo).ToList();

                //pegar todos os grupos que não contiver usuario.GruposCedidas
                foreach (var grupo in base._dbContexto.Grupos)
                {
                    if (!usuario.GruposCedidas.Exists(x => x.Id== grupo.Id))
                    {
                        usuario.GruposNaoCedidas.Add(grupo);
                    }

                }
            }


            return query;
        }

        public List<Grupo> ListarGrupo()
        {
            return base._dbContexto.Grupos.ToList();
        }

        public void RemoverGrupoUsuario(int usuarioId)
        {
            var grupoUsuario = _dbContexto.Set<GrupoUsuario>().Where(x => x.UsuarioId == usuarioId).ToList();
            if (grupoUsuario.Count > 0)
            {
                _dbContexto.Set<GrupoUsuario>().RemoveRange(grupoUsuario);
            }
            _dbContexto.SaveChanges();
        }
    }
}
