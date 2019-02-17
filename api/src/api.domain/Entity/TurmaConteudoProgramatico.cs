using System;
using System.Collections.Generic;
using System.Text;

namespace api.domain.Entity
{
    public class TurmaConteudoProgramatico
    {

        public int Id { get; set; }

        public int TurmaId { get; set; }
        public Turma Turma { get; set; }

        public int ConteudoProgramaticoId { get; set; }
        public ConteudoProgramatico ConteudoProgramatico { get; set; }
    }
}
