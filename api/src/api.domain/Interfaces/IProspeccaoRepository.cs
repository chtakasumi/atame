using api.domain.Entity;
using api.domain.Services.DTO;
using System.Collections.Generic;

namespace api.domain.Interfaces
{
    public interface IProspeccaoRepository : IRepository<Prospeccao>
    {
        IEnumerable<Prospeccao> Listar(ProspeccaoDTO Prospeccao);
        ProspeccaoInteresse VincularInteresse(ProspeccaoInteresse entidade);
        ProspeccaoInteresse PesquisarVinculoInteresse(int id);
        void ExcluirVinculoInteresse(ProspeccaoInteresse pi);
    }
}
