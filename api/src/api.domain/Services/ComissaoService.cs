using System;
using api.domain.Entity;
using api.domain.Interfaces;
using api.domain.Services.Commons;
using api.libs;
using System.Collections.Generic;
using api.domain.Services.DTO;

namespace api.domain.Services
{
    public class ComissaoService
    {
        private readonly IComissaoRepository _comissaoRepository;
        private readonly IParametroRepository _param;
        IParcelaRepository _parcela;

        public ComissaoService(IComissaoRepository comissao, IParametroRepository param, IParcelaRepository parcela)
        {
            _comissaoRepository = comissao;
            _param          = param;
            _parcela        = parcela;
        }

        public string ModelSerializale()
        {
            return Json.ToJson(new Comissao());
        }

        public string Status()
        {          
            var listaEnum = Enum.GetNames(typeof(EnumStatusComissao));
            return Json.ToJson(listaEnum);
        }

        public IEnumerable<Comissao> Listar(ComissaoDTO ComissaoVo)
        {
            return _comissaoRepository.Listar(ComissaoVo);
        }

        private Comissao Cadastrar(Comissao Comissao)
        {
            ValidarModelo(Comissao);         
            return _comissaoRepository.Inserir(Comissao);
        }

        private void Editar(Comissao comissao)
        {
            ValidarModelo(comissao);
            _comissaoRepository.Atualizar(comissao);
        }


        public IEnumerable<Comissao> Lov(int? Id = null)
        {
            var filtro = new ComissaoDTO();
            filtro.Id = (Id.HasValue) ? Id : null;

            return _comissaoRepository.Listar(filtro);
        }

        public Comissao PagarComissao(Comissao comissao)
        {

            if (comissao.Id == 0)
            {
                comissao = Cadastrar(comissao);
            }
            else
            {
                Editar(comissao);
            }
            
            return comissao;

        }

        public void Excluir(int id)
        {

            if (id == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir o Comissao. Comissao não localizado");
            }

            var Comissao = _comissaoRepository.PesquisarPorId(id);


            if (Comissao == null || Comissao.Id == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir o Comissao. Comissao não localizado");
            }

            _comissaoRepository.Excluir(Comissao);
        }

        private void ValidarModelo(Comissao comissao)
        {
            if (comissao.FaturamentoId ==0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Faturamento obrigatório");
            }

            if (comissao.Percentual == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Percentual obrigatório");
            }

            if (comissao.ValorApagar == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Valor a Apagar obrigatório");
            }


            if (comissao.ValorPago > comissao.ValorApagar)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Valor do pagamento não poderá ser maior que o valor a pagar");
            }
            
            if (comissao.ValorPago < comissao.ValorApagar)
            {
                comissao.Status = EnumStatusComissao.ParcialmentePago;
            }

            if (comissao.ValorPago == comissao.ValorApagar)
            {
                comissao.Status = EnumStatusComissao.Pago;
            }

        }
    }
}
