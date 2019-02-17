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
    public class TurmaService
    {

        private readonly ITurmaRepository _TurmaRepository;
        private readonly IDocenteRepository _docenteRepository;
        private readonly IConteudoProgramaticoRepository _conteudoProgramaticoRepository;
        
        public TurmaService(ITurmaRepository Turma, IDocenteRepository docente, IConteudoProgramaticoRepository conteudoProgramatico)
        {
            _TurmaRepository = Turma;
            _docenteRepository = docente;
            _conteudoProgramaticoRepository = conteudoProgramatico;
        }

        public string ModelTurmaDocenteSerializale()
        {
            return Json.ToJson(new TurmaDocente());
        }

        public string ModelTurmaConteudoProgramaticoSerializale()
        {
            return Json.ToJson(new TurmaConteudoProgramatico());
        }

        public string ModelSerializale()
        {  
            return Json.ToJson(new Turma());                
        }

        public IEnumerable<Turma> Listar(TurmaDTO TurmaVo)
        {
            return _TurmaRepository.Listar(TurmaVo);
        }

        public Turma Cadastrar(Turma Turma)
        {
            ValidarModelo(Turma);

            //grava os vinculos de docentes e conteudos programaticos
            foreach (var TurmaDoc in Turma.Docentes) {
                TurmaDoc.Docente = null;
            }

            foreach (var conteudoProg in Turma.ConteudosProgramaticos)
            {
                conteudoProg.ConteudoProgramatico = null;
            }

            return _TurmaRepository.Inserir(Turma);
        }

        public IEnumerable<Turma> Lov(string descricao)
        {
            var filtro = new TurmaDTO();
            filtro.Identificacao = (descricao != "null") ? descricao : null;

            return _TurmaRepository.Listar(filtro);
        }

        public void Editar(Turma Turma)
        {           
            ValidarModelo(Turma);
            _TurmaRepository.Atualizar(Turma);
        }

        public void Excluir(int id)
        {
            if (id==0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir a Turma. Turma não localizado");
            }

            var Turma = _TurmaRepository.PesquisarPorId(id);

            if (Turma == null || Turma.Id==0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir a Turma. Turma não localizado");
            }

            _TurmaRepository.Excluir(Turma);
        }      

        public void ExcluirVinculoDocente(int id)
        {
            if (id == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir o vinculo com o docente.");
            }

            TurmaDocente cd = _TurmaRepository.PesquisarVinculoDocente(id);
            cd.Turma = null;
            cd.Docente = null;

            if (cd == null || cd.Id == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir o vinculo com o docente. Vinculo não localizado");
            }

            _TurmaRepository.ExcluirVinculoDocente(cd);
        }
         
        public void ExcluirVinculoConteudoProgramatico(int id)
        {
            if (id == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir o vinculo com o conteúdo.");
            }

            TurmaConteudoProgramatico ctp = _TurmaRepository.PesquisarVinculoConteudoProgramatico(id);

            if (ctp == null || ctp.Id == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir o vinculo com o conteúdo. Vinculo não localizado");
            }

            _TurmaRepository.ExcluirVinculoConteudoProgramatico(ctp);
        }

        private void ValidarModelo(Turma Turma)
        {

            if (string.IsNullOrEmpty(Turma.Identificacao))
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Indenticação da Turma não informado");
            }
            if (Turma.CursoId == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Selecione o Curso");
            }

            if (Turma.Preco == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Preço da Turma não informado");
            }

            if (Turma.Inicio==null)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Inicio Previsto não informado");
            }

            if (Turma.Fim == null)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Fim Previsto não informado");
            }

            Turma.Curso = null;
        }

        public TurmaConteudoProgramatico VincularConteudoProgramatico(TurmaConteudoProgramatico entidade)
        {

            TurmaConteudoProgramatico conteudoProgramatico =  _TurmaRepository.VincularConteudoProgramatico(entidade);
            conteudoProgramatico.ConteudoProgramatico = _conteudoProgramaticoRepository.PesquisarPorId(conteudoProgramatico.ConteudoProgramaticoId);

            return conteudoProgramatico;
        }

        public TurmaDocente VincularDocente(TurmaDocente entidade)
        {
            var docente = _TurmaRepository.VincularDocente(entidade);
            docente.Docente = _docenteRepository.PesquisarPorId(docente.DocenteId);
            return docente;
        }
    }    
}
