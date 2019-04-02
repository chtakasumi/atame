using System;

namespace api.domain.Entity
{
    public class Parcela
    {

        public int Id { get; set; }

        public int VendaId { get; set; }
        public Venda Venda { get; set; }
        public decimal Preco { get; set; }
        public DateTime? PrevisaoPgto { get; set; }
        public int Numero{ get; set; }
        public EnumStatusPgto Status { get; set; }

        public int? FaturamentoId { get; set; }
        public Faturamento Faturamento{ get; set; }

}
}
