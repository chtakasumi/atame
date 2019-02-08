using api.domain.Entity;
using api.domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.web.Controllers
{
    [Route("api/conteudoProgramatico")]
    [ApiController]
    public class ConteudoProgramaticoController : ControllerBase
    {
        public readonly ConteudoProgramaticoService _ConteudoProgramatico;

        public ConteudoProgramaticoController(ConteudoProgramaticoService conteudoProgramatico)
        {
            _ConteudoProgramatico = conteudoProgramatico;
        }


        [HttpGet("model")]
        public IActionResult GetModel()
        {
            string usuario = _ConteudoProgramatico.ModelSerializale();
            return Ok(usuario);
        }

        [HttpPost("listar")]
        public IActionResult Listar(ConteudoProgramatico conteudoProgramatico)
        {
            return Ok(_ConteudoProgramatico.Listar(conteudoProgramatico));
        }

        [HttpPost]
        public IActionResult Cadastrar(ConteudoProgramatico conteudoProgramatico)
        {
            return Ok(_ConteudoProgramatico.Cadastrar(conteudoProgramatico));
        }

        [HttpPut]
        public IActionResult Editar(ConteudoProgramatico ConteudoProgramatico)
        {
            _ConteudoProgramatico.Editar(ConteudoProgramatico);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Excluir(int id)
        {
            _ConteudoProgramatico.Excluir(id);
            return Ok();
        }
    }
}