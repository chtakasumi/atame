
namespace api.domain.Entity
{
    public class Perfil
    {
        public int Id { get; set; }       
        public string Modulo { get; set; }
        public string Menu { get; set; }
        public string Funcionalidade { get; set; }
        public string Descricao { get; set; }
        public int Order { get; set; }
    }
}