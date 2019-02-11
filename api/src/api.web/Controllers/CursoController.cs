using api.domain.Entity;
using api.domain.Services;
using api.domain.Services.DTO;
using Microsoft.AspNetCore.Mvc;

namespace api.web.Controllers
{
    [Route("api/curso")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        public readonly CursoService _servico;

        public CursoController(CursoService servico)
        {
            _servico = servico;
        }


        [HttpGet("model")]
        public IActionResult GetModel()
        {
            string usuario = _servico.ModelSerializale();
            return Ok(usuario);
        }

        [HttpGet("{descricao}")]
        public IActionResult GetTipoCurso(string descricao)
        {
            return Ok(_servico.Lov(descricao));
        }

        [HttpPost("listar")]
        public IActionResult Listar(CursoDTO entidade)
        {
            return Ok(_servico.Listar(entidade));
        }

        [HttpPost]
        public IActionResult Cadastrar(Curso entidade)
        {
            return Ok(_servico.Cadastrar(entidade));
        }

        [HttpPut]
        public IActionResult Editar(Curso entidade)
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