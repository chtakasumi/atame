using api.domain.Entity;
using api.domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.web.Controllers
{
    [Route("api/vendedor")]
    [ApiController]
    public class VendedorController : ControllerBase
    {
        public readonly VendedorService _servico;

        public VendedorController(VendedorService servico)
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
        public IActionResult Listar(Vendedor entidade)
        {
            return Ok(_servico.Listar(entidade));
        }

        [HttpGet("{descricao}")]
        public IActionResult GetVendedor(string descricao)
        {            
            return Ok(_servico.Lov(descricao));
        }

        [HttpPost]
        public IActionResult Cadastrar(Vendedor entidade)
        {       
            return Ok(_servico.Cadastrar(entidade));
        }

        [HttpPut]
        public IActionResult Editar(Vendedor entidade)
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