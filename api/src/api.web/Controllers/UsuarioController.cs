using System.Collections.Generic;
using api.domain.Entity;
using api.domain.Services;
using api.domain.Services.Commons;
using api.domain.Services.DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace api.web.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        public readonly UsuarioService _servico;

        public UsuarioController(UsuarioService servico)
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
        public IActionResult Listar(UsuarioDTO entidade)
        {
            return Ok(_servico.Listar(entidade));
        }

        // crud
        [HttpGet]
        public IActionResult GetUsuarios()
        {
            return Ok(_servico.BuscarTodos());
        }

        [HttpGet("{id}")]
        public IActionResult GetUsuario(int id)
        {
            var usuario = _servico.BuscarPorId(id);
            return Ok(usuario);
        }

        [HttpPost]
        public IActionResult PostUsuario([FromBody] Usuario entidade)
        {
            var usu = _servico.Cadastrar(entidade);
            return CreatedAtAction("PostUsuario", new { id = usu.Id }, usu);
        }

        [HttpPut]
        public IActionResult PutUsuario([FromBody] Usuario entidade)
        {
            _servico.Atualizar(entidade);
            return NoContent();
        }

        [HttpDelete]
        public IActionResult DeleteUsuario(int id)
        {
            var usuario = _servico.Excluir(id);
            return Ok(usuario);
        }
        //fim do crud
        

        [HttpGet("{id}/grupo")]      
        public IEnumerable<Usuario> GetUsuarioEGrupos(int id)
        {
            return _servico.BuscarComGrupo(id);         
        }

        [HttpGet("IsAtivo/{id}")]
        public IActionResult IsAtivo(int id)
        {
            var ativo = _servico.IsAtivo(id);
            return Ok(ativo);
        }

        [HttpPost("Autenticar")]
        public IActionResult Autenticar([FromBody] Usuario entidade)
        {
            return Ok(_servico.Autenticar(entidade.Login, entidade.Senha));
        }

        [HttpGet("IsAutenticado")]
        public IActionResult IsAutenticado(string cookie)
        {
            return Ok(_servico.IsAutenticado(cookie));
        }


    }
}