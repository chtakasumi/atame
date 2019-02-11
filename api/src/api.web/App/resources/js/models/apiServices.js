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

app.factory("cursoService", ['consumerService', '$rootScope', function (consumerService, $rootScope) {
    var baseUrl = "curso";
   return extendsAbstractServices(baseUrl, consumerService, $rootScope); 
}]);

app.factory("tipoCursoService", ['consumerService', '$rootScope', function (consumerService, $rootScope) {
    var baseUrl = 'tipoCurso';
    return extendsAbstractServices(baseUrl, consumerService, $rootScope); 
}]); 

app.factory("docenteService", ['consumerService', '$rootScope', function (consumerService, $rootScope) {
    var baseUrl = "docente";
    return extendsAbstractServices(baseUrl, consumerService, $rootScope); 
}]);

app.factory("conteudoProgramaticoService", ['consumerService', '$rootScope', function (consumerService, $rootScope) {
    var baseUrl = "conteudoProgramatico";
    return extendsAbstractServices(baseUrl, consumerService, $rootScope);  
}]);
app.factory("vendedorService", ['consumerService', '$rootScope', function (consumerService, $rootScope) {
    var baseUrl = "vendedor";
    return extendsAbstractServices(baseUrl, consumerService, $rootScope);
}]);

function extendsAbstractServices(baseUrl, consumerService, $rootScope){

    var _lov = function (string, callBack) {
        var param = (string) ? string : null;
        consumerService.get(baseUrl + "/" + param, function (data) {
            callBack(data);
        })
    };

    var _model = function (callBack) {
        var url = baseUrl + "/model";
        $rootScope.Loading(true, "GET: " + url);
        consumerService.get(url, function (modelo) {
            callBack(JSON.parse(JSON.parse(modelo)));
        });
    };

    var _listar = function (filtro, callBack) {
        var url = baseUrl + "/listar";
        $rootScope.Loading(true, "POST: " + url);
        consumerService.post(url, filtro, function (data) {
            callBack(data);
        })
    };

    var _salvar = function (entidade, callBack) {

        if (entidade.id > 0) {
            $rootScope.Loading(true, "PUT: " + baseUrl);
            consumerService.put(baseUrl, entidade, function (data) {
                callBack(data);
            });
        } else {
            $rootScope.Loading(true, "POST: " + baseUrl);
            consumerService.post(baseUrl, entidade, function (data) {
                callBack(data);
            });
        }
    };

    var _excluir = function (id, callBack) {
        var url = baseUrl + "?id=" + id;
        $rootScope.Loading(true, "DELETE: " + url);
        consumerService.delete(url, function () {
            callBack();
        });
    };

    return {
        model: _model,
        lov: _lov,
        listar: _listar,
        salvar: _salvar,
        excluir: _excluir
    };
}

