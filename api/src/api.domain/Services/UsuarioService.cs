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
            var usuario = new Usuario();
            usuario.GruposNaoCedidas = _usuarioRepository.ListarGrupo();
            return Json.ToJson(usuario);
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

            #region //Limpa o grups do usuario e insere novamente os grupos para o usuario em questão

            if (usuario.Id > 0)
            {               
                _usuarioRepository.RemoverGrupoUsuario(usuario.Id);
            }

            usuario.GruposUsuarios.Clear();

            foreach (Grupo grupoCedidos in usuario.GruposCedidas)
            {
                usuario.GruposUsuarios.Add(new GrupoUsuario
                {
                    Id = 0,
                    UsuarioId = usuario.Id,
                    Usuario = null,
                    GrupoId = grupoCedidos.Id.Value,
                    Grupo = null
                });
            }

            #endregion

            _usuarioRepository.Atualizar(usuario);
        }

        public Usuario Cadastrar(Usuario usuario)
        {
            ValidarModel(usuario);
            var user=  _usuarioRepository.Inserir(usuario);
            Atualizar(user);
            return user;

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

            if (usuario.Id > 0)
            {
                var user= _usuarioRepository.PesquisarPorId(usuario.Id);
                //se seja que esta chegando for diferente da do banco de dados
                if (user.Senha != usuario.Senha)
                {
                    usuario.Senha= Seguranca.GerarHash(usuario.Senha);
                }
                _usuarioRepository.EntityStateDetached(user);                                
            }
            else
            {
                usuario.Senha = Seguranca.GerarHash(usuario.Senha); //criptograva a senha se estiver sendo cadastra
            }

            usuario.Login = usuario.Login.Trim().ToUpper();
            usuario.Vendedor = null;
        }
        
        public bool IsAutenticado(string cookie)
        {
            //todo: verifico se o cookie não foi expirado
            return true;
        }
    }
}
