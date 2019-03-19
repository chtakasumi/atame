using api.domain.Services.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace api.domain.Entity
{
    public class Parametro
    {
        public int? Id { get; set; }
        public string Chave {get; set; }
        public string valor { get; set; }
        public string Descricao { get; set; }
    }
}
