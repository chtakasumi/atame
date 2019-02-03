using api.domain.Entity;
using api.domain.Interfaces;
using api.domain.Services.DTO;
using api.infra.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace api.infra.Repository
{
    public class CursoRepository : EfRepository<Curso>, ICursoRepository
    {
        public CursoRepository(DBContext dbbcontexto) : base(dbbcontexto)
        {
          
        }

        public IEnumerable<Curso> Listar(CursoListarVo cursoVo)
        {
            var ListaCursos = base._dbContexto.Cursos.Where(x =>
                (x.Id == cursoVo.Id || !cursoVo.Id.HasValue) &&
                (x.TipoCursoId == cursoVo.TipoCursoId || !cursoVo.TipoCursoId.HasValue) &&
                (EF.Functions.Like(x.Nome, "%" + cursoVo.Nome + "%") || string.IsNullOrEmpty(cursoVo.Nome)
             )).Include(t=>t.TipoCurso).ToList();          

            return ListaCursos;
        }
    }
}
