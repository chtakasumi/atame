using System;
using System.Collections.Generic;
using System.Text;

namespace api.domain.Entity
{   
    public class GrupoUsuario
    {
        public int Id { get; set; }


        public int GrupoId { get; set; }
        public Grupo Grupo { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

    }
}
