//modulo principal
var app = angular.module("main", ["ngRoute", "ngSanitize", "ngAnimate"]);

//constantes
app.constant('configConst', {
    baseUrl: "http://localhost:52565/", //url da minha aplicação
    baseUrlView: "./view/", //paginas html
    baseUrlApi: "http://localhost:5000/api/", //onde meu servico ira consumir dados no banco de dados
});

//permissão e autorização
app.run(function ($rootScope, autenticacaoService, $location) {

    $rootScope.mensagem = '';
    $rootScope.mensagem_show = false;

    //esse evento acontece toda vez que mudo de url
    $rootScope.$on('$locationChangeSuccess', function (event, toState, toStateParams) {
        //ativar ao clicar no menu
        ativaMenu(toState);

        //checa permissão de acesso as paginas
        if (toState.indexOf('login') > -1) {
            autenticacaoService.deslogar();
        }
        else if (toState.indexOf('logout') > -1) {
            autenticacaoService.deslogar();
        }
        if (!autenticacaoService.getPermission()) {
            $rootScope.permission = false;
            $location.path("/login");
        } else {
            $rootScope.permission = true;
        }

    });

    function ativaMenu(toState) {
        $rootScope.classAgendamentoActive = '';
        $rootScope.classAtendimentoActive = '';
        $rootScope.classPacienteActive = '';

        if (toState.indexOf('agendamento') > -1) {
            $rootScope.classAgendamentoActive = 'active'
        }
        else if (toState.indexOf('atendimento') > -1) {
            $rootScope.classAtendimentoActive = 'active'
        }
        else if (toState.indexOf('paciente') > -1) {
            $rootScope.classPacienteActive = 'active'
        }
    }
});

//rotas de url amigaveis
app.config(['$routeProvider', 'configConst', function ($routeProvider, configConst) {

    $routeProvider
        .when("/", {
            templateUrl: configConst.baseUrlView + "home.html",
        })
        .when("/home", {
            templateUrl: configConst.baseUrlView + "home.html",
        })
        .when("/login", {
            templateUrl: configConst.baseUrlView + "login.html",
            controller: 'loginCtrl',
            resolve: {
                deslogar: function (autenticacaoService) {
                    autenticacaoService.deslogar();
                    return autenticacaoService;
                }
            }
        })
        .when("/logout", {
            templateUrl: configConst.baseUrlView + "login.html",
            controller: 'loginCtrl',
            resolve: {
                deslogar: function (autenticacaoService) {
                    autenticacaoService.deslogar();
                    return autenticacaoService;
                }
            }
        })
        .when("/paciente", {
            templateUrl: configConst.baseUrlView + "paciente.html",
        })
        .when("/atendimento", {
            templateUrl: configConst.baseUrlView + "atendimento.html"
        })
        .when("/agendamento", {
            templateUrl: configConst.baseUrlView + "agendamento.html"
        }).
        otherwise('/home');
}]);

app.factory("consumerService", ['$http', 'configConst', function ($http, configConst) {

    var param = { headers: { 'Content-Type': 'text/json', 'Accept': 'text/json' } };

    var headers = { 'Content-Type': 'text/json', 'Accept': 'text/json','Access-Control-Allow-Origin':'*' };

    var _get = function (url, callback) {
        var url =configConst.baseUrlApi + url;
       
        $http.get(url, param).then(function (data) {            
            callback(data.data);
        });
    }

    var _post= function (url, data, callback) {
        var urlNew = configConst.baseUrlApi + url;
        var parm = {
            method: 'POST',
            headers: headers           
        };

        $http.post(urlNew, data, parm).then(function(data){
            callback(data.data);
        });
     }

    return {
        get: _get,
        post: _post,
    };
}]);

app.factory("alertService", [function () {
    var $rootScope = null;
    var time = 5000;
    var _new = function (_$rootScope) {
        $rootScope = _$rootScope
    }
    var _getSucesso = function (msg) {
        $rootScope.mensagem = '<div class="alert alert-success" role="alert">' + msg + '</div>';
        $rootScope.mensagem_show = true;
        setTimeout(sumirMensagem, time);
    }
    var _getInfo = function (msg) {
        $rootScope.mensagem = '<div class="alert alert-info" role="alert">' + msg + '</div>';
        $rootScope.mensagem_show = true;
        setTimeout(sumirMensagem, time);
    }

    var _getAdvertencia = function (msg) {
        $rootScope.mensagem = '<div class="alert alert-warning" role="alert">' + msg + '</div>';
        $rootScope.mensagem_show = true;
        setTimeout(sumirMensagem, time);
    }

    var _getError = function (msg) {
        $rootScope.mensagem = '<div class="alert alert-danger" role="alert">' + msg + '</div>';
        $rootScope.mensagem_show = true;
        setTimeout(sumirMensagem, time);
    }

    function sumirMensagem() {
        $rootScope.mensagem_show = false;
        $rootScope.mensagem = '';
        $rootScope.$digest();
    }


    return {
        new: _new,
        getSucesso: _getSucesso,
        getInfo: _getInfo,
        getAdvertencia: _getAdvertencia,
        getError: _getError
    };

}]);