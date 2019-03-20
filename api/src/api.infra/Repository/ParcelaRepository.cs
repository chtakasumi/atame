using System.Collections.Generic;
using api.domain.Entity;
using api.domain.Interfaces;
using api.domain.Services.DTO;
using api.infra.Data;

namespace api.infra.Repository
{
    public class ParcelaRepository : EfRepository<Parcela>, IParcelaRepository
    {
        public ParcelaRepository(DBContext dbbcontexto) : base(dbbcontexto)
        {
          
        }

        public IEnumerable<Parcela> Listar(ParcelaDTO Parcela)
        {
            return base.Pesquisar(x =>
                (x.Id == Parcela.Id || !Parcela.Id.HasValue) &&
                (x.Numero == Parcela.Numero || !Parcela.Numero.HasValue) &&
                (x.VendaId == Parcela.VendaId || !Parcela.VendaId.HasValue));            
        }
    }
}
