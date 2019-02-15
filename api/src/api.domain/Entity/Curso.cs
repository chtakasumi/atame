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
        public decimal Preco { get; set; }

        public ICollection<CursoConteudoProgramatico> ConteudosProgramaticos { get; set; }
        public ICollection<CursoDocente> Docentes { get; set; }

        public Curso() {
            //TipoCurso = new TipoCurso(); isso para validar o tipo curso na cadastro
            Docentes = new List<CursoDocente>();
            ConteudosProgramaticos = new List<CursoConteudoProgramatico>();
        }

    }
}
