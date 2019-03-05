using api.domain.Entity;
using System.Collections.Generic;

namespace api.domain.Interfaces
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        IEnumerable<Cliente> Listar(ClienteDTO vendedor);
    }
}
