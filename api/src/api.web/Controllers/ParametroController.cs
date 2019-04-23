using api.domain.Entity;
using api.domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.web.Controllers
{
    [Route("api/parametro")]
    [ApiController]
    public class ParametroController : ControllerBase
    {
        public readonly ParametroService _servico;

        public ParametroController(ParametroService servico)
        {
            _servico = servico;
        }

        [HttpGet("model")]
        public IActionResult GetModel()
        {
            string model = _servico.ModelSerializale();
            return Ok(model);
        }


        [HttpGet("listar-parametro")]
        public IActionResult GetParametros()
        {
            return Ok(_servico.Chaves());
        }

        [HttpPost("listar")]
        public IActionResult Listar(Parametro entidade)
        {
            return Ok(_servico.Listar(entidade));
        }

        [HttpGet("{id}")]
        public IActionResult GetParametro(string chave)
        {            
            return Ok(_servico.Lov(chave));
        }

        [HttpPost]
        public IActionResult Cadastrar(Parametro entidade)
        {       
            return Ok(_servico.Cadastrar(entidade));
        }

        [HttpPut]
        public IActionResult Editar(Parametro entidade)
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