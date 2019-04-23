using System.Collections.Generic;
using api.domain.Entity;
using api.domain.Interfaces;
using api.domain.Services.DTO;
using api.infra.Data;
using Microsoft.EntityFrameworkCore;

namespace api.infra.Repository
{
    public class ComissaoRepository : EfRepository<Comissao>, IComissaoRepository
    {
        public ComissaoRepository(DBContext dbbcontexto) : base(dbbcontexto)
        {
          
        }
        
        public IEnumerable<Comissao> Listar(ComissaoDTO comissaoDTO)
        {
            return base.Query(x =>
                 (x.Id == comissaoDTO.Id || !comissaoDTO.Id.HasValue) &&
                 (x.FaturamentoId == comissaoDTO.FaturamentoId || !comissaoDTO.FaturamentoId.HasValue) &&
                 (x.Faturamento.Parcela.Numero == comissaoDTO.NumeroParcela || !comissaoDTO.NumeroParcela.HasValue) &&
                 (x.Faturamento.Parcela.Venda.Id == comissaoDTO.VendaId || !comissaoDTO.VendaId.HasValue) &&                 
                 (x.Faturamento.DataPgto >= comissaoDTO.DataFaturamentoIni || !comissaoDTO.DataFaturamentoIni.HasValue) &&
                 (x.Faturamento.DataPgto <= comissaoDTO.DataFaturamentoFim || !comissaoDTO.DataFaturamentoFim.HasValue) &&
                 (x.Status == comissaoDTO.Status || !comissaoDTO.Status.HasValue) &&
                 (x.DataPagamento == comissaoDTO.DataPagamento || !comissaoDTO.DataPagamento.HasValue) &&
                 (EF.Functions.Like(x.Faturamento.Parcela.Venda.Vendedor.Nome, "%" + comissaoDTO.Vendedor + "%") || string.IsNullOrEmpty(comissaoDTO.Vendedor)),
                 x=>x.Id
              ).Include(x=>x.Faturamento)
              .ThenInclude(f=>f.Parcela)
              .ThenInclude(f=>f.Venda)
              .ThenInclude(v=>v.Vendedor);
        }
    }
}
