using System;
using System.Collections.Generic;
using System.Text;
using api.domain.Entity;
using api.libs;

namespace api.domain.Services.Commons
{
    public class DadosChave
    {
        private int id;
        private string login;
        private string ativo;      
        private DateTime now;

        public DadosChave(int id, string login, string ativo, ICollection<GrupoUsuario> gruposUsuarios, DateTime now)
        {
            this.id = id;
            this.login = login;
            this.ativo = ativo;
            this.gruposUsuarios = gruposUsuarios;
            this.now = now;
        }

        public string Chave { get { return Seguranca.Critptografar(string.Concat(this.id, '|', this.login, '|', this.ativo)); } }

        public ICollection<GrupoUsuario> gruposUsuarios { get; set; }
    }
}
