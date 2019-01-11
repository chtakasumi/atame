using libs;
using System;

namespace api.test
{
    class Program
    {
        static void Main(string[] args)
        {
            TestarLibSeguranca();
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

            Console.ReadKey();
        }
    }
}
