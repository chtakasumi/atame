using System;
using System.Collections.Generic;
using System.Text;

namespace api.domain.Entity
{
    public class GeradorRelatorio
    {
        public int Id { get; set; }       
        public string Nome { get; set; }
        public string Alias { get; set; }
        public string Descricao { get; set; }
        public string Query { get; set; }       
    }
}
