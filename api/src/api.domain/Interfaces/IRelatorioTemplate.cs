using api.domain.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace api.domain.Interfaces
{
    public interface IRelatorioTemplate
    {
        string Titulo { get; }
        string GerarHtml(RelatorioDTO relDTO);
    }
}
