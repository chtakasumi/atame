using api.domain.Entity;
using api.domain.Services;
using api.domain.Services.DTO;
using Microsoft.AspNetCore.Mvc;

namespace api.web.Controllers
{
    [Route("api/comissao")]
    [ApiController]
    public class ComissaoController : ControllerBase
    {
        public readonly ComissaoService _servico;

        public ComissaoController(ComissaoService servico)
        {
            _servico = servico;
        }

        [HttpGet("model")]
        public IActionResult GetModel()
        {
            string model = _servico.ModelSerializale();
            return Ok(model);
        }
        
        [HttpGet("listar-status")]
        public IActionResult GetStatus()
        {
            return Ok(_servico.Status());
        }

        [HttpPost("listar")]
        public IActionResult Listar(ComissaoDTO entidade)
        {
            return Ok(_servico.Listar(entidade));
        }

        [HttpGet("{descricao}")]
        public IActionResult GetComissao(int id)
        {            
            return Ok(_servico.Lov(id));
        }

        [HttpPut]
        [HttpPost]
        public IActionResult PagarComissao(Comissao entidade)
        {
            Comissao comissao= _servico.PagarComissao(entidade);
            return Ok(comissao);
        }

        [HttpDelete]
        public IActionResult Excluir(int id)
        {
            _servico.Excluir(id);
            return Ok();
        }
    }
}