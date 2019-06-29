using System;
using System.Collections.Generic;
using System.Text;

namespace api.domain.Services.DTO
{
    public class RelatorioDTO
    {
        public static string FORMATO_PDF = "pdf";
        public static string FORMATO_CSV = "csv";
        public int RelatorioId { get; }
        public int UsuId { get; }
        public string Formato { get; }

        public RelatorioDTO(int relatorioId, int usuId, string formato = "pdf")
        {
            RelatorioId = relatorioId;
            UsuId = usuId;
            Formato = formato;
        }
    }

   

}
