using api.domain.Entity;
using api.domain.Interfaces;
using api.domain.Services.Commons;
using api.libs;
using System.Collections.Generic;

namespace api.domain.Services
{
    public class MunicipioService
    {

        private readonly IMunicipioRepository _MunicipioRepository;

        public MunicipioService(IMunicipioRepository municipio)
        {
            _MunicipioRepository = municipio;
        }
        
        public string ModelSerializale()
        {  
            return Json.ToJson(new Municipio());                
        }

        public IEnumerable<Municipio> Listar(Municipio municipioVo)
        {
            return _MunicipioRepository.Listar(municipioVo);
        }

        public Municipio Cadastrar(Municipio municipio)
        {
            ValidarModelo(municipio);

            return _MunicipioRepository.Inserir(municipio);
        }

        public IEnumerable<Municipio> Lov(string nome=null)
        {
            var filtro = new Municipio();
            filtro.Nome = (nome != "null")? nome : null;

            return _MunicipioRepository.Listar(filtro);
        }

        public void Editar(Municipio municipio)
        {
            ValidarModelo(municipio);
            _MunicipioRepository.Atualizar(municipio);
        }

        public void Excluir(int id)
        {

            if (id == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir o Municipio. Municipio não localizado");
            }

            var Municipio = _MunicipioRepository.PesquisarPorId(id);
            

            if (Municipio == null || Municipio.Id == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir o Municipio. Municipio não localizado");
            }

            _MunicipioRepository.Excluir(Municipio);
        }

        private void ValidarModelo(Municipio municipio)
        {
          
            if (string.IsNullOrEmpty(municipio.Nome))
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Nome do Municipio não informado");
            }

            if (municipio.UFId==0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "UF não informada");
            }

            municipio.UF = null;

        }
    }    
}
