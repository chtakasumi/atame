using System;

namespace api.domain.Entity
{
    //Ao baixar um faturamento Gera um registro aqui na comissão apenas os 5 primeiros campos mais o status como EmAberto
    //Esse registro gerado pela o gestor controlar as comissoes pagas
    //Valor Pago e ele preenche quando pagar as comissões

    public class Comissao
    {
        public int Id { get; set; }
        public int FaturamentoId { get; set; }
        public Faturamento Faturamento { get; set; }       
        public decimal Percentual { get; set; } //é a porcetagem que o vendedor tem a receber sobre o valor faturado
        public decimal ValorApagar { get; set; } //previsto
        public EnumStatusComissao Status { get; set; }
        public bool Gerente { get; set; }

        public decimal? ValorPago { get; set; }
        public string Observacao { get; set; }     
        public DateTime? DataPagamento { get; set; }
    }

    public enum EnumStatusComissao
    {
        EmAberto,
        Pago,
        ParcialmentePago
    }
   
}
