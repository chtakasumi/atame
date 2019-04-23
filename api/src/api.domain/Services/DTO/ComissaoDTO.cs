using api.domain.Entity;
using System;

namespace api.domain.Services.DTO
{
    public class ComissaoDTO
    {
        public int? Id { get; set; }
        public int? FaturamentoId { get; set; }
        public string Vendedor { get; set; }
        public int? NumeroParcela { get; set; }
        public int? VendaId { get; set; }
        public DateTime? DataFaturamentoIni { get; set; }
        public DateTime? DataFaturamentoFim { get; set; }
        public EnumStatusComissao? Status { get; set; }
        public DateTime? DataPagamento { get; set; }
    }
}
