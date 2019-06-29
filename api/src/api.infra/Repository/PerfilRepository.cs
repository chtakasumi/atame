using System.Collections.Generic;
using System.Linq;
using api.domain.Entity;
using api.domain.Interfaces;
using api.infra.Data;
using Microsoft.EntityFrameworkCore;


namespace api.infra.Repository
{
    public class PerfilRepository : EfRepository<Perfil>, IPerfilRepository
    {
        public PerfilRepository(DBContext dbbcontexto) : base(dbbcontexto)
        {

        }

    }
}
