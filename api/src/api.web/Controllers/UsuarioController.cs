using System.Collections.Generic;
using api.domain.Entity;
using api.domain.Services;
using api.domain.Services.Commons;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace api.web.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        public readonly UsuarioService _usuario;

        public UsuarioController(UsuarioService usuario)
        {
            _usuario = usuario;
        }


        // crud
        [HttpGet]
        public IActionResult GetUsuarios()
        {
            return Ok(_usuario.BuscarTodos());
        }

        [HttpGet("{id}")]
        public IActionResult GetUsuario(int id)
        {
            var usuario = _usuario.BuscarPorId(id);
            return Ok(usuario);
        }

        [HttpPost]
        public IActionResult PostUsuario([FromBody] Usuario usuario)
        {
            var usu = _usuario.Cadastrar(usuario);
            return CreatedAtAction("PostUsuario", new { id = usu.Id }, usu);
        }

        [HttpPut]
        public IActionResult PutUsuario([FromBody] Usuario usuario)
        {
            _usuario.Atualizar(usuario);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUsuario([FromRoute] int id)
        {
            var usuario = _usuario.Excluir(id);
            return Ok(usuario);
        }
        //fim do crud
        

        [HttpGet("{id}/grupo")]      
        public IEnumerable<Usuario> GetUsuarioEGrupos(int id)
        {
            return _usuario.BuscarComGrupo(id);         
        }

        [HttpGet("IsAtivo/{id}")]
        public IActionResult IsAtivo(int id)
        {
            var ativo = _usuario.IsAtivo(id);
            return Ok(ativo);
        }

        [HttpPost("Autenticar")]
        public IActionResult Autenticar([FromBody] Usuario usuario)
        {
            return Ok(_usuario.Autenticar(usuario.Login, usuario.Senha));
        }

        [HttpGet("IsAutenticado")]
        public IActionResult IsAutenticado(string cookie)
        {
            return Ok(_usuario.IsAutenticado(cookie));
        }


    }
}