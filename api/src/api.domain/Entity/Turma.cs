using System;
using System.Collections.Generic;

namespace api.domain.Entity
{
    public class Turma
    {
        public int Id { get; set; }
        public string Identificacao { get; set; }
        public int CursoId { get; set; }
        public Curso Curso { get; set; }
        public decimal Preco { get; set; }
        public DateTime? Inicio { get; set; }
        public DateTime? Fim { get; set; }
        public int Parcela { get; set; }
        public decimal ValorParcela { get; set; }

        public ICollection<TurmaConteudoProgramatico> ConteudosProgramaticos { get; set; }
        public ICollection<TurmaDocente> Docentes { get; set; }

        public Turma()
        {           
            Docentes = new List<TurmaDocente>();
            ConteudosProgramaticos = new List<TurmaConteudoProgramatico>();
        }

    }
}
