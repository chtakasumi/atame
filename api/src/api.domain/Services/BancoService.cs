using api.domain.Entity;
using api.domain.Interfaces;
using api.domain.Services.Commons;
using api.libs;
using System.Collections.Generic;

namespace api.domain.Services
{
    public class BancoService
    {

        private readonly IBancoRepository _bancoRepository;

        public BancoService(IBancoRepository banco)
        {
            _bancoRepository = banco;
        }
        
        public string ModelSerializale()
        {  
            return Json.ToJson(new Banco());                
        }

        public IEnumerable<Banco> Listar(Banco bancoVo)
        {
            return _bancoRepository.Listar(bancoVo);
        }

        public Banco Cadastrar(Banco banco)
        {
            ValidarModelo(banco);

            return _bancoRepository.Inserir(banco);
        }

        public IEnumerable<Banco> Lov(string nome=null)
        {
            var filtro = new Banco();
            filtro.Nome = (nome != "null")? nome : null;

            return _bancoRepository.Listar(filtro);
        }

        public void Editar(Banco banco)
        {
            ValidarModelo(banco);
            _bancoRepository.Atualizar(banco);
        }

        public void Excluir(int id)
        {

            if (id == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir o Banco. Banco não localizado");
            }

            var banco = _bancoRepository.PesquisarPorId(id);
            

            if (banco == null || banco.Id == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir o Banco. Banco não localizado");
            }

            _bancoRepository.Excluir(banco);
        }

        private void ValidarModelo(Banco banco)
        {
          
            if (string.IsNullOrEmpty(banco.Nome))
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Nome do Banco não informado");
            }           
        }
    }    
}
