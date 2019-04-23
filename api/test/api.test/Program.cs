using api.domain.Entity;
using api.domain.Services.Commons;
using api.libs;
using System;

namespace api.test
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestarLibSeguranca();
            //TestarJSONCoverter();
            TestarCalculoPercentualCommissao();

            Console.ReadKey();
        }

        static void TestarLibSeguranca()
        {
            var senha = "123456";

            var c = Seguranca.Critptografar(senha);
            Console.WriteLine(c);

            var d = Seguranca.Descriptografar(c);
            Console.WriteLine(d);


            var h = Seguranca.GerarHash(senha);
            Console.WriteLine(h);
           
        }

        static void TestarJSONCoverter()
        {
            string json= Json.ToJson(new Curso());
            Console.WriteLine(json);

            Curso o = Json.ToObject<Curso>(json);
            Console.WriteLine(o);
        }

        static void TestarCalculoPercentualCommissao()
        {   
            var valorRecebido = Convert.ToDecimal("800,00");
            var comissaoVendedor = Convert.ToDecimal("5,000");
            var descontoVenda = Convert.ToDecimal("20,00");

            var calc = new CalculoPercentual(valorRecebido, comissaoVendedor, descontoVenda);

            Console.WriteLine(calc.ToString());
        }



    }
}
