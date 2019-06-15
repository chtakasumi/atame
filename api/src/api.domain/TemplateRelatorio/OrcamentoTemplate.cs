using api.domain.Entity;
using api.domain.Interfaces;
using api.domain.Services;
using api.domain.Services.DTO;
using api.libs;
using System;
using System.Linq;

namespace api.domain.TemplateRelatorio
{
    public class OrcamentoTemplate : IRelatorioTemplate
    {
        readonly ProspeccaoService _prospeccaoService;
        readonly EmpresaService _empresaService;
        readonly TurmaService _turmaService;
        public string Titulo => "Orçamento";

        public OrcamentoTemplate(ProspeccaoService prospeccao, EmpresaService empresaService, TurmaService turmaService)
        {            
            _prospeccaoService = prospeccao;
            _empresaService = empresaService;
            _turmaService =turmaService;
        }
       
        public string GerarHtml(RelatorioDTO relDTO)
        {
            var dto = new ProspeccaoDTO
            {
                Id = relDTO.RelatorioId
            };

            Prospeccao orcamento = _prospeccaoService.ListarRelatorio(dto).SingleOrDefault();

            string interessesHtml = "";
            decimal somaTotal = 0;
            foreach (var item in orcamento.Interesses) {
                interessesHtml += "<tr>";

                interessesHtml += "<td>";
                interessesHtml += item.Curso.Nome.ToUpper();
                interessesHtml += "</td>";

                interessesHtml += "<td>";
                interessesHtml +=  1;
                interessesHtml += "</td>";

                interessesHtml += "<td>";
                interessesHtml += Mascaras.ToMoeda(item.Curso.Preco);
                interessesHtml += "</td>";
                
                interessesHtml += "</tr>";

                somaTotal += item.Curso.Preco;

            }
               
            var cursoPrincial = orcamento.Interesses.First().Curso;

            string conteudosProgramaticosHtml = string.Empty;
            int contador = 1;
            foreach (var item in cursoPrincial.ConteudosProgramaticos) {
                if (contador == 1) {
                    conteudosProgramaticosHtml += "<li>";
                }

                conteudosProgramaticosHtml += "- " + item.ConteudoProgramatico.Identificacao.ToUpper() + "; ";

                if (contador == cursoPrincial.ConteudosProgramaticos.Count())
                {
                    conteudosProgramaticosHtml += "</li>";
                }
                contador++;
            }

            if (string.IsNullOrEmpty(conteudosProgramaticosHtml))
            {
                conteudosProgramaticosHtml = "****";
            }
            
            string docentesProgramaticosHtml = string.Empty;
            foreach (var item in cursoPrincial.Docentes)
            {
                docentesProgramaticosHtml += "<li>- " + item.Docente.Nome.ToUpper() + "<li>";
            }

            if (string.IsNullOrEmpty(docentesProgramaticosHtml))
            {
                conteudosProgramaticosHtml = "****";
            }

            var turmaDto = new TurmaDTO
            {
                CursoId = cursoPrincial.Id,
                Inicio  = DateTime.Now                
            };

            var turma = _turmaService.Listar(turmaDto).FirstOrDefault();

            string turmaNomeHtml = (turma==null)?"**Sem Turma aberta para o curso em questão**": turma.Identificacao;
            string turmaInicio = (turma == null) ? "**Sem Turma aberta para o curso em questão**" : turma.Inicio.Value.ToString("dd/MM/yyyy");

            Empresa empresa = _empresaService.GetEmpresa();
            if (empresa==null) {
                throw new Exception("É necesariocadastrar uma empresa no sistema para impressão desse documento");
            }
            
            var html = @"<style>   
                        .visao-geral h3 {
                            color: #910517;
                            font-weight: bolder;
                        }
                        .visao-geral h3 {
                            padding: 5px;
                            margin:5px;
                        }
                        .visao-geral ul {
                            padding-top: 5px;
                            margin-top: 5px;
                            padding-bottom: 5px;
                            margin-bottom: 5px;
                        }
                        .visao-geral ul {
                            list-style-type:none;
                        }                       
                    </style>
                       
                    <div class=""visao-geral"">
                        <h3>Visão Geral</h3>
                        <blockquote>
                            O grupo Atame tem a satisfação de enviar esta proposta do curso de " + Mascaras.ToUpper(cursoPrincial?.Nome)+ @"
                            para ajudar " + Mascaras.ToUpper(orcamento?.ClienteNome)+ @"a alcançar seu objetivo de aumentar o conhecimento e 
                            consequentemente a atingir suas metas profissionais e financeiras, fornecendo cursos de pós-graduação e 
                            extensão em diversas áreas do conhecimento.
                        </blockquote>
                    </div>
                    <div class=""visao-geral"">
                        <h3>Cliente</h3>
                            <ul>
                                <li><strong>Nome: </strong>" + Mascaras.ToUpper(orcamento?.ClienteNome)+ @"</li>
                                <li><strong>Fone: </strong>" + Mascaras.ToTelefone(orcamento?.ClienteTelefone) + @"</li>
                                <li><strong>Celular: </strong>" + Mascaras.ToTelefone(orcamento?.ClienteCelular) + @"</li>
                                <li><strong>E-mail: </strong>" + Mascaras.ToEmail(orcamento?.ClienteEmail) + @"</li>
                            </ul>
                    </div>
                    <div class=""visao-geral"">
                        <h3>O Curso</h3>
                        <ul>
                            <li><strong>Nome do Curso: </strong>" + Mascaras.ToUpper(cursoPrincial?.Nome) + @"</li>
                            <strong>Conteúdo Programatico: </strong><ul>" + conteudosProgramaticosHtml + @"</ul>
                            <strong>Docentes: </strong><ul> " + docentesProgramaticosHtml + @" </ul> 
                        </ul>
                        </div>
                        <div class=""visao-geral"">
                            <h3>Nossa Proposta</h3>
                            <ul>
                                <li><strong>Turma: </strong>" + turmaNomeHtml + @"</li>
                                <li><strong>Inicio: </strong>" + turmaInicio + @"</li>
                            </ul>
                        </div>
                        <div class=""visao-geral"">
                            <h3>Preços</h3>
                            <table>
                                <thead>
                                    <tr style = ""font-weight:bolder"">
                                        <td> Curso </td>
                                        <td> Qtde </td>
                                        <td> Preço </td>
                                    </tr>
                                </thead>
                                <tbody>
                                    "+ interessesHtml + @"
                                </tbody>
                                <tfoot>
                                    <tr style = ""background-color:#b40c0c;color:white;font-weight:bold;font-size: large;"">
                                        <td> Total </td>
                                        <td colspan=""2"">"+ Mascaras.ToMoeda(somaTotal) + @"</td>
                                    </tr>
                                </tfoot>
                            </table>
                        </div>
                        <div class=""visao-geral"">
                            <h3>Contato</h3>
                            <ul>
                                <li><strong>Empresa: </strong>" + Mascaras.ToUpper(empresa?.RazaoSocial) + @"</li>
                                <li><strong>Fone: </strong>" + Mascaras.ToTelefone(empresa?.Telefone) + @"</li>
                                <li><strong>Cel: </strong>" + Mascaras.ToTelefone(empresa?.Celular)+ @"</li>
                                <li><strong>E-mail: </strong>" + Mascaras.ToEmail(empresa?.Email) + @"</li>                                
                            </ul>                       
                    </div>";

            return html;
        }
    }
}
