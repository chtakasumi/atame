using System;
using System.Collections.Generic;
using System.Text;

namespace api.domain.Services.DTO
{
    public class UsuarioDTO
    {
        public int? Id { get; set; }
        public int? VendedorId { get; set; }       
        public string Login { get; set; }      
        public string Ativo { get; set; }      
    }
}
