using api.domain.Entity;
using api.domain.Interfaces;
using api.domain.Services.Commons;
using api.libs;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace api.domain.Services
{
    public class GeradorRelatorioService
    {
        private readonly IGeradorRelatorioRepository _geradorRelatorioRepository;
        private readonly IPerfilRepository _perfilRepository;


        public GeradorRelatorioService(IGeradorRelatorioRepository geradorRelatorio, IPerfilRepository perfilRepository)
        {
            _geradorRelatorioRepository = geradorRelatorio;
            _perfilRepository = perfilRepository;
        }
        
        public string ModelSerializale()
        {  
            return Json.ToJson(new GeradorRelatorio());                
        }

        public GeradorRelatorio PesquisarPorId(int id)
        {
            return _geradorRelatorioRepository.PesquisarPorId(id);
        }

        public IEnumerable<GeradorRelatorio> Listar(GeradorRelatorioDTO dto)
        {
            return _geradorRelatorioRepository.Listar(dto);
        }

        public GeradorRelatorio Cadastrar(GeradorRelatorio geradorRelatorio)
        {
            ValidarModelo(geradorRelatorio);

            var gerador = _geradorRelatorioRepository.Inserir(geradorRelatorio);

            var total = _perfilRepository.BuscarTodos().Count();

            Perfil perfil = new Perfil {
                Modulo = Enum.GetName(typeof(EnumModulo), EnumModulo.Global),
                Funcionalidade = geradorRelatorio.Alias,
                Descricao = geradorRelatorio.Nome,
                Menu = Enum.GetName(typeof(EnumMenu), EnumMenu.Relatorio),
                Order = total+1
            };

            _perfilRepository.Inserir(perfil);


            return gerador;
        }

        public DbDataReader ExecutarQuery(string sql)
        {
            return _geradorRelatorioRepository.ExecutarQuery(sql);
        }

        public IEnumerable<GeradorRelatorio> Lov(string nome=null)
        {
            var filtro = new GeradorRelatorioDTO();
            filtro.Nome = (nome != "null")? nome : null;

            return _geradorRelatorioRepository.Listar(filtro);
        }

        public void Editar(GeradorRelatorio geradorRelatorio)
        {
            ValidarModelo(geradorRelatorio);

            var geradorAnterior = _geradorRelatorioRepository.PesquisarPorId(geradorRelatorio.Id);
            var perfilAntigo = _perfilRepository.Pesquisar(x => x.Funcionalidade== geradorAnterior.Alias).SingleOrDefault();

            _geradorRelatorioRepository.EntityStateDetached(geradorAnterior);
            _geradorRelatorioRepository.Atualizar(geradorRelatorio);

            _perfilRepository.EntityStateDetached(perfilAntigo);
            perfilAntigo.Funcionalidade = geradorRelatorio.Alias;
            perfilAntigo.Descricao = geradorRelatorio.Nome;

            _perfilRepository.Atualizar(perfilAntigo);

        }

        public void Excluir(int id)
        {

            if (id == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir o GeradorRelatorio. GeradorRelatorio não localizado");
            }

            var geradorRelatorio = _geradorRelatorioRepository.PesquisarPorId(id);
            

            if (geradorRelatorio == null || geradorRelatorio.Id == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir o GeradorRelatorio. GeradorRelatorio não localizado");
            }

            var perfil = _perfilRepository.Pesquisar(x => x.Funcionalidade == geradorRelatorio.Alias).SingleOrDefault();
            _perfilRepository.Excluir(perfil);
            _geradorRelatorioRepository.Excluir(geradorRelatorio);
        }

        private void ValidarModelo(GeradorRelatorio geradorRelatorio)
        {
          
            if (string.IsNullOrEmpty(geradorRelatorio.Nome))
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Nome do Gerador de Relatório não informado");
            }

            if (string.IsNullOrEmpty(geradorRelatorio.Alias))
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Alias do Gerador de Relatório não informado");
            }
           
            if (string.IsNullOrEmpty(geradorRelatorio.Descricao))
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Descrição do Gerador de Relatório não informado");
            }

            if (string.IsNullOrEmpty(geradorRelatorio.Query))
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Query do Gerador de Relatório não informado");
            }

            if (geradorRelatorio.Query.Length < "SELECT * FROM A".Length)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Query Inválida.");
            }

            if (geradorRelatorio.Query.Substring(0,6).ToUpper()!="SELECT")
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Query Inválida. Apenas query de consultas poderão ser executadas.");
            }

        }
    }    
}
