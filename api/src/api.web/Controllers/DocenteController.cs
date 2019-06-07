using api.domain.Entity;
using api.domain.Services;
using api.domain.Services.DTO;
using Microsoft.AspNetCore.Mvc;

namespace api.web.Controllers
{
    [Route("api/docente")]
    [ApiController]
    public class DocenteController : ControllerBase
    {
        public readonly DocenteService _servico;

        public DocenteController(DocenteService servico)
        {
            _servico = servico;
        }


        [HttpGet("model")]
        public IActionResult GetModel()
        {
            string usuario = _servico.ModelSerializale();
            return Ok(usuario);
        }

        [HttpPost("listar")]
        public IActionResult Listar(DocenteDTO entidade)
        {
            return Ok(_servico.Listar(entidade));
        }

        [HttpGet("{descricao}")]
        public IActionResult GetTipoCurso(string descricao)
        {
            return Ok(_servico.Lov(descricao));
        }

        [HttpPost]
        public IActionResult Cadastrar(Docente entidade)
        {
            return Ok(_servico.Cadastrar(entidade));
        }

        [HttpPut]
        public IActionResult Editar(Docente entidade)
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