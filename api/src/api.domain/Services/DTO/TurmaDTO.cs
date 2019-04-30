using System;


namespace api.domain.Services.DTO
{
    public class TurmaDTO
    {
        public int? Id { get; set; }
        public string Identificacao { get; set; }
        public int? CursoId { get; set; }
        public DateTime? Inicio { get; set; }
        public DateTime? Fim { get; set; }   
        public string CursoTuma { get; set; }
    }
}
