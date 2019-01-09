using System;
using System.Collections.Generic;
using System.Text;

namespace api.domain.Entity
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Ativo { get; set; }
        public ICollection<GrupoUsuario> GruposUsuarios { get; set; }

    }
}
