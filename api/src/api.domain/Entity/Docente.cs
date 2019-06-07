using System;
using System.Collections.Generic;
using System.Text;

namespace api.domain.Entity
{
    public class Docente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Formacao { get; set; }
        public string Foto { get; set; } //mapear como longVarchar
        public DateTime? Nascimento { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }

        public int? UfExpedicaoId { get; set; }
        public UF UfExpedicao { get; set; }

        public string OrgaoExpedicaoSiglaDescricao { get; set; }
        public OrgaoExpeditor orgaoExpedicao { get; set; }
       
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }

        public int? BancoId { get; set; }
        public Banco Banco { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }

        public string TipoContaDescricao { get; set; }
        public TipoConta TipoConta { get; set; }

        public string Titularizacao { get; set; }



    }
}
