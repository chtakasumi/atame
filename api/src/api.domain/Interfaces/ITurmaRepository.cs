using api.domain.Entity;
using api.domain.Services.DTO;
using System.Collections.Generic;

namespace api.domain.Interfaces
{
    public interface ITurmaRepository : IRepository<Turma>
    {
        IEnumerable<Turma> Listar(TurmaDTO TurmaVo);


        TurmaDocente PesquisarVinculoDocente(int id);
        TurmaDocente VincularDocente(TurmaDocente entidade);
        void ExcluirVinculoDocente(TurmaDocente cd);


        TurmaConteudoProgramatico PesquisarVinculoConteudoProgramatico(int id);
        TurmaConteudoProgramatico VincularConteudoProgramatico(TurmaConteudoProgramatico entidade);
        void ExcluirVinculoConteudoProgramatico(TurmaConteudoProgramatico ctp);
      




    }
}
