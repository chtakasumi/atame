//modulo principal
var app = angular.module("main", ["ngRoute", "ngSanitize", "ngAnimate", "toaster","datatables"]);

//constantes
app.constant('configConst', {
    baseUrl: "http://localhost:5000", //url da minha aplicação
    baseUrlView: "./view/", //paginas html
    baseUrlApi: "/api/", //onde meu servico ira consumir dados no banco de dados
});

//permissão e autorização
app.run(function ($rootScope, autenticacaoService, $location, configConst, DTDefaultOptions, DTColumnDefBuilder) { 

    DTDefaultOptions.setLanguageSource(configConst.baseUrl + '/resources/js/plugins/dataTable/Portuguese-Brasil.Json');
    DTDefaultOptions.setLoadingTemplate('<img src="' + configConst.baseUrl +'/resources/img/ajax-loader.gif">');
    DTDefaultOptions.setOption("filter", false);
    DTDefaultOptions.setOption("aoColumnDefs", [
        {
            aTargets: 'edicao',
            bSortable: false
        }
    ]);

    //esse evento acontece toda vez que mudo de url
    $rootScope.$on('$locationChangeSuccess', function (event, toState, toStateParams) {
        //ativar ao clicar no menu
       // ativaMenu(toState);

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

    //function ativaMenu(toState) {
    //    $rootScope.classAgendamentoActive = '';
    //    $rootScope.classAtendimentoActive = '';
    //    $rootScope.classPacienteActive = '';

    //    if (toState.indexOf('agendamento') > -1) {
    //        $rootScope.classAgendamentoActive = 'active'
    //    }
    //    else if (toState.indexOf('atendimento') > -1) {
    //        $rootScope.classAtendimentoActive = 'active'
    //    }
    //    else if (toState.indexOf('paciente') > -1) {
    //        $rootScope.classPacienteActive = 'active'
    //    }
    //}
});

//rotas de url amigaveis
app.config(['$routeProvider', 'configConst', '$httpProvider', '$qProvider', '$locationProvider', function ($routeProvider, configConst, $httpProvider, $qProvider, $locationProvider) {
    $httpProvider.interceptors.push('httpInterceptor');
    $httpProvider.defaults.withCredentials = true;
    $qProvider.errorOnUnhandledRejections(false);
      
  
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
        .when("/curso", {
            templateUrl: configConst.baseUrlView + "curso.html",
        })
        .otherwise('/home');
}]);

app.factory("consumerService", ['$http', 'configConst', function ($http, configConst) {

    var param = { headers: { 'Content-Type': 'text/json', 'Accept': 'text/json' } };

    var headers = { 'Content-Type': 'text/json', 'Accept': 'text/json'};

    var _get = function (url, callback) {
        var url =configConst.baseUrlApi + url;
       
        $http.get(url, param).then(function (data) {            
            callback(data.data);
        });

    }

    var _delete= function (url, callback) {
        var url = configConst.baseUrlApi + url;
        $http.delete(url, param).then(function (data) {
            callback(data.data);
        });

    }


    var _post= function (url, data, callback) {
        var urlNew = configConst.baseUrlApi + url;
        var parm = {
            method: 'POST',
            headers: headers           
        };

        $http.post(urlNew, data, parm).then(
            function (data) {
                callback(data.data);
            });
    }

    var _put = function (url, data, callback) {
        var urlNew = configConst.baseUrlApi + url;
        var parm = {
            method: 'PUT',
            headers: headers
        };

        $http.put(urlNew, data, parm).then(
            function (data) {
                callback(data.data);
        });
    }
  

    return {
        get: _get,
        delete: _delete,
        post: _post,
        put: _put,
    };
}]);

app.factory("alertService", ['toaster', function (toaster) {
    
    var _getSucesso = function (msg) {
        toaster.publicarSucesso(msg);
    }

    var _getInfo = function (msg) { 
        toaster.publicarInformacao(msg);
    }

    var _getAdvertencia = function (msg) {   
        toaster.publicarAdvertencia(msg)
    }

    var _getError = function (msg) {
        toaster.publicarErro(msg);
    }

    function sumirMensagem() {
        $rootScope.mensagem_show = false;
        $rootScope.mensagem = '';
        $rootScope.$digest();
    }
    
    return {       
        getSucesso: _getSucesso,
        getInfo: _getInfo,
        getAdvertencia: _getAdvertencia,
        getError: _getError
    };

}]);

app.factory('httpInterceptor', ['$q', '$rootScope', 'alertService','$location',
    function ($q, $rootScope, alertService, $location) {
        return {
            response: function (response) {  
                return response || $q.when(response);
            },
            responseError: function (rejection) {           

                switch (rejection.status) {
               
                    case 400:    
                        alertService.getAdvertencia(rejection.data);                        
                        break;  
                    case 401:
                        alertService.getAdvertencia(rejection.data);
                        $location.path("/login");
                        break;                   
                    case 403: 
                        alertService.getAdvertencia(rejection.data);
                        break;
                    case 404: 
                        alertService.getAdvertencia(rejection.data);
                        break;  
                    case 500:                        
                        alertService.getError(rejection.data);
                        break;
                    default:
                        alertService.getError(rejection.data);
                }
                return $q.reject(rejection);
            }
        };

    }]);