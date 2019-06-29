using api.domain.Entity;
using api.domain.Interfaces;
using api.domain.Services;
using api.domain.Services.DTO;
using api.domain.TemplateRelatorio;
using api.infra;
using api.infra.Repository;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;

namespace api.web.Controllers
{
    [Route("api/relatorio")]
    [ApiController]
    public class RelatorioController : ControllerBase
    {
        public DBContext _context;
        private IConverter _converter;

        public RelatorioController(DBContext context, IConverter converter)
        {
            _context = context;
            _converter = converter;
        }

        [HttpGet("teste")]
        public IActionResult GetTeste()
        {

            var converter = new BasicConverter(new PdfTools());
            //var converter = new SynchronizedConverter(new PdfTools());


            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    PageOffset=2
                },
                Objects = {
                        new ObjectSettings() {
                            PagesCount = true,
                            HtmlContent = @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. In consectetur mauris eget ultrices  iaculis. Ut                               odio viverra, molestie lectus nec, venenatis turpis.",
                            WebSettings = { DefaultEncoding = "utf-8" },
                            HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 },
                            FooterSettings = { Line= true,Center="Page [page] of [toPage]"},
                        }
                }
            };

            var file = converter.Convert(doc);
            return File(file, "application/pdf");

        }

        [HttpGet("orcamento")]
        public IActionResult GetOrcamento(int relatorioId, int usuId, string formato)
        {
            var dto = new RelatorioDTO(relatorioId, usuId, formato);

            var template = new OrcamentoTemplate
            (
                new ProspeccaoService(new ProspeccaoRepository(_context)),
                new EmpresaService(new EmpresaRepository(_context)),
                new TurmaService(new TurmaRepository(_context),
                                    new DocenteRepository(_context),
                                    new ConteudoProgramaticoRepository(_context))
            );

            return ExecutaRelatorio(dto, template, Orientation.Portrait);
        }


        [HttpGet("executar-relatorio")]
        public IActionResult ExecutarRelatorio(int relatorioId, int usuId, string formato)
        {
            var dto = new RelatorioDTO(relatorioId, usuId, formato);

            var template = new ExecutorTemplate(new GeradorRelatorioService(new GeradorRelatorioRepository(_context), null));

            return ExecutaRelatorio(dto, template, Orientation.Landscape);
        }

        //todo:Jogar esta parte na camada de dominios
        private IActionResult ExecutaRelatorio(RelatorioDTO dto, IRelatorioTemplate rel, Orientation orientacao)
        {
            var cabecalho = new CabecalhoTemplate(
                rel.GerarHtml(dto),
                rel.Titulo,
                new ParametroService(new ParametroRepository(_context)),
                new EmpresaService(new EmpresaRepository(_context)),
                new UsuarioService(new UsuarioRepository(_context)));

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = orientacao,
                    PaperSize = PaperKind.A4,
                    //Out = @"C:\DinkToPdf\src\DinkToPdf.TestThreadSafe\test.pdf", imforam caso queira salva ro documento
                },
                Objects = {
                        new ObjectSettings()
                        {
                            PagesCount =  true,
                            HtmlContent = cabecalho.GerarHtml(dto),
                            WebSettings = { DefaultEncoding = "utf-8" },
                            //WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet =  Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") }, para carregar um css externo
                            HeaderSettings = {  Spacing = 3 },
                            FooterSettings = { FontSize = 7, Spacing = 3 , Left= "Usuário: "+cabecalho.GetUsuarioLogin(), Center = "Pagina [page] de [toPage]", Right="Data hora: "+DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"), Line = true, },
                        }
                }
            };

            var file = _converter.Convert(doc);

            var nomeFile = cabecalho.GetNomeArquivo();

            return File(file, "application/pdf", nomeFile);

        }
    }
}
