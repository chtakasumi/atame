using api.domain.Entity;
using api.domain.Interfaces;
using api.domain.Services.Commons;
using api.domain.Services.DTO;
using api.libs;
using System;
using System.Collections.Generic;

namespace api.domain.Services
{
    public class ConteudoProgramaticoService
    {

        private readonly IConteudoProgramaticoRepository _conteudoProgramaticoRepository;

        public ConteudoProgramaticoService(IConteudoProgramaticoRepository conteudoProgramatico)
        {
            _conteudoProgramaticoRepository = conteudoProgramatico;
        }
        
        public string ModelSerializale()
        {  
            return Json.ToJson(new ConteudoProgramatico());                
        }

        public IEnumerable<ConteudoProgramatico> Lov(string descricao)
        {
            var filtro = new ConteudoProgramatico();
            filtro.Identificacao = (descricao != "null") ? descricao : null;

            return _conteudoProgramaticoRepository.Listar(filtro);
        }

        public IEnumerable<ConteudoProgramatico> Listar(ConteudoProgramatico ConteudoProgramatico)
        {
            return _conteudoProgramaticoRepository.Listar(ConteudoProgramatico);
        }

        public ConteudoProgramatico Cadastrar(ConteudoProgramatico conteudoProgramatico)
        {
            ValidarModelo(conteudoProgramatico);
            return _conteudoProgramaticoRepository.Inserir(conteudoProgramatico);
        }       

        public void Editar(ConteudoProgramatico ConteudoProgramatico)
        {
            ValidarModelo(ConteudoProgramatico);
            _conteudoProgramaticoRepository.Atualizar(ConteudoProgramatico);
        }

        public void Excluir(int id)
        {
            if (id==0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir o ConteudoProgramatico. ConteudoProgramatico não localizado");
            }

            var ConteudoProgramatico = _conteudoProgramaticoRepository.PesquisarPorId(id);

            if (ConteudoProgramatico == null || ConteudoProgramatico.Id==0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir o ConteudoProgramatico. ConteudoProgramatico não localizado");
            }

            _conteudoProgramaticoRepository.Excluir(ConteudoProgramatico);
        }


        private void ValidarModelo(ConteudoProgramatico ConteudoProgramatico)
        {

            if (string.IsNullOrEmpty(ConteudoProgramatico.Identificacao))
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Nome do ConteúdoProgramatico não informado");
            }                    
        }
    }    
}
