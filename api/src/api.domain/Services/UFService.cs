using api.domain.Entity;
using api.domain.Interfaces;
using api.domain.Services.Commons;
using api.libs;
using System.Collections.Generic;

namespace api.domain.Services
{
    public class UFService
    {

        private readonly IUFRepository _UFRepository;

        public UFService(IUFRepository uf)
        {
            _UFRepository = uf;
        }
        
        public string ModelSerializale()
        {  
            return Json.ToJson(new UF());                
        }

        public IEnumerable<UF> Listar(UF uf)
        {
            return _UFRepository.Listar(uf);
        }

        public UF Cadastrar(UF uf)
        {
            ValidarModelo(uf);

            return _UFRepository.Inserir(uf);
        }

        public IEnumerable<UF> Lov(string nome=null)
        {
            var filtro = new UF();
            filtro.Nome = (nome != "null")? nome : null;

            return _UFRepository.Listar(filtro);
        }

        public void Editar(UF uf)
        {
            ValidarModelo(uf);
            _UFRepository.Atualizar(uf);
        }

        public void Excluir(int id)
        {

            if (id == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir o UF. UF não localizado");
            }

            var uf = _UFRepository.PesquisarPorId(id);
            

            if (uf == null || uf.Id == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir o UF. UF não localizado");
            }

            _UFRepository.Excluir(uf);
        }

        private void ValidarModelo(UF UF)
        {
          
            if (string.IsNullOrEmpty(UF.Nome))
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Nome do UF não informado");
            }

            if (string.IsNullOrEmpty(UF.Sigla))
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Sigla não informado");
            }
        }
    }    
}
