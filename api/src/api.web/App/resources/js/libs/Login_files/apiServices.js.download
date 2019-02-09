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

app.factory("cursoService", ['consumerService', function (consumerService) {
    var baseUrl = "curso";

    var _model = function (callBack) {
        consumerService.get(baseUrl + "/model", function (modelo) {
            callBack(JSON.parse(JSON.parse(modelo)));
        });
    };

    var _listar = function (filtro, callBack) {
        consumerService.post(baseUrl + "/listar", filtro, function (data) {
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
        consumerService.delete(baseUrl + "?id=" + id, function () {
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

app.factory("tipoCursoService", ['consumerService', function (consumerService) {

    var baseUrl = 'tipoCurso';

    var _model = function (callBack) {
        consumerService.get(baseUrl+"/model", function (modelo) {
            callBack(JSON.parse(JSON.parse(modelo)));
        });
    };

    var _lov = function (string, callBack) {
        var param = (string) ? string : null;
        consumerService.get(baseUrl + "/" + param, function (data) {
            callBack(data);
        })
    };

    var _listar = function (filtro, callBack) {
        consumerService.post(baseUrl + "/listar", filtro, function (data) {
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
        lov: _lov,
        listar: _listar,
        salvar: _salvar,
        excluir: _excluir
    };

}]); 

app.factory("docenteService", ['consumerService', function (consumerService) {
    var baseUrl = "docente";

    //não muda
    var _model = function (callBack) {
        consumerService.get(baseUrl + "/model", function (modelo) {
            callBack(JSON.parse(JSON.parse(modelo)));
        });
    };

    //não muda
    var _listar = function (filtro, callBack) {       
        consumerService.post(baseUrl + "/listar", filtro, function (data) {
            callBack(data);
        })
    };

    //não muda
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

     //não muda
    var _excluir = function (id, callBack) {
        consumerService.delete(baseUrl + "?id=" + id, function () {
            callBack();
        });
    };

    //não muda
    return {
        model: _model,
        listar: _listar,
        salvar: _salvar,
        excluir: _excluir
    };

}]);

app.factory("conteudoProgramaticoService", ['consumerService', function (consumerService) {
    var baseUrl = "conteudoProgramatico";

    //não muda
    var _model = function (callBack) {
        consumerService.get(baseUrl + "/model", function (modelo) {
            callBack(JSON.parse(JSON.parse(modelo)));
        });
    };

    //não muda
    var _listar = function (filtro, callBack) {
        consumerService.post(baseUrl + "/listar", filtro, function (data) {
            callBack(data);
        })
    };

    //não muda
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

    //não muda
    var _excluir = function (id, callBack) {
        consumerService.delete(baseUrl + "?id=" + id, function () {
            callBack();
        });
    };

    //não muda
    return {
        model: _model,
        listar: _listar,
        salvar: _salvar,
        excluir: _excluir
    };

}]); 


