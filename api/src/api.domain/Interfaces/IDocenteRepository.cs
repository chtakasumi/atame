using api.domain.Entity;
using api.domain.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace api.domain.Interfaces
{
    public interface IDocenteRepository : IRepository<Docente>
    {
        IEnumerable<Docente> Listar(Docente docente);
    }
}
