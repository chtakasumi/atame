using api.domain.Entity;
using api.libs;
using System;

namespace api.test
{
    class Program
    {
        static void Main(string[] args)
        {
            TestarJSONCoverter();

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
    }
}
