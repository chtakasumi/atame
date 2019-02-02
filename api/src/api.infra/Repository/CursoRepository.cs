using System.Collections.Generic;
using System.Linq;
using api.domain.Entity;
using api.domain.Interfaces;
using api.domain.Services.DTO;
using api.infra.Data;
using Microsoft.EntityFrameworkCore;

namespace api.infra.Repository
{
    public class CursoRepository : EfRepository<Curso>, ICursoRepository
    {
        public CursoRepository(DBContext dbbcontexto) : base(dbbcontexto)
        {
          
        }

        public IEnumerable<Curso> Listar(CursoListarVo cursoVo)
        {
            return base.Pesquisar(x => 
                (x.Id == cursoVo.Id || !cursoVo.Id.HasValue) && 
                (EF.Functions.Like(x.Nome, "%"+cursoVo.Nome+"%") || string.IsNullOrEmpty(cursoVo.Nome))
             );
        }
    }
}
