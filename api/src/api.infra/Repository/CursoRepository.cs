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

        public IEnumerable<Curso> Listar(CursoDTO cursoVo)
        {
            var ListaCursos = base._dbContexto.Cursos.Where(x =>
                (x.Id == cursoVo.Id || !cursoVo.Id.HasValue) &&
                (x.TipoCursoId == cursoVo.TipoCursoId || !cursoVo.TipoCursoId.HasValue) &&
                (EF.Functions.Like(x.Nome, "%" + cursoVo.Nome + "%") || string.IsNullOrEmpty(cursoVo.Nome)
             )).Include(t => t.TipoCurso)
             .Include(c => c.Docentes).ThenInclude(c => c.Docente)
             .Include(c=>c.ConteudosProgramaticos).ThenInclude(c => c.ConteudoProgramatico).Take(500);



            return ListaCursos.ToList();
        }

        public void ExcluirVinculoDocente(CursoDocente cd)
        {
            base._dbContexto.Remove(cd);
            base._dbContexto.SaveChanges();
        }

        public CursoDocente PesquisarVinculoDocente(int id)
        {      
            return _dbContexto.Set<CursoDocente>().Find(id);
        }


        public void ExcluirVinculoConteudoProgramatico(CursoConteudoProgramatico ccp)
        {
            base._dbContexto.Remove(ccp);
            base._dbContexto.SaveChanges();
        }

        public CursoConteudoProgramatico PesquisarVinculoConteudoProgramatico(int id)
        {
            return _dbContexto.Set<CursoConteudoProgramatico>().Find(id);
        }

        public CursoConteudoProgramatico VincularConteudoProgramatico(CursoConteudoProgramatico entidade)
        {
            _dbContexto.Set<CursoConteudoProgramatico>().Add(entidade);
            _dbContexto.SaveChanges();
            return entidade;
        }

        public CursoDocente VincularDocente(CursoDocente entidade)
        {
            _dbContexto.Set<CursoDocente>().Add(entidade);
            _dbContexto.SaveChanges();
            return entidade;
        }
    }
}
