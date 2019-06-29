using api.domain.Entity;
using api.domain.Services;
using api.domain.TemplateRelatorio;
using Microsoft.AspNetCore.Mvc;

namespace api.web.Controllers
{
    [Route("api/gerador-relatorio")]
    [ApiController]
    public class GeradorRelatorioController : ControllerBase
    {
        public readonly GeradorRelatorioService _servico;

        public GeradorRelatorioController(GeradorRelatorioService servico)
        {
            _servico = servico;
        }

        [HttpGet("model")]
        public IActionResult GetModel()
        {
            string model = _servico.ModelSerializale();
            return Ok(model);
        }

        [HttpPost("listar")]
        public IActionResult Listar(GeradorRelatorioDTO entidade)
        {
            return Ok(_servico.Listar(entidade));
        }

        [HttpGet("{descricao}")]
        public IActionResult GetGeradorRelatorio(string descricao)
        {            
            return Ok(_servico.Lov(descricao));
        }

        [HttpPost]
        public IActionResult Cadastrar(GeradorRelatorio entidade)
        {       
            return Ok(_servico.Cadastrar(entidade));
        }

        [HttpPut]
        public IActionResult Editar(GeradorRelatorio entidade)
        {
            _servico.Editar(entidade);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Excluir(int id)
        {
            _servico.Excluir(id);
            return Ok();
        }


        [HttpPost("executar")]   
        public IActionResult Executar(int id)
        {
            //todo: finalizar
            GeradorRelatorio entity = _servico.PesquisarPorId(id);

            string html = "";
            using (var query = _servico.ExecutarQuery(entity.Query))
            {
                html = ExecutorTemplate.GerarHtml(query);
            }

            return Ok(html);
        }
    }
}