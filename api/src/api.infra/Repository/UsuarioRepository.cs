using api.domain.Entity;
using api.domain.Interfaces;
using api.infra.Data;
using System.Linq;

namespace api.infra.Repository
{
    public class UsuarioRepository : EfRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(DBContext dbbcontexto) : base(dbbcontexto)
        {
          
        }

        public Usuario Autenticar(string login, string senha)
        {
            return base.Pesquisar(x => x.Login == login && x.Senha == senha).First();
        }
    }
}
