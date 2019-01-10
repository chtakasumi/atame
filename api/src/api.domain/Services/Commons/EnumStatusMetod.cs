using System;
using System.Collections.Generic;
using System.Text;

namespace api.domain.Services.Commons
{
    /// <summary>
    /// Caso queiro outros codigos colocar aqui
    /// Veja a documentação abaixo para entender melhor os codigos HTTP
    /// https://pt.wikipedia.org/wiki/Lista_de_códigos_de_estado_HTTP
    /// </summary>
    enum EnumStatusCode
    {

        
        //Informativa
        Informativa = 100, //caso queira dar um informação para o cliente, porem deverá conseguir processar a requisição


        //sucesso
        Sucesso = 200, // outros casos de sucesso
        Criado =201,  //quando cadastra no banco de dados
        NenhumConteudo=204, //ao atualizar
       


        //Erro do cliente
        RequisicaoInvalida = 400, //quando o cliente não passar um dados valido
        NaoAutorizado =401, //quando o cliente não tiver logado
        Proibido = 403, //quando o cliente não tiver permissão
        NaoEncontrado = 404, //quando pesquisou e não econtrou nada       

        //Erro no servidor
        Erro =500

    }
}
