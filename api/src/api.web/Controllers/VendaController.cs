using api.domain.Entity;
using api.domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.web.Controllers
{
    [Route("api/venda")]
    [ApiController]
    public class VendaController : ControllerBase
    {
        public readonly VendaService _servico;

        public VendaController(VendaService servico)
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
        public IActionResult Listar(VendaDTO entidade)
        {
            return Ok(_servico.Listar(entidade));
        }

        [HttpGet("{id}")]
        public IActionResult GetVenda(int id)
        {            
            return Ok(_servico.Lov(id));
        }

        [HttpPost]
        public IActionResult Cadastrar(Venda entidade)
        {       
            return Ok(_servico.Cadastrar(entidade));
        }

        [HttpPut]
        public IActionResult Editar(Venda entidade)
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

        //********Metodos dos academicos************//

        [HttpGet("academico/model")]
        public IActionResult GetModelTurmaConteudo()
        {
            string TurmaConteudo = _servico.ModelClienteAcademicoSerializale();
            return Ok(TurmaConteudo);
        }

        [HttpPost("academico")]
        public IActionResult VincularDocente(VendaCliente entidade)
        {
            return Ok(_servico.VincularAcademico(entidade));
        }

        [HttpDelete("academico")]
        public IActionResult ExcluirVinculoDocente(int id)
        {
            _servico.ExcluirVinculoAcademico(id);
            return Ok();
        }
    }
}