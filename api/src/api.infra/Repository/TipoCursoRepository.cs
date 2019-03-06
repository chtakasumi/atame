using System.Collections.Generic;
using api.domain.Entity;
using api.domain.Interfaces;
using api.infra.Data;
using Microsoft.EntityFrameworkCore;

namespace api.infra.Repository
{
    public class TipoCursoRepository : EfRepository<TipoCurso>, ITipoCursoRepository
    {
        public TipoCursoRepository(DBContext dbbcontexto) : base(dbbcontexto)
        {          
        }

        public IEnumerable<TipoCurso> Listar(TipoCurso tipoCurso)
        {
            return base.Pesquisar(x =>
                (x.Id == tipoCurso.Id || !tipoCurso.Id.HasValue) &&
                (EF.Functions.Like(x.Descricao, "%" + tipoCurso.Descricao + "%") || string.IsNullOrEmpty(tipoCurso.Descricao))
             );
        }
    }
}
