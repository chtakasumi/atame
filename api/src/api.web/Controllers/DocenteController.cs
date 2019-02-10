using api.domain.Entity;
using api.domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.web.Controllers
{
    [Route("api/docente")]
    [ApiController]
    public class DocenteController : ControllerBase
    {
        public readonly DocenteService _Docente;

        public DocenteController(DocenteService docente)
        {
            _Docente = docente;
        }


        [HttpGet("model")]
        public IActionResult GetModel()
        {
            string usuario = _Docente.ModelSerializale();
            return Ok(usuario);
        }

        [HttpPost("listar")]
        public IActionResult Listar(Docente docente)
        {
            return Ok(_Docente.Listar(docente));
        }

        [HttpGet("{descricao}")]
        public IActionResult GetTipoCurso(string descricao)
        {
            return Ok(_Docente.Lov(descricao));
        }

        [HttpPost]
        public IActionResult Cadastrar(Docente docente)
        {
            return Ok(_Docente.Cadastrar(docente));
        }

        [HttpPut]
        public IActionResult Editar(Docente docente)
        {
            _Docente.Editar(docente);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Excluir(int id)
        {
            _Docente.Excluir(id);
            return Ok();
        }
    }
}