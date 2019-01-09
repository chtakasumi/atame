using System;
using System.Collections.Generic;
using System.Text;

namespace api.domain.Entity
{
    public class PerfilGrupo
    {
        public int Id { get; set; }

        public int PerfilId { get; set; }
        public Perfil Perfil { get; set; }

        public int GrupoId { get; set; }
        public Grupo Grupo { get; set; }
    }
}
