using api.domain.Entity;
using System.Collections.Generic;

namespace api.domain.Interfaces
{
    public interface ITipoCursoRepository : IRepository<TipoCurso>
    {
        IEnumerable<TipoCurso> Listar(TipoCurso tipoCursoVo);
    }
}
