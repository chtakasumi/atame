using api.domain.Entity;
using System.Collections.Generic;

namespace api.domain.Interfaces
{
    public interface IEmpresaRepository : IRepository<Empresa>
    {
        IEnumerable<Empresa> Listar(EmpresaDTO vendedor);
    }
}
