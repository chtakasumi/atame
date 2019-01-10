using System.Collections.Generic;
using api.domain.Entity;
using api.domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuario;

        public UsuarioController(UsuarioService usuario)
        {
            _usuario = usuario;
        }

        // GET: api/Usuario
        [HttpGet]       
        public IEnumerable<Usuario> GetUsuarios()
        {
            return _usuario.BuscarTodos();            

        }
        
        // GET: api/Usuario
        [HttpGet("{id}")]
        public IActionResult GetUsuario(int id)
        {
            var usuario = _usuario.BuscarPorId(id);
            return Ok(usuario);
        }

        // GET: api/Usuario
        [HttpGet("IsAtivo/{id}")]
        public IActionResult IsAtivo(int id)
        {
            var ativo = _usuario.IsAtivo(id);
            return Ok(ativo);
        }
        
        // GET: api/Usuario
        [HttpPost]
        public Usuario Autenticar(string login, string senha)
        {
            return _usuario.Autenticar(login, senha);
        }


        // GET: api/Usuario
        [HttpPost]
        public IActionResult PostUsuario([FromBody] Usuario usuario)
        {
            var usu = _usuario.Cadastrar(usuario);

            return CreatedAtAction("PostUsuario", new { id = usu.Id }, usu);
        }

        // GET: api/Usuario
        [HttpDelete("{id}")]
        public IActionResult DeleteUsuario([FromRoute] int id)
        {
            var usuario = _usuario.Excluir(id);
            return Ok(usuario);
        }


    }
}