using System;
using System.Collections.Generic;
using System.Text;

namespace api.domain.Entity
{
    public class Grupo
    {
        public int? Id { get; set; }
        public string Descricao { get; set; }

        public List<Perfil> PermissoesCedidas { get; set; }
        public List<Perfil> PermissoesNaoCedidas { get; set; }

        public ICollection<PerfilGrupo> PerfisGrupos { get; set; }       
        public ICollection<GrupoUsuario> GruposUsuarios { get; set; }


        public Grupo() {
            PerfisGrupos = new List<PerfilGrupo>();
            GruposUsuarios = new List<GrupoUsuario>();
            PermissoesCedidas = new List<Perfil>();
            PermissoesNaoCedidas = new List<Perfil>();

        }

    }    
}
