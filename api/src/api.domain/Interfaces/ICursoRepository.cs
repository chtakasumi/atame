using api.domain.Entity;
using api.domain.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace api.domain.Interfaces
{
    public interface ICursoRepository : IRepository<Curso>
    {
        IEnumerable<Curso> Listar(CursoDTO cursoVo);
    }
}
