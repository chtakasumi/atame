using api.domain.Entity;
using System.Collections.Generic;

namespace api.domain.Interfaces
{
    public interface IBancoRepository : IRepository<Banco>
    {
        IEnumerable<Banco> Listar(Banco Banco);
    }
}
