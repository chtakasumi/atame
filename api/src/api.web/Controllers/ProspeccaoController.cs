using api.domain.Entity;
using api.domain.Services;
using api.domain.Services.DTO;
using Microsoft.AspNetCore.Mvc;

namespace api.web.Controllers
{
    [Route("api/prospeccao")]
    [ApiController]
    public class ProspeccaoController : ControllerBase
    {
        public readonly ProspeccaoService _servico;

        public ProspeccaoController(ProspeccaoService servico)
        {
            _servico = servico;
        }

        [HttpGet("model")]
        public IActionResult GetModel()
        {
            string model = _servico.ModelSerializale();
            return Ok(model);
        }
        
        [HttpPost("listar")]
        public IActionResult Listar(ProspeccaoDTO entidade)
        {
            return Ok(_servico.Listar(entidade));
        }

        [HttpGet("{id}")]
        public IActionResult GetProspeccao(int id)
        {            
            return Ok(_servico.Lov(id));
        }

        [HttpPost]
        public IActionResult Cadastrar(Prospeccao entidade)
        {       
            return Ok(_servico.Cadastrar(entidade));
        }

        [HttpPut]
        public IActionResult Editar(Prospeccao entidade)
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

        //********************************************************************************//
        //**************************Metodos dos interresses******************************//
        //******************************************************************************//
        [HttpGet("interesse/model")]
        public IActionResult GetModelInteresseConteudo()
        {
            string TurmaConteudo = _servico.ModelinterresseSerializale();
            return Ok(TurmaConteudo);
        }

        [HttpPost("interesse")]
        public IActionResult VincularInteresse(ProspeccaoInteresse entidade)
        {
            return Ok(_servico.VincularInteresse(entidade));
        }

        [HttpDelete("interesse")]
        public IActionResult ExcluirVinculoInteresse(int id)
        {
            _servico.ExcluirVinculoInteresse(id);
            return Ok();
        }
    }
}