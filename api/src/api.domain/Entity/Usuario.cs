using System.Collections.Generic;

namespace api.domain.Entity
{
    public class Usuario
    {
        public int Id { get; set; }
        public int? VendedorId { get; set; }
        public Vendedor Vendedor { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Ativo { get; set; }
        public ICollection<GrupoUsuario> GruposUsuarios { get; set; }

        public Usuario()
        {
            this.GruposUsuarios = new List<GrupoUsuario>();
        }


    }
}
