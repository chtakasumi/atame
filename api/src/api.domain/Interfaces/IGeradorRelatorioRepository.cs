using api.domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace api.domain.Entity
{
    public interface IGeradorRelatorioRepository : IRepository<GeradorRelatorio>
    {
        IEnumerable<GeradorRelatorio> Listar(GeradorRelatorioDTO dto);
        DbDataReader ExecutarQuery(string query);
    }
}
