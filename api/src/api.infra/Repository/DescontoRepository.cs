using System.Collections.Generic;
using api.domain.Entity;
using api.domain.Interfaces;
using api.infra.Data;
using Microsoft.EntityFrameworkCore;

namespace api.infra.Repository
{
    public class DescontoRepository : EfRepository<Desconto>, IDescontoRepository
    {
        public DescontoRepository(DBContext dbbcontexto) : base(dbbcontexto)
        {   
            
        }
        public IEnumerable<Desconto> Listar(Desconto desconto)
        {
            return base.Pesquisar(x =>(x.Id == desconto.Id || !desconto.Id.HasValue) &&
                (EF.Functions.Like(x.Descricao, "%" + desconto.Descricao + "%") || string.IsNullOrEmpty(desconto.Descricao)) &&
                (x.Percentual == desconto.Percentual || desconto.Percentual== 0 || !desconto.Percentual.HasValue));
        }
    }
}
