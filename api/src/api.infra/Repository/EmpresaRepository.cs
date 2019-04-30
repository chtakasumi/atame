using System.Collections.Generic;
using api.domain.Entity;
using api.domain.Interfaces;
using api.infra.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace api.infra.Repository
{
    public class EmpresaRepository : EfRepository<Empresa>, IEmpresaRepository
    {
        public EmpresaRepository(DBContext dbbcontexto) : base(dbbcontexto)
        {

        }
        public IEnumerable<Empresa> Listar(EmpresaDTO Empresa)
        {
            return base.Query(x => (x.Id == Empresa.Id || !Empresa.Id.HasValue) &&                  
                   (EF.Functions.Like(x.RazaoSocial, "%" + Empresa.RazaoSocial + "%") || string.IsNullOrEmpty(Empresa.RazaoSocial)) &&
                   (x.Cnpj==Empresa.Cnpj || string.IsNullOrEmpty(Empresa.Cnpj)), x=>x.Id)
                   .Include(c => c.UF).Include(c => c.Municipio).ToList();
        }
    }
}
