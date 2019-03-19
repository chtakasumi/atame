using api.domain.Entity;
using System.Collections.Generic;

namespace api.domain.Interfaces
{
    public interface IParametroRepository : IRepository<Parametro>
    {
        IEnumerable<Parametro> Listar(Parametro vendedor);
    }
}
