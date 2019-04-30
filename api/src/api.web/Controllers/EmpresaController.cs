using api.domain.Entity;
using api.domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.web.Controllers
{
    [Route("api/empresa")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        public readonly EmpresaService _servico;

        public EmpresaController(EmpresaService servico)
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
        public IActionResult Listar(EmpresaDTO entidade)
        {
            return Ok(_servico.Listar(entidade));
        }

        [HttpGet("{descricao}")]
        public IActionResult GetEmpresa(string descricao)
        {            
            return Ok(_servico.Lov(descricao));
        }

        [HttpPost]
        public IActionResult Cadastrar(Empresa entidade)
        {       
            return Ok(_servico.Cadastrar(entidade));
        }

        [HttpPut]
        public IActionResult Editar(Empresa entidade)
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