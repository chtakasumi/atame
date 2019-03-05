using api.domain.Entity;
using api.domain.Interfaces;
using api.domain.Services.Commons;
using api.libs;
using System.Collections.Generic;

namespace api.domain.Services
{
    public class VendedorService
    {

        private readonly IVendedorRepository _VendedorRepository;

        public VendedorService(IVendedorRepository Vendedor)
        {
            _VendedorRepository = Vendedor;
        }
        
        public string ModelSerializale()
        {  
            return Json.ToJson(new Vendedor());                
        }

        public IEnumerable<Vendedor> Listar(Vendedor VendedorVo)
        {
            return _VendedorRepository.Listar(VendedorVo);
        }

        public Vendedor Cadastrar(Vendedor Vendedor)
        {
            ValidarModelo(Vendedor);

            return _VendedorRepository.Inserir(Vendedor);
        }

        public IEnumerable<Vendedor> Lov(string nome=null)
        {
            var filtro = new Vendedor();
            filtro.Nome = (nome != "null")? nome : null;

            return _VendedorRepository.Listar(filtro);
        }

        public void Editar(Vendedor Vendedor)
        {
            ValidarModelo(Vendedor);
            _VendedorRepository.Atualizar(Vendedor);
        }

        public void Excluir(int id)
        {

            if (id == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir o Vendedor. Vendedor não localizado");
            }

            var Vendedor = _VendedorRepository.PesquisarPorId(id);
            

            if (Vendedor == null || Vendedor.Id == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir o Vendedor. Vendedor não localizado");
            }

            _VendedorRepository.Excluir(Vendedor);
        }

        private void ValidarModelo(Vendedor Vendedor)
        {
          
            if (string.IsNullOrEmpty(Vendedor.Nome))
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Nome do Vendedor não informado");
            }           
        }
    }    
}
