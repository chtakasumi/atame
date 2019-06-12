using System.Collections.Generic;
using api.domain.Entity;
using api.domain.Interfaces;
using api.infra.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace api.infra.Repository
{
    public class GrupoRepository : EfRepository<Grupo>, IGrupoRepository
    {
        public GrupoRepository(DBContext dbbcontexto) : base(dbbcontexto)
        {

        }

        public IEnumerable<Grupo> Listar(Grupo Grupo)
        {
            var query = base.Query(x =>
                 (x.Id == Grupo.Id || !Grupo.Id.HasValue) &&
                 (EF.Functions.Like(x.Descricao, "%" + Grupo.Descricao + "%") ||
                 string.IsNullOrEmpty(Grupo.Descricao)), x => x.Descricao
             ).Include(x => x.PerfisGrupos).ThenInclude(x => x.Perfil);

            foreach (var grupo in query)
            {
                grupo.PermissoesCedidas = grupo.PerfisGrupos.Select(x => x.Perfil).ToList();


                //pegar todos os perfil que não contiver grupo.PermissoesCedidas
                foreach (var pn in base._dbContexto.Perfis)
                {
                    if (!grupo.PermissoesCedidas.Exists(x => x.Id == pn.Id))
                    {
                        grupo.PermissoesNaoCedidas.Add(pn);
                    }

                }
            }
            
            return query;

        }

        public void RemoverPerfilGrupos(int  grupoId)
        {
            var perfilGrupo = _dbContexto.Set<PerfilGrupo>().Where(x => x.GrupoId == grupoId).ToList();
            if (perfilGrupo.Count > 0)
            {
                _dbContexto.Set<PerfilGrupo>().RemoveRange(perfilGrupo);
            }
            _dbContexto.SaveChanges();
        }

        public List<Perfil> ListarPerfil()
        {
            return base._dbContexto.Perfis.ToList();
        }
    }
}
