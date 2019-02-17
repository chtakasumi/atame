using api.domain.Entity;
using api.domain.Services.DTO;
using System.Collections.Generic;

namespace api.domain.Interfaces
{
    public interface ICursoRepository : IRepository<Curso>
    {
        IEnumerable<Curso> Listar(CursoDTO cursoVo);


        CursoDocente PesquisarVinculoDocente(int id);      
        CursoDocente VincularDocente(CursoDocente entidade);
        void ExcluirVinculoDocente(CursoDocente cd);

        CursoConteudoProgramatico PesquisarVinculoConteudoProgramatico(int id);
        CursoConteudoProgramatico VincularConteudoProgramatico(CursoConteudoProgramatico entidade);
        void ExcluirVinculoConteudoProgramatico(CursoConteudoProgramatico ctp);
        
       
    }
}
