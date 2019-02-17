using System;
using System.Collections.Generic;
using System.Text;

namespace api.domain.Entity
{
    public class UF
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public ICollection<Municipio> Municipios { get; set; }


        public UF()
        {
            Municipios = new List<Municipio>();
        }

    }
}
