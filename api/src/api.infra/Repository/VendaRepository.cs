using System.Collections.Generic;
using System.Linq;
using api.domain.Entity;
using api.domain.Interfaces;
using api.infra.Data;
using Microsoft.EntityFrameworkCore;

namespace api.infra.Repository
{
    public class VendaRepository : EfRepository<Venda>, IVendaRepository
    {
        public VendaRepository(DBContext dbbcontexto) : base(dbbcontexto)
        {

        }

        public IEnumerable<Venda> Listar(VendaDTO venda)
        {
            var ListaCursos = base.Query(x =>
                (x.Id == venda.Id || !venda.Id.HasValue) &&
                ((x.Turma.Inicio >= venda.Inicio || !venda.Inicio.HasValue) && (x.Turma.Fim <= venda.Fim || !venda.Fim.HasValue)) &&
                (x.ClienteFinanceiroId == venda.ClienteFinanceiroId || !venda.ClienteFinanceiroId.HasValue) &&
                (x.TurmaId == venda.TurmaId || !venda.TurmaId.HasValue) &&
                (x.VendedorId == venda.VendedorId || !venda.VendedorId.HasValue), x => x.Id)
                .Include(x => x.Turma)
                .Include(x => x.Vendedor)
                .Include(x => x.ClienteFinanceiro)
                .Include(x => x.ClientesAcademicos)
                .ThenInclude(x => x.ClienteAcademico);

            return ListaCursos.ToList();
        }

        public void ExcluirVinculoAcademico(VendaCliente vc)
        {
            base._dbContexto.Remove(vc);
            base._dbContexto.SaveChanges();
        }     

        public VendaCliente PesquisarVinculoAcademico(int id)
        {
            return _dbContexto.Set<VendaCliente>().Find(id);
        }

        public VendaCliente VincularAcademico(VendaCliente entidade)
        {
            _dbContexto.Set<VendaCliente>().Add(entidade);
            _dbContexto.SaveChanges();
            return entidade;
        }
    }
}
