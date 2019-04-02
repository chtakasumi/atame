using api.domain.Entity;
using api.domain.Interfaces;
using api.domain.Services.Commons;
using api.libs;
using System.Collections.Generic;
using api.domain.Services.DTO;

namespace api.domain.Services
{
    public class FaturamentoParcelaService
    {
        private readonly IFaturamentoRepository _faturamentoRepository;       
        IParcelaRepository _parcela;

        public FaturamentoParcelaService(IFaturamentoRepository faturamento,  IParcelaRepository parcela)
        {
            _faturamentoRepository = faturamento;         
            _parcela        = parcela;
        }

        public string ModelSerializale()
        {
            return Json.ToJson(new Parcela());
        }

        public IEnumerable<Parcela> Listar(FaturamentoDTO faturamentoVo)
        {
            return _faturamentoRepository.ListarParcelas(faturamentoVo);
        }

        public Parcela Cadastrar(Parcela parcelaFaturamento)
        {
            ValidarModelo(parcelaFaturamento);
         
            Faturamento faturamento= _faturamentoRepository.Inserir(parcelaFaturamento.Faturamento);

            parcelaFaturamento.FaturamentoId = faturamento.Id;
            parcelaFaturamento.Faturamento = null;


            AtualizaStatusParcela(parcelaFaturamento, EnumStatusPgto.Pago);           

            return parcelaFaturamento;
        }

        private void AtualizaStatusParcela(Parcela parcela, EnumStatusPgto status)
        {
            parcela.Status = status;

            if (status == EnumStatusPgto.Pendente)
            {
                parcela.Faturamento = null;
                parcela.FaturamentoId = null;
            }
            
            _parcela.Atualizar(parcela);
        }

        public IEnumerable<Faturamento> Lov(int? numero = null)
        {
            var filtro = new FaturamentoDTO
            {
                Numero = (numero.HasValue) ? numero : null
            };

            return _faturamentoRepository.Listar(filtro);
        }

        public void Editar(Parcela parcela)
        {
            ValidarModelo(parcela);

            Parcela parcelaAnterior = _parcela.PesquisarPorId(parcela.Id);

            if (parcela.Id != parcelaAnterior.Id) {
                AtualizaStatusParcela(parcelaAnterior, EnumStatusPgto.Pendente);
            }

            _faturamentoRepository.Atualizar(parcela.Faturamento);

            AtualizaStatusParcela(parcela, EnumStatusPgto.Pago);
        }

        public void Excluir(int id)
        {

            if (id == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir o Faturamento. Faturamento não localizado");
            }

            var faturamento = _faturamentoRepository.PesquisarPorId(id);


            if (faturamento == null || faturamento.Id == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir o Faturamento. Faturamento não localizado");
            }
            
            if (faturamento.Parcela == null)
            {
                faturamento.Parcela  = _parcela.PesquisarPorId(faturamento.ParcelaId);
            }

            AtualizaStatusParcela(faturamento.Parcela, EnumStatusPgto.Pendente);

            _faturamentoRepository.Excluir(faturamento);
        }

        private void ValidarModelo(Parcela parcelaFaturamento)
        {

            if (parcelaFaturamento.Faturamento.CompetenciaMes == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Mês da Competencia não informado");
            }

            if (parcelaFaturamento.Faturamento.CompetenciaAno == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Ano da Competencia não informado");
            }

            if (parcelaFaturamento.Faturamento.ParcelaId == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Parcela do Faturamento não informado");
            }

            if (parcelaFaturamento.VendaId == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Venda do Faturamento não informado");
            }

            if (!parcelaFaturamento.Faturamento.DataPgto.HasValue)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Data Pagamento não informado");
            }
            //no faturamento não tem dados da venda nem da parcela
            parcelaFaturamento.Faturamento.Venda = null;
            parcelaFaturamento.Faturamento.Parcela = null;
        }

      
    }
}
