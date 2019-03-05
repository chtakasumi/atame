using System;
using System.Collections.Generic;

namespace api.domain.Entity
{
    public class VendaCliente
    {
        public int Id { get; set; }
      
        public int ClienteAcademicoId { get; set; }
        public Cliente ClienteAcademico { get; set; }

        public int VendaId { get; set; }
        public Venda Venda { get; set; }

      

      

    }
}
