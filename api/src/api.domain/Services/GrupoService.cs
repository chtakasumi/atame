using api.domain.Entity;
using api.domain.Interfaces;
using api.domain.Services.Commons;
using api.libs;
using System.Collections.Generic;

namespace api.domain.Services
{
    public class GrupoService
    {

        private readonly IGrupoRepository _grupoRepository;

        public GrupoService(IGrupoRepository Grupo)
        {
            _grupoRepository = Grupo;
        }
        
        public string ModelSerializale()
        {
            var grupo = new Grupo();
            grupo.PermissoesNaoCedidas = _grupoRepository.ListarPerfil();
            return Json.ToJson(grupo);                
        }

        public IEnumerable<Grupo> Listar(Grupo GrupoVo)
        {
            var dados = _grupoRepository.Listar(GrupoVo);

            foreach (var data in dados) {
                data.PerfisGrupos = null;
            }

            return dados;
        }

        public Grupo Cadastrar(Grupo grupo)
        {
            ValidarModelo(grupo);
            grupo  = _grupoRepository.Inserir(grupo);
            Editar(grupo);
            return grupo;
        }

        public IEnumerable<Grupo> Lov(string nome=null)
        {
            var filtro = new Grupo();
            filtro.Descricao = (nome != "null")? nome : null;
            return _grupoRepository.Listar(filtro);
        }

        public void Editar(Grupo grupo)
        {
            ValidarModelo(grupo);

            //se for uma uatualização apenas remove
            if (grupo.Id > 0)
            {
                _grupoRepository.RemoverPerfilGrupos(grupo.Id.Value);
            }

            grupo.PerfisGrupos.Clear();
            foreach (var p in grupo.PermissoesCedidas)
            {
                grupo.PerfisGrupos.Add(new PerfilGrupo
                {
                    Grupo = grupo,
                    GrupoId = grupo.Id.Value,
                    Id = 0,
                    Perfil = null,
                    PerfilId = p.Id
                });
            }
            _grupoRepository.Atualizar(grupo);
        }

        public void Excluir(int id)
        {

            if (id == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir o Grupo. Grupo não localizado");
            }

            var Grupo = _grupoRepository.PesquisarPorId(id);
            

            if (Grupo == null || Grupo.Id == 0)
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Não foi possivel excluir o Grupo. Grupo não localizado");
            }

            _grupoRepository.Excluir(Grupo);
        }
               
        private void ValidarModelo(Grupo grupo)
        {          
            if (string.IsNullOrEmpty(grupo.Descricao))
            {
                throw new MensagemException(EnumStatusCode.RequisicaoInvalida, "Nome do Grupo não informado");
            }
        }
    }    
}
