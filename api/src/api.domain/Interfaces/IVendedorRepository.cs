using api.domain.Entity;
using System.Collections.Generic;

namespace api.domain.Interfaces
{
    public interface IVendedorRepository : IRepository<Vendedor>
    {
        IEnumerable<Vendedor> Listar(Vendedor vendedor);
    }
}
