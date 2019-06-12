using System.Collections.Generic;
using api.domain.Entity;

namespace api.domain.Interfaces
{
    public interface IGrupoRepository : IRepository<Grupo>
    {
        IEnumerable<Grupo> Listar(Grupo grupoVo);     
        List<Perfil> ListarPerfil();
        void RemoverPerfilGrupos(int id);
    }
}
