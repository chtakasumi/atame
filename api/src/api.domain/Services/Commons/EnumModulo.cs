using System;
using System.Collections.Generic;
using System.Text;

namespace api.domain.Services.Commons
{
    public enum EnumModulo
    {       
        Vendas,
        Academico,
        Financeiro,
        Global //este modulo esta para todos os modulos
    }

    public enum EnumMenu
    {
        Configuracao,
        Cadastro,
        Operacional,
        Relatorio
    }
}
