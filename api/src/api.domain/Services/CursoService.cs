using api.domain.Entity;
using api.domain.Interfaces;
using api.domain.Services.DTO;
using api.libs;
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
