﻿using System;
using System.Collections.Generic;
using System.Text;

namespace api.domain.Entity
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CpfCnpj { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public int UFId { get; set; }
        public UF UF { get; set; }
        public int MunicipioId { get; set; }
        public Municipio Municipio { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public string NomeCpfCnpj { get { return string.Format("{0} {1}", this.Nome, this.CpfCnpj); } }
    }
}
