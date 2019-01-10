using api.domain.Entity;
using api.domain.Interfaces;
using api.domain.Services.Commons;
using libs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace api.domain.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuario)
        {
            _usuarioRepository = usuario;
        }
        
        public Usuario Autenticar(string login, string senha)
        {
            string senhaCriptografada = Seguranca.GerarHash(senha);
            return _usuarioRepository.Autenticar(login, senhaCriptografada);
        }

        public IEnumerable<Usuario> BuscarTodos()
        {
            return _usuarioRepository.PesquisarTodos();
        }

        public bool IsAtivo(int id)
        {
            if (id==0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Id não informado");
            }

            return _usuarioRepository.Pesquisar(x => x.Id == id && x.Ativo == "S").Count() > 0;
        }

        public Usuario BuscarPorId(int id)
        {
            return _usuarioRepository.PesquisarPorId(id);
        }

        public Usuario Cadastrar(Usuario usuario)
        {
            if (string.IsNullOrEmpty(usuario.Login))
            {
               throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Login não informado");
            }

            if (string.IsNullOrEmpty(usuario.Senha))
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Senha não informada");
            }

            return _usuarioRepository.Inserir(usuario);
        }

        public Usuario Excluir(int id)
        {   

            if (id == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Id não informado");
            }

            var usuario = _usuarioRepository.PesquisarPorId(id);

            if (usuario == null)
            {
                throw new MensagemException(EnumStatusCode.NaoEncontrado, "Usuario não encontrado");
            }

            _usuarioRepository.Excluir(usuario);

            return usuario;

        }
    }
}
