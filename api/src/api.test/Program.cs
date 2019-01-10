using libs;

using System;

namespace ConsoleTeste
{
    class Program
    {
        static void Main(string[] args)
        {
            string c = Seguranca.Critptografar("123456");
            Console.WriteLine(c);

            string d = Seguranca.Descriptografar(c);
            Console.WriteLine(d);


            string r = Seguranca.GerarHash("123456");
            Console.WriteLine(r);


            Console.ReadKey();
        }
    }
}

