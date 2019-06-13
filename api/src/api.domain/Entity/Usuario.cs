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
        public List<Grupo> GruposCedidas { get; set; }
        public List<Grupo> GruposNaoCedidas { get; set; }
        
        public Usuario()
        {
            GruposUsuarios = new List<GrupoUsuario>();
            GruposCedidas = new List<Grupo>();
            GruposNaoCedidas = new List<Grupo>();
        }
    }
}
