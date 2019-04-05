using System.Collections.Generic;
using api.domain.Entity;
using api.domain.Interfaces;
using api.domain.Services.DTO;
using api.infra.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace api.infra.Repository
{
    public class FaturamentoRepository : EfRepository<Faturamento>, IFaturamentoRepository
    {
        public FaturamentoRepository(DBContext dbbcontexto) : base(dbbcontexto)
        {

        }

        public IEnumerable<Faturamento> Listar(FaturamentoDTO faturamento)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Parcela> ListarParcelas(FaturamentoDTO faturamento)
        {
            var parcelas = this._dbContexto.Parcelas.Where(x =>
                  (x.Id == faturamento.Id || !faturamento.Id.HasValue) &&
                  (x.Numero == faturamento.Numero || !faturamento.Numero.HasValue) &&
                  (x.Status == faturamento.Status || !faturamento.Status.HasValue) &&
                  (x.PrevisaoPgto == faturamento.Vencimento || !faturamento.Vencimento.HasValue) &&
                  //vendas
                  (x.Venda.Id == faturamento.VendaId || !faturamento.VendaId.HasValue) &&
                   (x.Venda.ClienteFinanceiro.CpfCnpj == faturamento.CpfCnpj || string.IsNullOrEmpty(faturamento.CpfCnpj)) &&
                  (x.Venda.ClienteFinanceiroId == faturamento.ClienteFinanceiroId || !faturamento.ClienteFinanceiroId.HasValue) &&
                  (x.Venda.VendedorId == faturamento.VendedorId || !faturamento.VendedorId.HasValue) &&
                  (x.Venda.VendedorId == faturamento.VendedorId || !faturamento.VendedorId.HasValue)).Include(x => x.Faturamento)
                  .Include(p => p.Venda)
                  .ThenInclude(v => v.Vendedor)
                  .Include(p=>p.Venda).ThenInclude(v=>v.ClienteFinanceiro);


            return parcelas.OrderBy(p => p.Numero);

        }
    } 
}
