using System.Collections.Generic;
using api.domain.Entity;
using api.domain.Interfaces;
using api.infra.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace api.infra.Repository
{
    public class ClienteRepository : EfRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(DBContext dbbcontexto) : base(dbbcontexto)
        {

        }
        public IEnumerable<Cliente> Listar(ClienteDTO cliente)
        {
            return base.Query(x => (x.Id == cliente.Id || !cliente.Id.HasValue) &&
                   (EF.Functions.Like(x.Nome, "%" + cliente.Nome + "%") || string.IsNullOrEmpty(cliente.Nome)) &&
                   (EF.Functions.Like(x.CpfCnpj, cliente.CpfCnpj) || string.IsNullOrEmpty(cliente.CpfCnpj)) &&
                   (EF.Functions.Like(x.NomeCpfCnpj, cliente.NomeCpfCnpj + "%") || string.IsNullOrEmpty(cliente.NomeCpfCnpj)),
                   x=>x.Id).Include(c => c.UF).Include(c => c.Municipio);


        }
    }
}
