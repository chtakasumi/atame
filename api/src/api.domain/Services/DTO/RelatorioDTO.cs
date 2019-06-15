using System;
using System.Collections.Generic;
using System.Text;

namespace api.domain.Services.DTO
{
    public class RelatorioDTO
    {
        public int RelatorioId { get; }
        public int UsuId { get; }

        public RelatorioDTO(int relatorioId,int usuId)
        {
            RelatorioId = relatorioId;
            UsuId = usuId;
        }
      
    }
}
