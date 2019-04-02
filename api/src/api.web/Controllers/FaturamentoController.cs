using api.domain.Entity;
using api.domain.Services;
using api.domain.Services.DTO;
using Microsoft.AspNetCore.Mvc;

namespace api.web.Controllers
{
    [Route("api/faturamento")]
    [ApiController]
    public class FaturamentoController : ControllerBase
    {
        public readonly FaturamentoParcelaService _servico;

        public FaturamentoController(FaturamentoParcelaService servico)
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
        public IActionResult Listar(FaturamentoDTO entidade)
        {
            return Ok(_servico.Listar(entidade));
        }

        [HttpGet("{descricao}")]
        public IActionResult GetFaturamento(int? id)
        {            
            return Ok(_servico.Lov(id));
        }

        [HttpPost]
        public IActionResult Cadastrar(Parcela entidade)
        {       
            return Ok(_servico.Cadastrar(entidade));
        }

        [HttpPut]
        public IActionResult Editar(Parcela entidade)
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
    }
}