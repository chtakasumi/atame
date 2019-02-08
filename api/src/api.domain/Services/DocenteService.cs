using api.domain.Entity;
using api.domain.Interfaces;
using api.domain.Services.Commons;
using api.libs;
using System.Collections.Generic;

namespace api.domain.Services
{
    public class DocenteService
    {

        private readonly IDocenteRepository _docenteRepository;

        public DocenteService(IDocenteRepository docente)
        {
            _docenteRepository = docente;
        }
        
        public string ModelSerializale()
        {  
            return Json.ToJson(new Docente());                
        }

        public IEnumerable<Docente> Listar(Docente docente)
        {
            return _docenteRepository.Listar(docente);
        }

        public Docente Cadastrar(Docente docente)
        {
            ValidarModelo(docente);
            return _docenteRepository.Inserir(docente);
        }       

        public void Editar(Docente docente)
        {
            ValidarModelo(docente);
            _docenteRepository.Atualizar(docente);
        }

        public void Excluir(int id)
        {
            if (id==0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir o docente. Docente não localizado");
            }

            var docente = _docenteRepository.PesquisarPorId(id);

            if (docente == null || docente.Id==0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir o docente. Docente não localizado");
            }

            _docenteRepository.Excluir(docente);
        }


        private void ValidarModelo(Docente docente)
        {

            if (string.IsNullOrEmpty(docente.Nome))
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Nome do docente não informado");
            }
            if (string.IsNullOrEmpty(docente.Formacao))
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Formação do docente não informado");
            }
         }
    }    
}
