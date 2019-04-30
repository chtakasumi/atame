var app = angular.module('main');
app.factory("autenticacaoService", ['consumerService', '$window', '$location',
function (consumerService, $window, $location) {
       
   var key = "Chave";

    var _autenticate = function () {       
        return $window.localStorage.getItem(key);
    };

    var _deslogar = function () {        
        $window.localStorage.removeItem(key);
        $location.path("/login");
    }

    var _autenticar = function (login, senha, func) {

        var parm = { "login": login, "senha": senha };

        consumerService.post("usuario/Autenticar", parm,
        function (dados) {              
             $window.localStorage.setItem(key, dados.chave);             
             return func(dados.chave);              
        });
    };

    return {
        getPermission: _autenticate,
        deslogar: _deslogar,
        autenticar: _autenticar
    };

}]);

app.factory("cursoService", ['consumerService', '$rootScope', 'globalService', function (consumerService, $rootScope, globalService) {
    var baseUrl = "curso";
    return globalService.extendsAbstractServices(baseUrl, consumerService, $rootScope);      
}]);

app.factory("cursoDocenteService", ['consumerService', '$rootScope', 'globalService', function (consumerService, $rootScope, globalService) {
    var baseUrl = "curso/docente";
    return globalService.extendsAbstractServices(baseUrl, consumerService, $rootScope); 
}]);

app.factory("cursoConteudoProgramaticoService", ['consumerService', '$rootScope', 'globalService', function (consumerService, $rootScope, globalService) {
    var baseUrl = "curso/conteudo";
    return globalService.extendsAbstractServices(baseUrl, consumerService, $rootScope);    
}]);

app.factory("turmaService", ['consumerService', '$rootScope','globalService', function (consumerService, $rootScope, globalService) {
    var baseUrl = "turma";
    return globalService.extendsAbstractServices(baseUrl, consumerService, $rootScope);
}]);

app.factory("turmaDocenteService", ['consumerService', '$rootScope', 'globalService', function (consumerService, $rootScope, globalService) {
    var baseUrl = "turma/docente";
    return globalService.extendsAbstractServices(baseUrl, consumerService, $rootScope);
}]);

app.factory("turmaConteudoProgramaticoService", ['consumerService', '$rootScope', 'globalService', function (consumerService, $rootScope, globalService) {
    var baseUrl = "turma/conteudo";
    return globalService.extendsAbstractServices(baseUrl, consumerService, $rootScope);
}]);

app.factory("tipoCursoService", ['consumerService', '$rootScope', 'globalService', function (consumerService, $rootScope, globalService) {
    var baseUrl = 'tipoCurso';
    return globalService.extendsAbstractServices(baseUrl, consumerService, $rootScope); 
}]); 

app.factory("docenteService", ['consumerService', '$rootScope', 'globalService', function (consumerService, $rootScope, globalService) {
    var baseUrl = "docente";
    return globalService.extendsAbstractServices(baseUrl, consumerService, $rootScope); 
}]);

app.factory("conteudoProgramaticoService", ['consumerService', '$rootScope', 'globalService', function (consumerService, $rootScope, globalService) {
    var baseUrl = "conteudoProgramatico";
    return globalService.extendsAbstractServices(baseUrl, consumerService, $rootScope);  
}]);

app.factory("vendedorService", ['consumerService', '$rootScope','globalService', function (consumerService, $rootScope, globalService) {
    var baseUrl = "vendedor";
    return globalService.extendsAbstractServices(baseUrl, consumerService, $rootScope);
}]);

app.factory("ufService", ['consumerService', '$rootScope', 'globalService', function (consumerService, $rootScope, globalService) {
    var baseUrl = "uf";
    return globalService.extendsAbstractServices(baseUrl, consumerService, $rootScope);
}]);

app.factory("municipioService", ['consumerService', '$rootScope', 'globalService', function (consumerService, $rootScope, globalService) {
    var baseUrl = "municipio";
    return globalService.extendsAbstractServices(baseUrl, consumerService, $rootScope);
}]);

app.factory("clienteService", ['consumerService', '$rootScope', 'globalService', function (consumerService, $rootScope, globalService) {
    var baseUrl = "cliente";
    return globalService.extendsAbstractServices(baseUrl, consumerService, $rootScope);
}]);

app.factory("empresaService", ['consumerService', '$rootScope', 'globalService', function (consumerService, $rootScope, globalService) {
    var baseUrl = "empresa";
    return globalService.extendsAbstractServices(baseUrl, consumerService, $rootScope);
}]);


app.factory("vendaService", ['consumerService', '$rootScope', 'globalService', function (consumerService, $rootScope, globalService) {
    var baseUrl = "venda";
    return globalService.extendsAbstractServices(baseUrl, consumerService, $rootScope);
}]);

app.factory("vendaClienteService", ['consumerService', '$rootScope', 'globalService', function (consumerService, $rootScope, globalService) {
    var baseUrl = "venda/academico";
    return globalService.extendsAbstractServices(baseUrl, consumerService, $rootScope);
}]);

app.factory("prospeccaoService", ['consumerService', '$rootScope', 'globalService', function (consumerService, $rootScope, globalService) {
    var baseUrl = "prospeccao";
    return globalService.extendsAbstractServices(baseUrl, consumerService, $rootScope);
}]);

app.factory("prospeccaoInteresseService", ['consumerService', '$rootScope', 'globalService', function (consumerService, $rootScope, globalService) {
    var baseUrl = "prospeccao/interesse";
    return globalService.extendsAbstractServices(baseUrl, consumerService, $rootScope);
}]);

app.factory("parametroService", ['consumerService', '$rootScope', 'globalService', function (consumerService, $rootScope, globalService) {
    var baseUrl = "parametro";
    var api = globalService.extendsAbstractServices(baseUrl, consumerService, $rootScope);
    api.listarChave = function (callback) {
        consumerService.get(baseUrl + "/listar-parametro", function (modelo) {
            callback(JSON.parse(JSON.parse(modelo)));
        })
    }

    return api;
}]);

app.factory("faturamentoService", ['consumerService', '$rootScope', 'globalService', function (consumerService, $rootScope, globalService) {
    var baseUrl = "faturamento";
    return globalService.extendsAbstractServices(baseUrl, consumerService, $rootScope);

}]);

app.factory("usuarioService", ['consumerService', '$rootScope', 'globalService', function (consumerService, $rootScope, globalService) {
    var baseUrl = "usuario";
    return globalService.extendsAbstractServices(baseUrl, consumerService, $rootScope);
}]);

app.factory("comissaoService", ['consumerService', '$rootScope', 'globalService', function (consumerService, $rootScope, globalService) {
    var baseUrl = "comissao";
    var api = globalService.extendsAbstractServices(baseUrl, consumerService, $rootScope);
    api.listaStatus = function (callback) {
        consumerService.get(baseUrl + "/listar-status", function (modelo) {
            callback(JSON.parse(JSON.parse(modelo)));
        })
    }
    return api;
}]);

app.factory("bancoService", ['consumerService', '$rootScope', 'globalService', function (consumerService, $rootScope, globalService) {
    var baseUrl = "banco";
    return globalService.extendsAbstractServices(baseUrl, consumerService, $rootScope);
}]);

app.factory("descontoService", ['consumerService', '$rootScope', 'globalService', function (consumerService, $rootScope, globalService) {
    var baseUrl = "desconto";
    return globalService.extendsAbstractServices(baseUrl, consumerService, $rootScope);
}]);



