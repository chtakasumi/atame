using api.domain.Entity;
using api.domain.Services.DTO;
using System.Collections.Generic;

namespace api.domain.Interfaces
{
    public interface IComissaoRepository : IRepository<Comissao>
    {
        IEnumerable<Comissao> Listar(ComissaoDTO Comissao);
    }
}
