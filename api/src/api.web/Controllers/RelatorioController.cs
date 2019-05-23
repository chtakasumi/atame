using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DinkToPdf;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.web.Controllers
{
    [Route("api/relatorio")]
    [ApiController]
    public class RelatorioController : ControllerBase
    {

        [HttpGet("teste")]
        public IActionResult GetTeste()
        {

            var converter = new BasicConverter(new PdfTools());
           // var converter = new SynchronizedConverter(new PdfTools());


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

            var file =  converter.Convert(doc);
            return File(file, "application/pdf");
           
        }
    }
}
