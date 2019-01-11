using System;
using System.Security.Cryptography;
using System.Text;

namespace libs
{
    public static class Seguranca
    {
        public static string Critptografar(string valor)
        {
            Byte[] cript = ASCIIEncoding.ASCII.GetBytes(valor);
            return Convert.ToBase64String(cript);
        }

        public static string Descriptografar(string valor)
        {
            try
            {
                Byte[] cript = Convert.FromBase64String(valor);
                return ASCIIEncoding.ASCII.GetString(cript);
            }
            catch
            {
                throw new Exception("o valor informado não esta criptografado para ser descriptogravado");
            }           
        }

        public static string GerarHash(string valor)
        {
            byte[] data = Encoding.UTF8.GetBytes(valor);

            SHA1 sha = new SHA1CryptoServiceProvider();
            // This is one implementation of the abstract class SHA1.
            byte[] hash = sha.ComputeHash(data);

            var sb = new StringBuilder(hash.Length * 2);

            foreach (byte b in hash)
            {
                // can be "x2" if you want lowercase
                sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
