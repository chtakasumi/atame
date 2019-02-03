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

app.factory("tipoCursoService", ['consumerService', function (consumerService) {

    var baseUrl = 'tipoCurso';

    var _model = function (callBack) {
        consumerService.get(baseUrl+"/model", function (modelo) {
            callBack(JSON.parse(JSON.parse(modelo)));
        });
    };

    var _listar = function (id, descricao, callBack) {
        var param = { Id: id, Descricao: descricao };
        consumerService.post(baseUrl+"/listar", param, function (data) {
            callBack(data);
        })
    };

    var _salvar = function (entidade, callBack) {
        if (entidade.id > 0) {
            consumerService.put(baseUrl, entidade, function (data) {
                callBack(data);
            });
        } else {
            consumerService.post(baseUrl, entidade, function (data) {
                callBack(data);
            });
        }
    };

    var _excluir = function (id, callBack) {
        consumerService.delete(baseUrl+"?id=" + id, function () {
            callBack();
        });
    };    

    return {
        model: _model,
        listar: _listar,
        salvar: _salvar,
        excluir: _excluir
    };

}]); 

app.factory("cursoService", ['consumerService', function (consumerService) {
    var baseUrl = "curso";

    var _model = function (callBack) {
        consumerService.get(baseUrl+"/model", function (modelo) {
            callBack(JSON.parse(JSON.parse(modelo)));
        });
    };

    var _listar = function (id, nome, tipoCursoId, callBack) {
        var param = { Id: id, Nome: nome, TipoCursoId: tipoCursoId };
        consumerService.post(baseUrl +"/listar", param, function (data) {
            callBack(data);
        })
    };

    var _salvar = function (entidade, callBack) {    
        if(entidade.id > 0){
            consumerService.put(baseUrl, entidade, function (data) {
                callBack(data);
            });
        } else {         
            consumerService.post(baseUrl, entidade, function (data) {  
                callBack(data);
            });
        }
    };

    var _excluir = function (id, callBack) {       
        consumerService.delete(baseUrl +"?id=" + id, function () {
            callBack();
        });  
    };

   

    return {
        model: _model,
        listar: _listar,
        salvar: _salvar,
        excluir: _excluir
    };

}]); 

