using System;
using System.Collections.Generic;
using System.Text;

namespace api.domain.Entity
{
    public class Grupo
    {
        public int Id { get; set; }
        public string Descricao { get; set; }       
        public ICollection<PerfilGrupo> PerfisGrupos { get; set; }
        public ICollection<GrupoUsuario> GruposUsuarios { get; set; }
        
    }    
}
