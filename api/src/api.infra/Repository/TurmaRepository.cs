using api.domain.Entity;
using api.domain.Interfaces;
using api.domain.Services.DTO;
using api.infra.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace api.infra.Repository
{
    public class TurmaRepository : EfRepository<Turma>, ITurmaRepository
    {
        public TurmaRepository(DBContext dbbcontexto) : base(dbbcontexto)
        {
          
        }     

        public IEnumerable<Turma> Listar(TurmaDTO TurmaVo)
        {
            var ListaTurmas = base._dbContexto.Turmas.Where(x =>
                (x.Id == TurmaVo.Id || !TurmaVo.Id.HasValue) &&
                (x.CursoId == TurmaVo.CursoId || !TurmaVo.CursoId.HasValue) &&
                (EF.Functions.Like(x.Identificacao, "%" + TurmaVo.Identificacao + "%") || string.IsNullOrEmpty(TurmaVo.Identificacao)&&
                ((x.Inicio >= TurmaVo.Inicio || !TurmaVo.Inicio.HasValue) &&
                (x.Inicio <= TurmaVo.Fim || !TurmaVo.Fim.HasValue))

             )).Include(t => t.Curso)
             .Include(c => c.Docentes).ThenInclude(c => c.Docente)
             .Include(c=>c.ConteudosProgramaticos).ThenInclude(c => c.ConteudoProgramatico).Take(500);

            return ListaTurmas.ToList();
        }

        public void ExcluirVinculoDocente(TurmaDocente cd)
        {
            base._dbContexto.Remove(cd);
            base._dbContexto.SaveChanges();
        }

        public TurmaDocente PesquisarVinculoDocente(int id)
        {      
            return _dbContexto.Set<TurmaDocente>().Find(id);
        }


        public void ExcluirVinculoConteudoProgramatico(TurmaConteudoProgramatico ccp)
        {
            base._dbContexto.Remove(ccp);
            base._dbContexto.SaveChanges();
        }

        public TurmaConteudoProgramatico PesquisarVinculoConteudoProgramatico(int id)
        {
            return _dbContexto.Set<TurmaConteudoProgramatico>().Find(id);
        }

        public TurmaConteudoProgramatico VincularConteudoProgramatico(TurmaConteudoProgramatico entidade)
        {
            _dbContexto.Set<TurmaConteudoProgramatico>().Add(entidade);
            _dbContexto.SaveChanges();
            return entidade;
        }

        public TurmaDocente VincularDocente(TurmaDocente entidade)
        {
            _dbContexto.Set<TurmaDocente>().Add(entidade);
            _dbContexto.SaveChanges();
            return entidade;
        }
    }
}
