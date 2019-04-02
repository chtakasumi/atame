using api.domain.Entity;
using api.domain.Services.DTO;
using System.Collections.Generic;

namespace api.domain.Interfaces
{
    public interface IFaturamentoRepository : IRepository<Faturamento>
    {
        IEnumerable<Faturamento> Listar(FaturamentoDTO faturamento);

        IEnumerable<Parcela> ListarParcelas(FaturamentoDTO faturamento);
       
    }
}
