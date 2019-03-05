using api.domain.Entity;
using System.Collections.Generic;

namespace api.domain.Interfaces
{
    public interface IVendaRepository : IRepository<Venda>
    {
        IEnumerable<Venda> Listar(VendaDTO venda);
        VendaCliente VincularAcademico(VendaCliente entidade);
        VendaCliente PesquisarVinculoAcademico(int id);
        void ExcluirVinculoAcademico(VendaCliente vc);
    }
}
