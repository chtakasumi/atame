using api.domain.Entity;
using api.domain.Interfaces;
using api.domain.Services.Commons;
using api.libs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace api.domain.Services
{
    public class ParametroService
    {

        private readonly IParametroRepository _parametroRepository;

        public ParametroService(IParametroRepository parametro)
        {
            _parametroRepository = parametro;
        }
        
        public string ModelSerializale()
        {  
            return Json.ToJson(new Parametro());                
        }

        public IEnumerable<Parametro> Listar(Parametro parametroVo)
        {
            return _parametroRepository.Listar(parametroVo);
        }

        public Parametro Cadastrar(Parametro Parametro)
        {
            ValidarModelo(Parametro);

            return _parametroRepository.Inserir(Parametro);
        }

        public IEnumerable<Parametro> Lov(string chave = null)
        {
            var filtro = new Parametro();
            filtro.Chave = (chave != "null")? chave : null;

            return _parametroRepository.Listar(filtro);
        }

        public string Chaves()
        {
            //var lista = new List<Lista>();
            var listaEnum = Enum.GetNames(typeof(EnumParametros));
            return Json.ToJson(listaEnum);
        }

        public void Editar(Parametro parametro)
        {
            ValidarModelo(parametro);
            _parametroRepository.Atualizar(parametro);
        }

        public void Excluir(int id)
        {

            if (id == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir o Parametro. Parametro não localizado");
            }

            var Parametro = _parametroRepository.PesquisarPorId(id);
            

            if (Parametro == null || Parametro.Id == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir o Parametro. Parametro não localizado");
            }

            _parametroRepository.Excluir(Parametro);
        }

        private void ValidarModelo(Parametro Parametro)
        {
          
            if (string.IsNullOrEmpty(Parametro.Chave))
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Chave do Parametro não informado");
            }

            if (string.IsNullOrEmpty(Parametro.valor))
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Valor Obrigatório");
            }
        }
    }    
}
