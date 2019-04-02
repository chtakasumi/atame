using System;

namespace api.domain.Entity
{
    public class Faturamento
    {
        public int Id { get; set; }
        public int CompetenciaAno { get; set; }
        public int CompetenciaMes { get; set; }
        public virtual string Competencia
        {
            get
            {
                return this.CompetenciaMes.ToString()+"/"+CompetenciaAno.ToString();
            }          
        }
        public int VendaId { get; set; }
        public Venda Venda { get; set; }
        public int ParcelaId { get; set; }
        public Parcela Parcela{ get; set; }
        public decimal ValorPago { get; set; }
        public DateTime? DataPgto { get; set; }

    }
}
