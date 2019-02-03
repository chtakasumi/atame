using System;
using System.Collections.Generic;
using System.Text;

namespace api.domain.Entity
{
    public class Curso
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int TipoCursoId { get; set; }
        public TipoCurso TipoCurso { get; set; }
    }
}
