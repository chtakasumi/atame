using api.domain.Entity;
using api.domain.Services.DTO;
using System.Collections.Generic;

namespace api.domain.Interfaces
{
    public interface IParcelaRepository : IRepository<Parcela>
    {
        IEnumerable<Parcela> Listar(ParcelaDTO Parcela);
    }
}
