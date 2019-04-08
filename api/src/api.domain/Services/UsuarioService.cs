using api.domain.Entity;
using api.domain.Interfaces;
using api.domain.Services.Commons;
using api.domain.Services.DTO;
using api.libs;
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
        
        public DadosChave Autenticar(string login, string senha)
        {

            if (string.IsNullOrEmpty(login))
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Login não informado");
            }

            if (string.IsNullOrEmpty(senha))
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Senha não informada");
            }

            string senhaCriptografada = Seguranca.GerarHash(senha.Trim());
            Usuario usuario =  _usuarioRepository.Autenticar(login.Trim().ToUpper(), senhaCriptografada);

            if (usuario == null)
            {
                throw new MensagemException(EnumStatusCode.NaoAutorizado, "Usuário ou senhas inválidas");
            }
            
            if (usuario.Ativo != "S")
            {
                throw new MensagemException(EnumStatusCode.NaoAutorizado, "Usuário Bloqueado. Entre em contato com o administrador do sistema.");
            }

            return new DadosChave(usuario.Id, usuario.Login, usuario.Ativo,usuario.GruposUsuarios, DateTime.Now);
       
        }

        public IEnumerable<Usuario> Listar(UsuarioDTO dto)
        {
            return _usuarioRepository.Listar(dto);
        }

        public string ModelSerializale()
        {
            return Json.ToJson(new Usuario());
        }

        public IEnumerable<Usuario> BuscarTodos()
        {
            return _usuarioRepository.BuscarTodos();
        }

        public bool IsAtivo(int id)
        {
            if (id==0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Id não informado");
            }

            return _usuarioRepository.Pesquisar(x => x.Id == id && x.Ativo == "S").Count() > 0;
        }

        public IEnumerable<Usuario> BuscarComGrupo(int id)
        {
            return _usuarioRepository.BuscarComGrupo(id);
        }
              
        public Usuario BuscarPorId(int id)
        {
            return _usuarioRepository.PesquisarPorId(id);
        }

        public void Atualizar(Usuario usuario)
        {
            ValidarModel(usuario);

            _usuarioRepository.Atualizar(usuario);
        }

        public Usuario Cadastrar(Usuario usuario)
        {
            ValidarModel(usuario);           
            usuario.Login = usuario.Login.Trim().ToUpper();

            usuario.GruposUsuarios.Add(VincularGrupoMaster());
            
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

        private GrupoUsuario VincularGrupoMaster()
        {
            return new GrupoUsuario {
                GrupoId = 1
            };             
        }

        private void ValidarModel(Usuario usuario)
        {
            if (string.IsNullOrEmpty(usuario.Login))
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Login não informado");
            }

            if (string.IsNullOrEmpty(usuario.Senha))
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Senha não informada");
            }

            usuario.Senha = Seguranca.GerarHash(usuario.Senha);

            usuario.Vendedor = null;
        }
        
        public bool IsAutenticado(string cookie)
        {
            //todo: verifico se o cookie não foi expirado
            return true;
        }
    }
}
