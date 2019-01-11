using System.Collections.Generic;
using api.domain.Entity;

namespace api.domain.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Usuario Autenticar(string login, string senha);
        IEnumerable<Usuario> BuscarComGrupo(int usuarioId);
    }
}
