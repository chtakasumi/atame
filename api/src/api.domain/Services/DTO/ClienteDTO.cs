using System;
using System.Collections.Generic;
using System.Text;

namespace api.domain.Entity
{
    public class ClienteDTO
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public string CpfCnpj { get; set; }
        public string NomeCpfCnpj { get; set; }
    }
}
