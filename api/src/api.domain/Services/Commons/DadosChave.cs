using System;
using System.Collections.Generic;
using api.domain.Entity;
using api.libs;

namespace api.domain.Services.Commons
{
    public class DadosChave
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public DateTime Now { get; set; }
        private string Ativo;

        public DadosChave(int id, string login, string ativo, ICollection<GrupoUsuario> gruposUsuarios, DateTime now)
        {
            Perfils = new List<Perfil>();

            this.Id = id;
            this.Login = login;
            this.Ativo = ativo;           
            this.Now = now;

            foreach (var gu in gruposUsuarios)
            {
                foreach (var perfilGrupo in gu.Grupo.PerfisGrupos)
                {
                    Perfils.Add(perfilGrupo.Perfil);
                }               
            }
        }

        public string Chave { get { return Seguranca.Critptografar(string.Concat(this.Id, '|', this.Login, '|', this.Ativo)); } }

        public IList<Perfil> Perfils { get; set; }
    }
}
