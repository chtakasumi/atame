using api.domain.Entity;
using System.Collections.Generic;

namespace api.domain.Interfaces
{
    public interface IMunicipioRepository : IRepository<Municipio>
    {
        IEnumerable<Municipio> Listar(Municipio municipio);
    }
}
