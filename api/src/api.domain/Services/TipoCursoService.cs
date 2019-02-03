using api.domain.Entity;
using api.domain.Interfaces;
using api.domain.Services.Commons;
using api.libs;
using System.Collections.Generic;

namespace api.domain.Services
{
    public class TipoCursoService
    {

        private readonly ITipoCursoRepository _tipoCursoRepository;

        public TipoCursoService(ITipoCursoRepository tipoCurso)
        {
            _tipoCursoRepository = tipoCurso;
        }
        
        public string ModelSerializale()
        {  
            return Json.ToJson(new TipoCurso());                
        }

        public IEnumerable<TipoCurso> Listar(TipoCurso tipoCursoVo)
        {
            return _tipoCursoRepository.Listar(tipoCursoVo);
        }

        public TipoCurso Cadastrar(TipoCurso tipoCurso)
        {
            ValidarModelo(tipoCurso);

            return _tipoCursoRepository.Inserir(tipoCurso);
        }

        public void Editar(TipoCurso tipoCurso)
        {
            ValidarModelo(tipoCurso);
            _tipoCursoRepository.Atualizar(tipoCurso);
        }

        public void Excluir(int id)
        {

            if (id == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir o tipo curso. Tipo Curso não localizado");
            }

            var tipoCurso = _tipoCursoRepository.PesquisarPorId(id);
            

            if (tipoCurso == null || tipoCurso.Id == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir o tipo curso. Tipo Curso não localizado");
            }

            _tipoCursoRepository.Excluir(tipoCurso);
        }

        private void ValidarModelo(TipoCurso tipoCurso)
        {

            if (string.IsNullOrEmpty(tipoCurso.Descricao))
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Descrição do tipo curso não informado");
            }   
        }
    }    
}
