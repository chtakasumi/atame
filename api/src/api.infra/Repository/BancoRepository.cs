using System.Collections.Generic;
using api.domain.Entity;
using api.domain.Interfaces;
using api.infra.Data;
using Microsoft.EntityFrameworkCore;

namespace api.infra.Repository
{
    public class BancoRepository : EfRepository<Banco>, IBancoRepository
    {
        public BancoRepository(DBContext dbbcontexto) : base(dbbcontexto)
        {
          
        }
        public IEnumerable<Banco> Listar(Banco Banco)
        {
            return base.Pesquisar(x =>
                (x.Id == Banco.Id || !Banco.Id.HasValue) &&
                (x.Codigo == Banco.Codigo || string.IsNullOrEmpty(Banco.Codigo)) &&
                (EF.Functions.Like(x.Nome, "%" + Banco.Nome + "%") || string.IsNullOrEmpty(Banco.Nome))
             );
        }
    }
}
