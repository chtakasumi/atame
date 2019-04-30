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

        public IEnumerable<Venda> Listar(VendaDTO vendaDtdo)
        {
            var ListaVendas = base.Query(x =>
                (x.Id == vendaDtdo.Id || !vendaDtdo.Id.HasValue) &&
                ((x.Turma.Inicio >= vendaDtdo.Inicio || !vendaDtdo.Inicio.HasValue) && (x.Turma.Inicio <= vendaDtdo.Fim || !vendaDtdo.Fim.HasValue)) &&
                (x.ClienteFinanceiroId == vendaDtdo.ClienteFinanceiroId || !vendaDtdo.ClienteFinanceiroId.HasValue) &&
                (x.TurmaId == vendaDtdo.TurmaId || !vendaDtdo.TurmaId.HasValue) &&
                (x.VendedorId == vendaDtdo.VendedorId || !vendaDtdo.VendedorId.HasValue), x => x.Id)
                .Include(x => x.Turma)
                .Include(x => x.Vendedor)
                .Include(x => x.ClienteFinanceiro)
                .Include(x => x.Parcelas)
                .Include(x => x.ClientesAcademicos)
                .ThenInclude(x => x.ClienteAcademico);

            //ordenando as parcelas
            foreach (var venda in ListaVendas)
            {
                venda.Parcelas = venda.Parcelas.OrderBy(p => p.Numero).ToList();
            }

            return ListaVendas;
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
