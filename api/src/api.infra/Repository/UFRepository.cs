using System.Collections.Generic;
using api.domain.Entity;
using api.domain.Interfaces;
using api.infra.Data;
using Microsoft.EntityFrameworkCore;

namespace api.infra.Repository
{
    public class UFRepository : EfRepository<UF>, IUFRepository
    {
        public UFRepository(DBContext dbbcontexto) : base(dbbcontexto)
        {
          
        }
        public IEnumerable<UF> Listar(UF uf)
        {
            return base.Pesquisar(x =>
                (x.Id == uf.Id || !uf.Id.HasValue) &&
                (EF.Functions.Like(x.Nome, "%" + uf.Nome + "%") || string.IsNullOrEmpty(uf.Nome)) &&
                 (EF.Functions.Like(x.Sigla, uf.Sigla) || string.IsNullOrEmpty(uf.Sigla))
             );
        }
    }
}
