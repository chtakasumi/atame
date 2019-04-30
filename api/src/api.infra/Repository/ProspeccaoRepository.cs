using System.Collections.Generic;
using System.Linq;
using api.domain.Entity;
using api.domain.Interfaces;
using api.domain.Services.DTO;
using api.infra.Data;
using Microsoft.EntityFrameworkCore;

namespace api.infra.Repository
{
    public class ProspeccaoRepository : EfRepository<Prospeccao>, IProspeccaoRepository
    {
        public ProspeccaoRepository(DBContext dbbcontexto) : base(dbbcontexto)
        {

        }

        public IEnumerable<Prospeccao> Listar(ProspeccaoDTO Prospeccao)
        {
            var ListaCursos = base.Query(x =>
                (x.Id == Prospeccao.Id || !Prospeccao.Id.HasValue) &&
                ((x.Data >= Prospeccao.Inicio || !Prospeccao.Inicio.HasValue) && (x.Data <= Prospeccao.Fim || !Prospeccao.Fim.HasValue)) &&
                (x.VendedorId == Prospeccao.VendedorId || !Prospeccao.VendedorId.HasValue) &&
                (EF.Functions.Like(x.ClienteNome, "%" + Prospeccao.ClienteNome + "%") || string.IsNullOrEmpty(Prospeccao.ClienteNome)), x => x.Id)             
                .Include(x => x.Vendedor)              
                .Include(x => x.Interesses)
                .ThenInclude(x => x.Curso);

            return ListaCursos.ToList();
        }
              
                       
        public ProspeccaoInteresse VincularInteresse(ProspeccaoInteresse entidade)
        {
            _dbContexto.Set<ProspeccaoInteresse>().Add(entidade);
            _dbContexto.SaveChanges();
            return entidade;
        }

        public ProspeccaoInteresse PesquisarVinculoInteresse(int id)
        {
            return _dbContexto.Set<ProspeccaoInteresse>().Find(id);
        }

        public void ExcluirVinculoInteresse(ProspeccaoInteresse pi)
        {
            base._dbContexto.Remove(pi);
            base._dbContexto.SaveChanges();
        }
    }
}
