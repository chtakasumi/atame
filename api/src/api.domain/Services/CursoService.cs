using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using api.domain.Entity;
using api.domain.Interfaces;
using api.domain.Services.Commons;
using api.domain.Services.DTO;
using api.libs;

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

        public IEnumerable<Curso> Listar(CursoListarVo cursoVo)
        {
            return _cursoRepository.Listar(cursoVo);
        }

        public Curso Cadastrar(Curso curso)
        {
            return _cursoRepository.Inserir(curso);
        }

        public void Editar(Curso curso)
        {
            _cursoRepository.Atualizar(curso);
        }

        public void Excluir(int id)
        {
            var curso = _cursoRepository.PesquisarPorId(id);
            _cursoRepository.Excluir(curso);
        }
    }    
}
