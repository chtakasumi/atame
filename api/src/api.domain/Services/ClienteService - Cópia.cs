using api.domain.Entity;
using api.domain.Interfaces;
using api.domain.Services.Commons;
using api.libs;
using System.Collections.Generic;

namespace api.domain.Services
{
    public class ClienteService
    {

        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository cliente)
        {
            _clienteRepository = cliente;
        }
        
        public string ModelSerializale()
        {  
            return Json.ToJson(new Cliente());                
        }

        public IEnumerable<Cliente> Listar(ClienteDTO clienteDTO)
        { 
            return _clienteRepository.Listar(clienteDTO);
        }

        public Cliente Cadastrar(Cliente cliente)
        {
            ValidarModelo(cliente);

            return _clienteRepository.Inserir(cliente);
        }

        public IEnumerable<Cliente> Lov(string nomeCpfCnpj = null)
        {
            var filtro = new ClienteDTO();
            filtro.NomeCpfCnpj = (nomeCpfCnpj != "null")? nomeCpfCnpj : null;

            return _clienteRepository.Listar(filtro);
        }

        public void Editar(Cliente cliente)
        {
            ValidarModelo(cliente);
            _clienteRepository.Atualizar(cliente);
        }

        public void Excluir(int id)
        {

            if (id == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir o Cliente. Cliente não localizado");
            }

            var Cliente = _clienteRepository.PesquisarPorId(id);
            

            if (Cliente == null || Cliente.Id == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir o Cliente. Cliente não localizado");
            }

            _clienteRepository.Excluir(Cliente);
        }

        private void ValidarModelo(Cliente cliente)
        {
          
            if (string.IsNullOrEmpty(cliente.Nome))
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Nome do Cliente não informado");
            }
            
            if (string.IsNullOrEmpty(cliente.CpfCnpj))
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "CpfCnpj do Cliente não informado");
            }
                        
            if (string.IsNullOrEmpty(cliente.Telefone) && string.IsNullOrEmpty(cliente.Celular) 
                && string.IsNullOrEmpty(cliente.Email))
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Infome ao menos um tipo de contato. Celular, Telefone ou E-mail");
            }

            if (cliente.UFId ==0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "UF obrigatório.");
            }
            
            if (cliente.MunicipioId == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Municipio obrigatório.");
            }

            var clienteEncontrado = _clienteRepository.Listar(new ClienteDTO { CpfCnpj = cliente.CpfCnpj }).ToEntity();

            _clienteRepository.EntityStateDetached(clienteEncontrado);

            if (clienteEncontrado!=null && clienteEncontrado.Id != cliente.Id)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, string.Format("CPF/CNPJ já registrado na base de dados para o cliente de codigo: {0}, Nome: {1}", clienteEncontrado.Id, clienteEncontrado.Nome));               
            }
            cliente.UF = null;
            cliente.Municipio = null;
            cliente.Vendedor = null;

        }
    }    
}
