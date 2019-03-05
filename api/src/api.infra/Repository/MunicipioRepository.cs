using System.Collections.Generic;
using api.domain.Entity;
using api.domain.Interfaces;
using api.infra.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace api.infra.Repository
{
    public class MunicipioRepository : EfRepository<Municipio>, IMunicipioRepository
    {
        public MunicipioRepository(DBContext dbbcontexto) : base(dbbcontexto)
        {
          
        }
        public IEnumerable<Municipio> Listar(Municipio municipio)
        {
            return base.Query(x =>
                (x.Id == municipio.Id || !municipio.Id.HasValue) &&
                (EF.Functions.Like(x.Nome, "%" + municipio.Nome + "%") || string.IsNullOrEmpty(municipio.Nome)) &&
                (EF.Functions.Like(x.Codigo, municipio.Codigo) || string.IsNullOrEmpty(municipio.Codigo)) &&
                (x.UFId == municipio.UFId || !municipio.UFId.HasValue), x=>x.Id
             ).Include(m=>m.UF);
        }
    }
}
