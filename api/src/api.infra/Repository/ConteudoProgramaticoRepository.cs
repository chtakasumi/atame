using System.Collections.Generic;
using api.domain.Entity;
using api.domain.Interfaces;
using api.infra.Data;
using Microsoft.EntityFrameworkCore;

namespace api.infra.Repository
{
    public class ConteudoProgramaticoRepository : EfRepository<ConteudoProgramatico>, IConteudoProgramaticoRepository
    {
        public ConteudoProgramaticoRepository(DBContext dbbcontexto) : base(dbbcontexto)
        {
          
        }
        public IEnumerable<ConteudoProgramatico> Listar(ConteudoProgramatico conteudoProgramatico)
        {
            return base.Pesquisar(x =>
                (x.Id == conteudoProgramatico.Id || !conteudoProgramatico.Id.HasValue) &&
                (EF.Functions.Like(x.Identificacao, "%" + conteudoProgramatico.Identificacao + "%") 
                || string.IsNullOrEmpty(conteudoProgramatico.Identificacao))
             );
        }
    }
}
