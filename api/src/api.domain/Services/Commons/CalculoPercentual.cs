using System;
using System.Collections.Generic;
using System.Text;

namespace api.domain.Services.Commons
{
    public class CalculoPercentual
    {
        public decimal ValorVendido { get; private set; }
        public decimal ValorNovaComisao { get; private set; }
        public decimal ValorAhReceber { get; private set; }

        public CalculoPercentual(decimal precoCurso, decimal comissaoVendedor, decimal descontoVenda)
        {
            //Quando for lançado a venda automaticamente deve ser inserido uma linha na tabela
            //de comissão seguindo a seguinte regra:
            //1º Através da comissão cadastrado no tipo curso e o valor cadastrado na turma;
            //2º Quantos porcento for dado de desconto no valor do curso será reduzido da comissão do vendedor:
            //Ex.Valor do Curso R$ 1.000,00
            //Comissão 5 %
            //Desconto 20 % R$ 200,00
            //Valor Vendido R$ 800,00
            //Comissão = 5 % -(20 % de 5 %) 1 % = 4 %
            //Total de Comissão: R$ 800,00 * 4 % = R$ 32,00
            //decimal precoCurso = decimal.Parse("1.000,00");
            //decimal comissaoVendedor = (decimal.Parse("5")/100);
            //decimal descontoVenda = (decimal.Parse("20") / 100);

            comissaoVendedor = comissaoVendedor / 100;
            descontoVenda = descontoVenda / 100;

            this.ValorVendido = precoCurso - (descontoVenda * precoCurso);

            this.ValorNovaComisao = comissaoVendedor - (descontoVenda * comissaoVendedor);

            this.ValorAhReceber = this.ValorVendido * this.ValorNovaComisao;

        }

        public override string ToString()
        {
            return string.Format(@"ValorVendido: {0:C} {3}ValorNovaComisao: {1:C} {3}ValorAhReceber: {2:C}", this.ValorVendido, this.ValorNovaComisao, this.ValorAhReceber, Environment.NewLine);
        }
    }
}
