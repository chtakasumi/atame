using api.domain.Entity;
using api.domain.Services.DTO;
using System.Collections.Generic;

namespace api.domain.Interfaces
{
    public interface ICursoRepository : IRepository<Curso>
    {
        IEnumerable<Curso> Listar(CursoDTO cursoVo);

        void ExcluirVinculoDocente(CursoDocente cd);
        CursoDocente PesquisarVinculoDocente(int id);

        CursoConteudoProgramatico PesquisarVinculoConteudoProgramatico(int id);
        void ExcluirVinculoConteudoProgramatico(CursoConteudoProgramatico ctp);
        CursoConteudoProgramatico VincularConteudoProgramatico(CursoConteudoProgramatico entidade);
        CursoDocente VincularDocente(CursoDocente entidade);
    }
}
