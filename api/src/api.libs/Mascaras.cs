using System;
using System.Collections.Generic;
using System.Text;

namespace api.libs
{
    public static class Mascaras
    {
        public static string ToTelefone(string phone)
        {
            if (string.IsNullOrEmpty(phone))
            {
                return "****";
            }
            if (phone.Length == 10)
            {
                return long.Parse(phone).ToString(@"(00) 0000-0000");
            }
            return long.Parse(phone).ToString(@"(00) 00000-0000");
        }

        public static string ToMoeda(decimal preco)
        {
            return string.Format("{0:C}", preco);
        }

        public static string ToEmail(string valor)
        {
            if (string.IsNullOrEmpty(valor))
            {
                return "****";
            }
            return valor.ToLower();
        }

        public static string ToUpper(string valor)
        {
            if (string.IsNullOrEmpty(valor))
            {
                return "****";
            }
            return valor.ToLower();           
        }
    }
}
