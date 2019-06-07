using System;
using System.Collections.Generic;
using System.Text;

namespace api.domain.Entity
{
    public class TipoConta
    {
        public string Sigla { get; set; }
        public string Descricao { get; set; }
        

        public TipoConta(string sigla, string descricao)
        {
            Sigla = sigla;
            Descricao = descricao;
        }

        public TipoConta()
        {
        }


        public List<TipoConta> GetAll()
        {
            return new List<TipoConta>
            {
                new TipoConta("CC","Conta Corrente"),
                new TipoConta("CP","Conta Poupança"),
                new TipoConta("CS","Conta Salárioa")
            };
        }
    }
}
