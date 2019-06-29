using System.Collections.Generic;
using System.Data.Common;
using api.domain.Entity;
using api.infra.Data;
using Microsoft.EntityFrameworkCore;

namespace api.infra.Repository
{
    public class GeradorRelatorioRepository : EfRepository<GeradorRelatorio>, IGeradorRelatorioRepository
    {
        public GeradorRelatorioRepository(DBContext dbcontexto) : base(dbcontexto)
        {
          
        }

        public DbDataReader ExecutarQuery(string query)
        {          
            using (var command = base._dbContexto.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                base._dbContexto.Database.OpenConnection();
                return command.ExecuteReader();
            }
          
        }

        public IEnumerable<GeradorRelatorio> Listar(GeradorRelatorioDTO dto)
        {
            return base.Pesquisar(x =>
                   (x.Id == dto.Id || !dto.Id.HasValue) &&
                   (EF.Functions.Like(x.Nome, "%" + dto.Nome + "%") || string.IsNullOrEmpty(dto.Nome)) &&
                   (EF.Functions.Like(x.Descricao, "%" + dto.Descricao + "%") || string.IsNullOrEmpty(dto.Descricao))
                );
        }
    }
}
