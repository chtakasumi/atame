using api.domain.Entity;
using api.domain.Services;
using api.domain.Services.DTO;
using Microsoft.AspNetCore.Mvc;

namespace api.web.Controllers
{
    [Route("api/turma")]
    [ApiController]
    public class TurmaController : ControllerBase
    {
        public readonly TurmaService _servico;

        public TurmaController(TurmaService servico)
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
        public IActionResult GetModelTurmaDocente()
        {
            string TurmaDocente = _servico.ModelTurmaDocenteSerializale();
            return Ok(TurmaDocente);
        }

        [HttpGet("conteudo/model")]
        public IActionResult GetModelTurmaConteudo()
        {
            string TurmaConteudo = _servico.ModelTurmaConteudoProgramaticoSerializale();
            return Ok(TurmaConteudo);
        }        

        [HttpGet("{descricao}")]
        public IActionResult GetTipoTurma(string descricao)
        {
            return Ok(_servico.Lov(descricao));
        }

        [HttpPost("listar")]
        public IActionResult Listar(TurmaDTO entidade)
        {
            return Ok(_servico.Listar(entidade));
        }

        [HttpPost]
        public IActionResult Cadastrar(Turma entidade)
        {
            return Ok(_servico.Cadastrar(entidade));
        }                

        [HttpPut]
        public IActionResult Editar(Turma entidade)
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
        public IActionResult VincularDocente(TurmaDocente entidade)
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
        public IActionResult VincularConteudo(TurmaConteudoProgramatico entidade)
        {
            return Ok(_servico.VincularConteudoProgramatico(entidade));
        }
        

    }
}