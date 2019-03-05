using System;
using System.Collections.Generic;


namespace api.domain.Entity
{
    public class Venda
    {
        public int Id { get; set; }
        public DateTime? Data { get; set; }

        public int ClienteFinanceiroId { get; set; }
        public Cliente ClienteFinanceiro { get; set; }

        public int TurmaId { get; set; }
        public Turma Turma { get; set; }

        public int VendedorId { get; set; }
        public Vendedor Vendedor { get; set; }

        public int Quantidade { get; set; }

        public decimal ValorCurso { get; set; }

        public decimal Desconto { get; set; }

        public decimal ValorVenda { get; set; }

        public ICollection<VendaCliente> ClientesAcademicos { get; set; }

        public Venda()
        {
            ClientesAcademicos = new List<VendaCliente>();
        }

        public void CalcularValorVenda()
        {
            decimal valorTotal = (this.ValorCurso * this.Quantidade);
            decimal desconto =((this.Desconto * valorTotal) / 100);
            this.ValorVenda = valorTotal - desconto;
        }
    }
}
