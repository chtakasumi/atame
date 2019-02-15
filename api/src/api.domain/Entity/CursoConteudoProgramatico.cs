using System;
using System.Collections.Generic;
using System.Text;

namespace api.domain.Entity
{
    public class CursoConteudoProgramatico
    {
        public int Id { get; set; }

        public int CursoId { get; set; }
        public Curso Curso { get; set; }

        public int ConteudoProgramaticoId { get; set; }
        public ConteudoProgramatico ConteudoProgramatico { get; set; }
    }
}
