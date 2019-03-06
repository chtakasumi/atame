using System;
using System.Collections.Generic;


namespace api.domain.Entity
{
    public class Prospeccao
    {
        public int Id { get; set; }
        public DateTime? Data { get; set; }
        public int VendedorId { get; set; }
        public Vendedor Vendedor { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public string Observacao { get; set; }
        public ICollection<ProspeccaoInteresse> Interesses { get; set; }

        public Prospeccao()
        {
            Interesses = new List<ProspeccaoInteresse>();
        }        
    }
}
