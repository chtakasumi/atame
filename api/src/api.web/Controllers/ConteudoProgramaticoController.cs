using api.domain.Entity;
using api.domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.web.Controllers
{
    [Route("api/conteudoProgramatico")]
    [ApiController]
    public class ConteudoProgramaticoController : ControllerBase
    {
        public readonly ConteudoProgramaticoService _servico;

        public ConteudoProgramaticoController(ConteudoProgramaticoService servico)
        {
            _servico = servico;
        }

        [HttpGet("{descricao}")]
        public IActionResult GetTipoCurso(string descricao)
        {
            return Ok(_servico.Lov(descricao));
        }

        [HttpGet("model")]
        public IActionResult GetModel()
        {
            string usuario = _servico.ModelSerializale();
            return Ok(usuario);
        }

        [HttpPost("listar")]
        public IActionResult Listar(ConteudoProgramatico entidade)
        {
            return Ok(_servico.Listar(entidade));
        }

        [HttpPost]
        public IActionResult Cadastrar(ConteudoProgramatico entidade)
        {
            return Ok(_servico.Cadastrar(entidade));
        }

        [HttpPut]
        public IActionResult Editar(ConteudoProgramatico entidade)
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