using System;
using System.Collections.Generic;
using System.Text;

namespace api.domain.Entity
{
    public class EmpresaDTO
    {
        public int? Id { get; set; }
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }      
    }
}
