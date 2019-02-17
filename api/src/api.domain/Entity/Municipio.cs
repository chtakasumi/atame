using System;
using System.Collections.Generic;
using System.Text;

namespace api.domain.Entity
{
    public class Municipio
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public string Codigo { get; set; }
        public int? UFId { get; set; }
        public UF UF { get; set; }

    }
}
