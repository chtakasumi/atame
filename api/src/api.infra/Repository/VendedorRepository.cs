using System.Collections.Generic;
using api.domain.Entity;
using api.domain.Interfaces;
using api.infra.Data;
using Microsoft.EntityFrameworkCore;

namespace api.infra.Repository
{
    public class VendedorRepository : EfRepository<Vendedor>, IVendedorRepository
    {
        public VendedorRepository(DBContext dbbcontexto) : base(dbbcontexto)
        {
          
        }
        public IEnumerable<Vendedor> Listar(Vendedor Vendedor)
        {
            return base.Pesquisar(x =>
                (x.Id == Vendedor.Id || !Vendedor.Id.HasValue) &&
                (EF.Functions.Like(x.Nome, "%" + Vendedor.Nome + "%") || string.IsNullOrEmpty(Vendedor.Nome))
             );
        }
    }
}
