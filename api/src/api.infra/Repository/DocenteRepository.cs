using System.Collections.Generic;
using api.domain.Entity;
using api.domain.Interfaces;
using api.infra.Data;
using Microsoft.EntityFrameworkCore;

namespace api.infra.Repository
{
    public class DocenteRepository : EfRepository<Docente>, IDocenteRepository
    {
        public DocenteRepository(DBContext dbbcontexto) : base(dbbcontexto)
        {
          
        }
        public IEnumerable<Docente> Listar(Docente docente)
        {
            return base.Pesquisar(x =>
                (x.Id == docente.Id || !docente.Id.HasValue) &&
                (EF.Functions.Like(x.Nome, "%" + docente.Nome + "%") || string.IsNullOrEmpty(docente.Nome))&&              
                (EF.Functions.Like(x.Formacao, "%" + docente.Formacao + "%") || string.IsNullOrEmpty(docente.Formacao))
             );
        }
    }
}
