using System;

namespace api.domain.Entity
{
    public class VendaDTO
    {
        public int? Id { get; set; }
        public DateTime? Inicio { get; set; }
        public DateTime? Fim { get; set; }
        public int? VendedorId { get; set; }
        public int? ClienteFinanceiroId { get; set; }       
        public int? TurmaId { get; set; }
    }
}
