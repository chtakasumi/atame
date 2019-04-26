using api.domain.Entity;
using api.domain.Interfaces;
using api.domain.Services.Commons;
using api.libs;
using System;
using System.Collections.Generic;

namespace api.domain.Services
{
    public class DescontoService
    {

        private readonly IDescontoRepository _descontoRepository;

        public DescontoService(IDescontoRepository desconto)
        {
            _descontoRepository = desconto;
        }
        
        public string ModelSerializale()
        {  
            return Json.ToJson(new Desconto());                
        }

        public IEnumerable<Desconto> Listar(Desconto descontoVo)
        {
            return _descontoRepository.Listar(descontoVo);
        }

        public Desconto Cadastrar(Desconto desconto)
        {
            ValidarModelo(desconto);

            return _descontoRepository.Inserir(desconto);
        }

        public IEnumerable<Desconto> Lov(string percentual)
        {
            var filtro = new Desconto();
            filtro.Percentual = Convert.ToDecimal(percentual == "null" ? "0": percentual);
            return _descontoRepository.Listar(filtro);
        }

        public void Editar(Desconto desconto)
        {            
            ValidarModelo(desconto);
            _descontoRepository.Atualizar(desconto);
        }

        public void Excluir(int id)
        {

            if (id == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir o Desconto. Desconto não localizado");
            }

            var desconto = _descontoRepository.PesquisarPorId(id);
            

            if (desconto == null || desconto.Id == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir o Desconto. Desconto não localizado");
            }

            _descontoRepository.Excluir(desconto);
        }

        private void ValidarModelo(Desconto desconto)
        {
            if (desconto.Percentual==0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Percentual do Desconto não informado");
            }           
        }
    }    
}
