using api.domain.Entity;
using api.domain.Interfaces;
using api.domain.Services.Commons;
using api.domain.Services.DTO;
using api.libs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace api.domain.Services
{
    public class CursoService
    {

        private readonly ICursoRepository _cursoRepository;
        private readonly IDocenteRepository _docenteRepository;
        private readonly IConteudoProgramaticoRepository _conteudoProgramaticoRepository;
        
        public CursoService(ICursoRepository curso, IDocenteRepository docente, IConteudoProgramaticoRepository conteudoProgramatico)
        {
            _cursoRepository = curso;
            _docenteRepository = docente;
            _conteudoProgramaticoRepository = conteudoProgramatico;
        }

        public string ModelCursoDocenteSerializale()
        {
            return Json.ToJson(new CursoDocente());
        }

        public string ModelCursoConteudoProgramaticoSerializale()
        {
            return Json.ToJson(new CursoConteudoProgramatico());
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

            //grava os vinculos de docentes e conteudos programaticos
            foreach (var cursoDoc in curso.Docentes) {
                cursoDoc.Docente = null;
            }

            foreach (var conteudoProg in curso.ConteudosProgramaticos)
            {
                conteudoProg.ConteudoProgramatico = null;
            }

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

        public void ExcluirVinculoDocente(int id)
        {
            if (id == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir o vinculo com o docente.");
            }

            CursoDocente cd = _cursoRepository.PesquisarVinculoDocente(id);
            cd.Curso = null;
            cd.Docente = null;

            if (cd == null || cd.Id == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir o vinculo com o docente. Vinculo não localizado");
            }

            _cursoRepository.ExcluirVinculoDocente(cd);
        }
         
        public void ExcluirVinculoConteudoProgramatico(int id)
        {
            if (id == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir o vinculo com o conteúdo.");
            }

            CursoConteudoProgramatico ctp = _cursoRepository.PesquisarVinculoConteudoProgramatico(id);

            if (ctp == null || ctp.Id == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir o vinculo com o conteúdo. Vinculo não localizado");
            }

            _cursoRepository.ExcluirVinculoConteudoProgramatico(ctp);
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

        public CursoConteudoProgramatico VincularConteudoProgramatico(CursoConteudoProgramatico entidade)
        {

            CursoConteudoProgramatico conteudoProgramatico =  _cursoRepository.VincularConteudoProgramatico(entidade);
            conteudoProgramatico.ConteudoProgramatico = _conteudoProgramaticoRepository.PesquisarPorId(conteudoProgramatico.ConteudoProgramaticoId);

            return conteudoProgramatico;
        }

        public CursoDocente VincularDocente(CursoDocente entidade)
        {
            var docente = _cursoRepository.VincularDocente(entidade);
            docente.Docente = _docenteRepository.PesquisarPorId(docente.DocenteId);
            return docente;
        }
    }    
}
