using System;
using System.Collections.Generic;
using System.Text;

namespace api.domain.Services.Commons
{
    public class CalculoPercentual
    {
       
        public decimal ValorNovaComisao { get; private set; }
        public decimal ValorAhReceber { get; private set; }
      
        public CalculoPercentual(decimal valorVendido, decimal comissaoVendedor, decimal descontoVenda, bool gerente=false)
        {           
            var divisor = 100;                       
            descontoVenda = descontoVenda / divisor;

            if (gerente)
            {
                this.ValorNovaComisao = comissaoVendedor;
            }
            else {
                this.ValorNovaComisao = comissaoVendedor - (descontoVenda * comissaoVendedor);
            }
            

            this.ValorAhReceber = valorVendido * (this.ValorNovaComisao/ divisor); 

        }

        public override string ToString()
        {
            return string.Format(@"ValorNovaComissaoVendedor: {0:C} {2}ValorAhReceberVendedor: {1:C}",                             
                                    this.ValorNovaComisao,
                                    this.ValorAhReceber, Environment.NewLine);
        }
    }
}
