using api.domain.Entity;
using System;

namespace api.domain.Services.DTO
{
    public class FaturamentoDTO
    {
        //public int? Id { get; set; }       
        //public DateTime? DataPgto { get; set; }
        //public int? CompetenciaAno { get; set; }
        //public int? CompetenciaMes { get; set; }   
       
         //vendas   
        public int? VendaId { get; set; }
        public int? ClienteFinanceiroId { get; set; }
        public int? VendedorId { get; set; }
        public DateTime? DataVenda { get; set; }

        //parcelas
        public int? ParcelaId { get; set; }
        public int? Numero { get; set; }
        public EnumStatusPgto? Status { get; set; }
        public DateTime? Vencimento { get; set; }

    }
}
