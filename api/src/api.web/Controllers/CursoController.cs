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

        [HttpGet("docente/model")]
        public IActionResult GetModelCursoDocente()
        {
            string cursoDocente = _servico.ModelCursoDocenteSerializale();
            return Ok(cursoDocente);
        }

        [HttpGet("conteudo/model")]
        public IActionResult GetModelCursoConteudo()
        {
            string cursoConteudo = _servico.ModelCursoConteudoProgramaticoSerializale();
            return Ok(cursoConteudo);
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


        [HttpPost("docente")]
        public IActionResult VincularDocente(CursoDocente entidade)
        {
            return Ok(_servico.VincularDocente(entidade));
        }

        [HttpDelete("docente")]
        public IActionResult ExcluirVinculoDocente(int id)
        {
            _servico.ExcluirVinculoDocente(id);
            return Ok();
        }
        
        [HttpDelete("conteudo")]
        public IActionResult ExcluirVinculoConteudo(int id)
        {
            _servico.ExcluirVinculoConteudoProgramatico(id);
            return Ok();
        }

        [HttpPost("conteudo")]
        public IActionResult VincularConteudo(CursoConteudoProgramatico entidade)
        {
            return Ok(_servico.VincularConteudoProgramatico(entidade));
        }
        

    }
}