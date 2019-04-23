using api.domain.Entity;
using api.domain.Interfaces;
using api.domain.Services.Commons;
using api.libs;
using System.Collections.Generic;
using api.domain.Services.DTO;
using System;
using System.Collections;

namespace api.domain.Services
{
    public class FaturamentoParcelaService
    {
        private readonly IFaturamentoRepository _faturamentoRepository;
        private readonly IParcelaRepository _parcelaRepository;
        private readonly IComissaoRepository _comissaoRepository;
        private readonly IParametroRepository _parametroRepository;

        public FaturamentoParcelaService(IFaturamentoRepository faturamento, IParcelaRepository parcela, IComissaoRepository comissao, IParametroRepository parametro)
        {
            this._faturamentoRepository = faturamento;
            this._parcelaRepository = parcela;
            this._comissaoRepository = comissao;
            this._parametroRepository = parametro;
        }

        public string ModelSerializale()
        {
            return Json.ToJson(new Parcela());
        }

        public IEnumerable<Parcela> Listar(FaturamentoDTO faturamentoVo)
        {
            return _faturamentoRepository.ListarParcelas(faturamentoVo);
        }

        private Parcela Cadastrar(Parcela parcelaFaturamento)
        {

            var comissaoVenda = BuscarComissao(parcelaFaturamento.Venda.TurmaId);
            var descontoVenda = parcelaFaturamento.Venda.Desconto;
            var comissaoGerente = Convert.ToDecimal(_parametroRepository.Listar(new Parametro { Chave = EnumParametros.PERCENTUAL_COMISSAO_GERENTE.ToString() }).ToEntity().valor);

            ValidarModelo(parcelaFaturamento);

            Faturamento faturamento = _faturamentoRepository.Inserir(parcelaFaturamento.Faturamento);

            parcelaFaturamento.FaturamentoId = faturamento.Id;
            parcelaFaturamento.Faturamento = null;

            AtualizaStatusParcela(parcelaFaturamento, EnumStatusPgto.Pago);

            var calculoVendedor = new CalculoPercentual(faturamento.ValorPago, comissaoVenda, descontoVenda);
            var calculoGerente = new CalculoPercentual(faturamento.ValorPago, comissaoGerente, descontoVenda,true);

            var listaComissao = new List<Comissao>{

            new Comissao
            {
                FaturamentoId = faturamento.Id,
               // Faturamento = faturamento,
                Percentual = calculoVendedor.ValorNovaComisao,
                ValorApagar = calculoVendedor.ValorAhReceber,
                Status = EnumStatusComissao.EmAberto,
                Gerente = false
            },
            new Comissao
            {
                FaturamentoId = faturamento.Id,
                //Faturamento = faturamento,
                Percentual = calculoGerente.ValorNovaComisao,
                ValorApagar = calculoGerente.ValorAhReceber,
                Status = EnumStatusComissao.EmAberto,
                Gerente = true
            }
        };
            _comissaoRepository.Inserir(listaComissao);



            return parcelaFaturamento;
        }

        private decimal BuscarComissao(int turmaId)
        {
            return _faturamentoRepository.BuscarComissao(turmaId);
        }

        public Parcela BaixarParcela(Parcela entidade)
        {
            if (entidade.FaturamentoId.GetValueOrDefault() == 0)
            {
                entidade = Cadastrar(entidade);
            }
            else
            {
                Editar(entidade);
            }
            return entidade;
        }

        private void AtualizaStatusParcela(Parcela parcela, EnumStatusPgto status)
        {
            parcela.Status = status;
            parcela.Venda = null;
            if (status == EnumStatusPgto.Pendente)
            {
                parcela.Faturamento = null;
                parcela.FaturamentoId = null;
            }
            _parcelaRepository.Atualizar(parcela);
        }

        public IEnumerable<Faturamento> Lov(int? numero = null)
        {
            var filtro = new FaturamentoDTO
            {
                Numero = (numero.HasValue) ? numero : null
            };

            return _faturamentoRepository.Listar(filtro);
        }

        private void Editar(Parcela parcela)
        {
            ValidarModelo(parcela);

            Parcela parcelaAnterior = _parcelaRepository.PesquisarPorId(parcela.Id.GetValueOrDefault());

            if (parcela.Id != parcelaAnterior.Id)
            {
                parcelaAnterior.Faturamento = null;
                AtualizaStatusParcela(parcelaAnterior, EnumStatusPgto.Pendente);
            }

            _faturamentoRepository.Atualizar(parcela.Faturamento);

            //parcela.Faturamento = null;
            //AtualizaStatusParcela(parcela, EnumStatusPgto.Pago);
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
                faturamento.Parcela = _parcelaRepository.PesquisarPorId(faturamento.ParcelaId);
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
