using api.domain.Entity;
using api.domain.Interfaces;
using api.domain.Services;
using api.domain.Services.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace api.domain.TemplateRelatorio
{
    public class CabecalhoTemplate : IRelatorioTemplate
    {
        private Usuario _usuario;

        public CabecalhoTemplate(           
            string corpoTemplateHtml,
            string titulo,
            ParametroService parametroService,
            EmpresaService empresaService,
            UsuarioService usuarioService)
        {         
            _templateHtml = corpoTemplateHtml;
            _titulo = titulo;
            _parametroService = parametroService;
            _empresaService = empresaService;
            _usuarioService = usuarioService;
        }

        public string _titulo { get; }
        public string _templateHtml { get; }
        public ParametroService _parametroService { get; }
        public EmpresaService _empresaService { get; }
        public UsuarioService _usuarioService { get; }
        string IRelatorioTemplate.Titulo { get; set; }

        public string GerarHtml(RelatorioDTO relDTO)
        {
            this._usuario = _usuarioService.BuscarPorId(relDTO.UsuId);
            return @"<!DOCTYPE html>
                        <html>
                        <head>
                            <style>
                                table {
                                    width: 100%;
                                }
                                table, th, td {
                                    border: 1px solid black;
                                    border-collapse: collapse;
                                }
                                th, td {
                                    padding: 15px;
                                    text-align: left;
                                }
                                .cabecalhoLeft {
                                   display: block;
                                   width: 80%;
                                   float: left;
                                }
                                .cabecalhoRigth {                                  
                                    width: 20%;
                                    display: block;
                                    overflow: hidden;
                                    float: right;
                                }
                              .logo img {
                                    width: 100%;
                                    float: right;
                                }
                                .corpo {                                    
                                    display: block;
                                    clear: both;
                                    padding: 10px;
                                }
                                .descricao {
                                    color: #ea0522;
                                    font-size: 20px;
                                    font-weight: bold;
                                }
                                .SobreEmpresa {
                                    display: block;
                                    border-left: solid 3px #910517;
                                    padding-left: 3px;
                                }
                                .SobreEmpresa .interno {
                                    display: block;
                                    border-left: solid 3px #910517;
                                    padding: 10px;
                                 }
                                .SobreEmpresa p {
                                    font-weight: bolder;
                                    font-size: 32px;
                                    font-family: auto;
                                    margin-top: auto;
                                    margin-bottom: auto;
                                    color: #910517;
                                }    
                                
                            </style>
                        </head>
                        <body>
                            <div class=""cabecalhoLeft"">
                                <div class=""SobreEmpresa"">
                                    <div class=""interno"">
                                        <p>Grupo Atame</p>
                                        <p>"+this._titulo+@"</p>
                                        <span class=""descricao"">Para Cursos de Pós-Graducação e Cursos de Extensão</span>
                                    </div>
                                </div>
                            </div>
                            <div class=""cabecalhoRigth"">
                                <div class=""logo"">
                                    <img src = ""data:image/jpeg;base64,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"" />
                                </div>
                            </div>
                            <div class=""corpo"">
                               "+ this._templateHtml + @"
                            </div>
                        </body>
                   </html>";
        }

        public string GetNomeArquivo()
        {
            return this._titulo + new Random(DateTime.Now.Millisecond).Next() + ".pdf";
        }

        public string GetUsuarioLogin()
        {
            return this._usuario?.Login;
        }
    }
}
