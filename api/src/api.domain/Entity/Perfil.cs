using System.Collections.Generic;

namespace api.domain.Entity
{
    public class Perfil
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public ICollection<PerfilGrupo> PerfisGrupos { get; set; }
    }
}
