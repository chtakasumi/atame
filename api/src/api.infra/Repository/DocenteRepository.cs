using System.Collections.Generic;
using api.domain.Entity;
using api.domain.Interfaces;
using api.domain.Services.DTO;
using api.infra.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace api.infra.Repository
{
    public class DocenteRepository : EfRepository<Docente>, IDocenteRepository
    {
        public DocenteRepository(DBContext dbbcontexto) : base(dbbcontexto)
        {
          
        }
        public IEnumerable<Docente> Listar(DocenteDTO docente)
        {
            var query= base.Query(x =>
                (x.Id == docente.Id || !docente.Id.HasValue) &&
                (EF.Functions.Like(x.Nome, "%" + docente.Nome + "%") || string.IsNullOrEmpty(docente.Nome))&&              
                (EF.Functions.Like(x.Formacao, "%" + docente.Formacao + "%") || string.IsNullOrEmpty(docente.Formacao)),x=>x.Id)
                .Include(x=>x.UfExpedicao)
                .Include(x=>x.Banco);

            foreach(var x in query)
            {
                x.TipoConta = new TipoConta().GetAll().Where(c => c.Descricao == x.TipoContaDescricao).SingleOrDefault(); 
                x.orgaoExpedicao = new OrgaoExpeditor().GetAll().Where(c=>c.SiglaDescricao== x.OrgaoExpedicaoSiglaDescricao).SingleOrDefault();
            };

         

            return query;
        }
    }
}
