using api.domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace api.domain.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Usuario Autenticar(string login, string senha);
    }
}
