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
        public string Observacao { get; set; }

        public string ClienteNome { get; set; }
        public string ClienteEmail { get; set; }
        public string ClienteTelefone { get; set; }
        public string ClienteCelular { get; set; }
        public EnumEstatusProspeccao Status { get; set; }


        public ICollection<ProspeccaoInteresse> Interesses { get; set; }

        public Prospeccao()
        {
            Interesses = new List<ProspeccaoInteresse>();
        }        
    }

    public enum EnumEstatusProspeccao
    {
         EmAnalise,
         Aprovado,
         SemInteresse,
         FuturoContato
    }
}
