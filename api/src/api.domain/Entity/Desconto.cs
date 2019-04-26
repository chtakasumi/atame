using System;
using System.Collections.Generic;
using System.Text;

namespace api.domain.Entity
{
    public class Desconto
    {
        public int? Id { get; set; }
        public string Descricao { get; set; }
        public decimal? Percentual { get; set; }
    }
}
