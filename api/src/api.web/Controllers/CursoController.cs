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
        public readonly CursoService _curso;

        public CursoController(CursoService curso)
        {
            _curso = curso;
        }


        [HttpGet("model")]
        public IActionResult GetModel()
        {
            string usuario = _curso.ModelSerializale();
            return Ok(usuario);
        }

        [HttpPost("listar")]
        public IActionResult Listar(CursoListarVo cursoVo)
        {
            return Ok(_curso.Listar(cursoVo));
        }

        [HttpPost]
        public IActionResult Cadastrar(Curso curso)
        {
            return Ok(_curso.Cadastrar(curso));
        }

        [HttpPut]
        public IActionResult Editar(Curso curso)
        {
            _curso.Editar(curso);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Excluir(int id)
        {
            _curso.Excluir(id);
            return Ok();
        }
    }
}