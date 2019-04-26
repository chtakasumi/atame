using api.domain.Entity;
using System.Collections.Generic;

namespace api.domain.Interfaces
{
    public interface IDescontoRepository : IRepository<Desconto>
    {
        IEnumerable<Desconto> Listar(Desconto Desconto);      
    }
}
