using System;
using System.Collections.Generic;
using System.Text;

namespace api.domain.Entity
{
    public class TurmaDocente
    {

        public int Id { get; set; }

        public int TurmaId { get; set; }
        public Turma Turma { get; set; }

        public int DocenteId { get; set; }
        public Docente Docente { get; set; }
    }
}
