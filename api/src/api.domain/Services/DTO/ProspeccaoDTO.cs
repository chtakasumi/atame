using System;

namespace api.domain.Services.DTO
{
    public class ProspeccaoDTO
    {
        public int? Id { get; set; }
        public DateTime? Inicio { get; set; }
        public DateTime? Fim { get; set; }
        public int? VendedorId { get; set; }       
        public int? ClienteId { get; set; }
        public string ClienteNome { get; set; }
    }
}
