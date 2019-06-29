using api.domain.Entity;
using api.domain.Interfaces;
using api.domain.Services;
using api.domain.Services.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Text;

namespace api.domain.TemplateRelatorio
{
    public class ExecutorTemplate : IRelatorioTemplate
    {
        
        public GeradorRelatorioService _geradorRelatorioService { get; }
        public string Titulo { get ; set ; }

        public ExecutorTemplate(GeradorRelatorioService geradorRelatorioService)
        {
            _geradorRelatorioService = geradorRelatorioService;           
        }

        public string GerarHtml(RelatorioDTO relDTO)
        {
            GeradorRelatorio entity = _geradorRelatorioService.PesquisarPorId(relDTO.RelatorioId);

            Titulo = entity.Nome;
            var html = "<table style=\"width:100%\"><thead><tr>";

            using (DbDataReader reader = _geradorRelatorioService.ExecutarQuery(entity.Query))
            {
                //monta as colunas da tabela              
                ReadOnlyCollection<DbColumn> tbEsquema = reader.GetColumnSchema();
                foreach (DbColumn coluna in reader.GetColumnSchema())
                {
                    html += string.Concat("<th>", coluna.ColumnName, "</th>");
                }

                html += "</tr></thead><tbody>";


                while (reader.Read())
                {
                    html += "<tr>";
                    for (int i = 0; i < tbEsquema.Count; i++)
                    {
                        html += string.Concat("<td>",reader.GetValue(i), "</td>");
                    }
                    html += "</tr>";
                }

                html += "</tbody></table>";

            }




            return html;
        }
      
    }
}
