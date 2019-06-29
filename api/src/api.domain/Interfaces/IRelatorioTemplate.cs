using api.domain.Services.DTO;

namespace api.domain.Interfaces
{
    public interface IRelatorioTemplate
    {
        string Titulo { get; set; }
        string Descricao { get;set;}
        string GerarHtml(RelatorioDTO relDTO);
    }
}
