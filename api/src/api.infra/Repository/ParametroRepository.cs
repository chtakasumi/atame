using System.Collections.Generic;
using api.domain.Entity;
using api.domain.Interfaces;
using api.infra.Data;
using Microsoft.EntityFrameworkCore;

namespace api.infra.Repository
{
    public class ParametroRepository : EfRepository<Parametro>, IParametroRepository
    {
        public ParametroRepository(DBContext dbbcontexto) : base(dbbcontexto)
        {
          
        }
        public IEnumerable<Parametro> Listar(Parametro parametro)
        {
            return base.Pesquisar(x =>
                (x.Id == parametro.Id || !parametro.Id.HasValue) &&
                (EF.Functions.Like(x.Chave, parametro.Chave) || string.IsNullOrEmpty(parametro.Chave))
             );
        }
    }
}
