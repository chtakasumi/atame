using api.domain.Entity;
using api.domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.web.Controllers
{
    [Route("api/tipoCurso")]
    [ApiController]
    public class TipoCursoController : ControllerBase
    {
        public readonly TipoCursoService _tipoCurso;

        public TipoCursoController(TipoCursoService tipoCurso)
        {
            _tipoCurso = tipoCurso;
        }


        [HttpGet("model")]
        public IActionResult GetModel()
        {
            string model = _tipoCurso.ModelSerializale();
            return Ok(model);
        }

        [HttpPost("listar")]
        public IActionResult Listar(TipoCurso vo)
        {
            return Ok(_tipoCurso.Listar(vo));
        }

        [HttpGet("{descricao}")]
        public IActionResult GetTipoCurso(string descricao)
        {            
            return Ok(_tipoCurso.Lov(descricao));
        }

        [HttpPost]
        public IActionResult Cadastrar(TipoCurso entidade)
        {       
            return Ok(_tipoCurso.Cadastrar(entidade));
        }

        [HttpPut]
        public IActionResult Editar(TipoCurso entidade)
        {
            _tipoCurso.Editar(entidade);
            return Ok();
        }

        [HttpDelete]
        public IActionResult Excluir(int id)
        {
            _tipoCurso.Excluir(id);
            return Ok();
        }
    }
}