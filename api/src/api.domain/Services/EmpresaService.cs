using api.domain.Entity;
using api.domain.Interfaces;
using api.domain.Services.Commons;
using api.libs;
using System.Linq;
using System.Collections.Generic;

namespace api.domain.Services
{
    public class EmpresaService
    {

        private readonly IEmpresaRepository _EmpresaRepository;

        public EmpresaService(IEmpresaRepository Empresa)
        {
            _EmpresaRepository = Empresa;
        }
        
        public string ModelSerializale()
        {  
            return Json.ToJson(new Empresa());                
        }

        public IEnumerable<Empresa> Listar(EmpresaDTO EmpresaDTO)
        { 
            return _EmpresaRepository.Listar(EmpresaDTO);
        }

        public Empresa Cadastrar(Empresa Empresa)
        {
            ValidarModelo(Empresa);

            return _EmpresaRepository.Inserir(Empresa);
        }

        public IEnumerable<Empresa> Lov(string razaoSocial = null)
        {
            var filtro = new EmpresaDTO();
            filtro.RazaoSocial = (razaoSocial != "null")? razaoSocial : null;

            return _EmpresaRepository.Listar(filtro);
        }

        public void Editar(Empresa Empresa)
        {
            ValidarModelo(Empresa);
            _EmpresaRepository.Atualizar(Empresa);
        }

        public Empresa GetEmpresa()
        {
          var empresa=  _EmpresaRepository.BuscarTodos();
            return empresa.FirstOrDefault();
        }

        public void Excluir(int id)
        {

            if (id == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir o Empresa. Empresa não localizado");
            }

            var Empresa = _EmpresaRepository.PesquisarPorId(id);
            

            if (Empresa == null || Empresa.Id == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir o Empresa. Empresa não localizado");
            }

            _EmpresaRepository.Excluir(Empresa);
        }

        private void ValidarModelo(Empresa Empresa)
        {
          
            if (string.IsNullOrEmpty(Empresa.RazaoSocial))
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Nome do Empresa não informado");
            }
            
            if (string.IsNullOrEmpty(Empresa.Cnpj))
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "CpfCnpj do Empresa não informado");
            }
                        
            if (string.IsNullOrEmpty(Empresa.Telefone) && string.IsNullOrEmpty(Empresa.Celular) 
                && string.IsNullOrEmpty(Empresa.Email))
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Infome ao menos um tipo de contato. Celular, Telefone ou E-mail");
            }

            if (Empresa.UFId ==0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "UF obrigatório.");
            }
            
            if (Empresa.MunicipioId == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Municipio obrigatório.");
            }

            Empresa.UF = null;
            Empresa.Municipio = null;          

        }
    }    
}
