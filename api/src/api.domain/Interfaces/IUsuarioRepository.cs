using System.Collections.Generic;
using api.domain.Entity;
using api.domain.Services.DTO;

namespace api.domain.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Usuario Autenticar(string login, string senha);
        IEnumerable<Usuario> BuscarComGrupo(int usuarioId);
        IEnumerable<Usuario> Listar(UsuarioDTO dto);
        ICollection<GrupoUsuario> BuscarGrupos(int id);
        List<Grupo> ListarGrupo();
        void RemoverGrupoUsuario(int id);
    }
}
