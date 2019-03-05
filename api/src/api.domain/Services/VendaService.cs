using api.domain.Entity;
using api.domain.Interfaces;
using api.domain.Services.Commons;
using api.libs;
using System.Collections.Generic;

namespace api.domain.Services
{
    public class VendaService
    {
        private readonly IVendaRepository _vendaRepository;

        public VendaService(IVendaRepository venda)
        {
            _vendaRepository = venda;
        }

        public string ModelSerializale()
        {
            return Json.ToJson(new Venda());
        }

        public IEnumerable<Venda> Listar(VendaDTO vendaVo)
        {
            return _vendaRepository.Listar(vendaVo);
        }

        public Venda Cadastrar(Venda venda)
        {
            ValidarModelo(venda);
            
            //isso fala para o etity não criar um novo cliente.
            foreach (var vendaCliente in venda.ClientesAcademicos)
            {
                vendaCliente.Id = 0;
                vendaCliente.ClienteAcademico = null;
            }

            return _vendaRepository.Inserir(venda);
        }
       
        public IEnumerable<Venda> Lov(int? Id = null)
        {
            var filtro = new VendaDTO();
            filtro.Id = (Id.HasValue) ? Id : null;

            return _vendaRepository.Listar(filtro);
        }

        public void Editar(Venda venda)
        {
            ValidarModelo(venda);
            _vendaRepository.Atualizar(venda);
        }

        public void Excluir(int id)
        {

            if (id == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir o Venda. Venda não localizado");
            }

            var Venda = _vendaRepository.PesquisarPorId(id);


            if (Venda == null || Venda.Id == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir o Venda. Venda não localizado");
            }

            _vendaRepository.Excluir(Venda);
        }

        private void ValidarModelo(Venda venda)
        {
            if (!venda.Data.HasValue)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Data do Venda não informado");
            }

            if (venda.ClienteFinanceiroId == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Cliente financeiro não informado");
            }

            if (venda.TurmaId == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Turma não informada");
            }

            if (venda.Quantidade == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Quantidade não informada");
            }

            if (venda.ValorCurso == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Valor do Curso não informado");
            }

            if (venda.ClientesAcademicos.Count == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "informe ao menos 1 cliente  Acadêmico");
            }

            if (venda.ClientesAcademicos.Count != venda.Quantidade)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "A quantidade de vagas deve ser a mesma quantidade de acadêmicos.");
            }

            venda.ClienteFinanceiro = null;
            venda.Turma = null;
            venda.Vendedor = null;

            venda.CalcularValorVenda();
        }

        //**Metodos academicos**//
        public string ModelClienteAcademicoSerializale()
        {
            return Json.ToJson(new VendaCliente());
        }

        public VendaCliente VincularAcademico(VendaCliente entidade)
        {
            VendaCliente vendaCliente = _vendaRepository.VincularAcademico(entidade);
            vendaCliente.Venda = _vendaRepository.PesquisarPorId(vendaCliente.VendaId);
            return vendaCliente;
        }

        public void ExcluirVinculoAcademico(int id)
        {
            if (id == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir o vinculo com o acadêmico.");
            }

            VendaCliente vc = _vendaRepository.PesquisarVinculoAcademico(id);
            vc.Venda = null;
            vc.ClienteAcademico = null;

            if (vc == null || vc.Id == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir o vinculo com o acadêmico. Vinculo não localizado");
            }

            //_vendaRepository.BeginTransation();

            _vendaRepository.ExcluirVinculoAcademico(vc);


            //var dto = new VendaDTO
            //{
            //    Id = vc.VendaId
            //};

            //Regra caso exclua um academico.
            //Venda venda = _vendaRepository.Listar(dto).ToEntity();
           // venda.Quantidade = venda.ClientesAcademicos.Count;
            
            //foreach (var academicos in venda.ClientesAcademicos) {
            //    academicos.Venda = null;
            //   academicos.
            //}

            //Editar(venda);

           // _vendaRepository.Commit();

           // throw new MensagemException(EnumStatusCode.Informativa, "Atenção seu número de vagas anterior foi alterado.");
            
        }

    }
}
