using api.domain.Entity;
using api.domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.web.Controllers
{
    [Route("api/desconto")]
    [ApiController]
    public class DescontoController : ControllerBase
    {
        public readonly DescontoService _servico;

        public DescontoController(DescontoService servico)
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
        public IActionResult Listar(Desconto entidade)
        {
            return Ok(_servico.Listar(entidade));
        }

        [HttpGet("{descricao}")]
        public IActionResult GetDesconto(string descricao)
        {            
            return Ok(_servico.Lov(descricao));
        }

        [HttpPost]
        public IActionResult Cadastrar(Desconto entidade)
        {       
            return Ok(_servico.Cadastrar(entidade));
        }

        [HttpPut]
        public IActionResult Editar(Desconto entidade)
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