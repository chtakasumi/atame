using api.domain.Entity;
using api.domain.Interfaces;
using api.domain.Services.Commons;
using api.domain.Services.DTO;
using api.libs;
using System.Collections.Generic;

namespace api.domain.Services
{
    public class ProspeccaoService
    {
        private readonly IProspeccaoRepository _prospeccaoRepository;

        public ProspeccaoService(IProspeccaoRepository prospeccao)
        {
            _prospeccaoRepository = prospeccao;
        }

        public string ModelSerializale()
        {
            return Json.ToJson(new Prospeccao());
        }

        public IEnumerable<Prospeccao> Listar(ProspeccaoDTO prospeccaoVo)
        {
            return _prospeccaoRepository.Listar(prospeccaoVo);
        }

        public Prospeccao Cadastrar(Prospeccao Prospeccao)
        {
            ValidarModelo(Prospeccao);
            
            //isso fala para o  não criar um novo cliente.
            foreach (var item in Prospeccao.Interesses)
            {
                item.Id = 0;
                item.Curso = null;
            }

            return _prospeccaoRepository.Inserir(Prospeccao);
        }
       
        public IEnumerable<Prospeccao> Lov(int? Id = null)
        {
            var filtro = new ProspeccaoDTO();
            filtro.Id = (Id.HasValue) ? Id : null;

            return _prospeccaoRepository.Listar(filtro);
        }

        public void Editar(Prospeccao prospeccao)
        {
            ValidarModelo(prospeccao);
            _prospeccaoRepository.Atualizar(prospeccao);
        }

        public void Excluir(int id)
        {

            if (id == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir a Prospeccao. Prospeccao não localizado");
            }

            var Prospeccao = _prospeccaoRepository.PesquisarPorId(id);


            if (Prospeccao == null || Prospeccao.Id == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir a Prospeccao. Prospeccao não localizado");
            }

            _prospeccaoRepository.Excluir(Prospeccao);
        }

        private void ValidarModelo(Prospeccao Prospeccao)
        {
            if (Prospeccao.Data==null)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Data do Prospeccao não informado");
            }

            if (Prospeccao.VendedorId == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Vendedor não informado");
            }
            
            if (Prospeccao.Interesses.Count == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Informe ao menos um Curso de Interesse.");
            }

            Prospeccao.Vendedor = null;          
          
        }

        //********************************************************************************//
        //**************************Metodos dos interresses*******************************//
        //******************************************************************************//
        public string ModelinterresseSerializale()
        {
            return Json.ToJson(new ProspeccaoInteresse());
        }

        public void ExcluirVinculoInteresse(int id)
        {
            if (id == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir o vinculo com o acadêmico.");
            }

            ProspeccaoInteresse obj = _prospeccaoRepository.PesquisarVinculoInteresse(id);
            obj.Prospeccao = null;
            obj.Curso = null;

            if (obj == null || obj.Id == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir o vinculo com o acadêmico. Vinculo não localizado");
            }

            _prospeccaoRepository.ExcluirVinculoInteresse(obj);
        }

        public object VincularInteresse(ProspeccaoInteresse entidade)
        {
            ProspeccaoInteresse prospeccaoInteresse = _prospeccaoRepository.VincularInteresse(entidade);
            prospeccaoInteresse.Prospeccao = _prospeccaoRepository.PesquisarPorId(prospeccaoInteresse.ProspeccaoId);
            return prospeccaoInteresse;
        }

    }
}
