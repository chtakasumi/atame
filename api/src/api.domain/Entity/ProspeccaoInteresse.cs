using System;
using System.Collections.Generic;

namespace api.domain.Entity
{
    public class ProspeccaoInteresse
    {
        public int Id { get; set; }

        public int ProspeccaoId { get; set; }
        public Prospeccao Prospeccao { get; set; }

        public int CursoId { get; set; }
        public Curso Curso { get; set; }
    }
}
