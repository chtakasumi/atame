using System.Collections.Generic;
using api.domain.Entity;

namespace api.domain.Interfaces
{
    public interface IUFRepository : IRepository<UF>
    {
        IEnumerable<UF> Listar(UF uf);
    }
}
