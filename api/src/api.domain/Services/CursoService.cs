using api.domain.Entity;
using api.domain.Interfaces;
using api.domain.Services.Commons;
using api.domain.Services.DTO;
using api.libs;
using System;
using System.Collections.Generic;

namespace api.domain.Services
{
    public class CursoService
    {

        private readonly ICursoRepository _cursoRepository;

        public CursoService(ICursoRepository curso)
        {
            _cursoRepository = curso;
        }
        
        public string ModelSerializale()
        {  
            return Json.ToJson(new Curso());                
        }

        public IEnumerable<Curso> Listar(CursoDTO cursoVo)
        {
            return _cursoRepository.Listar(cursoVo);
        }

        public Curso Cadastrar(Curso curso)
        {
            ValidarModelo(curso);
            return _cursoRepository.Inserir(curso);
        }

        public IEnumerable<Curso> Lov(string descricao)
        {
            var filtro = new CursoDTO();
            filtro.Nome = (descricao != "null") ? descricao : null;

            return _cursoRepository.Listar(filtro);
        }


        public void Editar(Curso curso)
        {           
            ValidarModelo(curso);
            _cursoRepository.Atualizar(curso);
        }

        public void Excluir(int id)
        {
            if (id==0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir o curso. Curso não localizado");
            }

            var curso = _cursoRepository.PesquisarPorId(id);

            if (curso == null || curso.Id==0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir o curso. Curso não localizado");
            }

            _cursoRepository.Excluir(curso);
        }


        private void ValidarModelo(Curso curso)
        {

            if (string.IsNullOrEmpty(curso.Nome))
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Nome do curso não informado");
            }
            if (curso.TipoCursoId == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Selecione o tipo do curso");
            }

            if (curso.Preco == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "preço do curso não informado");
            }

            curso.TipoCurso = null;
        }
    }    
}
