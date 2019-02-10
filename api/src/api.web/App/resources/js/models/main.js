//modulo principal
var app = angular.module("main", ["ngRoute", "ngSanitize", "ngAnimate", "toaster", "datatables", "ui.select", "ui.utils.masks","angular.modal.bootstrap"]);

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

    //modal
    $rootScope.modalExcluir = function (func, id, descricao)
    {       
        $rootScope.showModalExcluir = true;  //abre modal
        
        $rootScope.msg = descricao;
        
        $rootScope.Confirmar = function () {  
            $rootScope.showModalExcluir = false; //fecha modal
            func(id); //executa funcao de exclusão           
        }

        $rootScope.FecharModal = function () {
            $rootScope.showModalExcluir = false;         
        }

        facaDisgestao();       
    }

    //loading
    $rootScope.Loading = function (showModalLoading, msg) {      
        $rootScope.showModalLoading = showModalLoading;
        $rootScope.msg = msg;
        facaDisgestao();      
    }

    function facaDisgestao() {
        setTimeout(function () {
            if ($rootScope.$$phase != '$apply' && $rootScope.$$phase != '$digest') {
                $rootScope.$apply();
            }
        }, 500);
    }
    
});

//rotas de url amigaveis
app.config(['$routeProvider', 'configConst', '$httpProvider', '$qProvider', '$locationProvider', 'uiSelectConfig',
    function ($routeProvider, configConst, $httpProvider, $qProvider, $locationProvider, uiSelectConfig) {
               
        $httpProvider.interceptors.push('httpInterceptor');

        $httpProvider.defaults.withCredentials = true;
        $qProvider.errorOnUnhandledRejections(false);
        uiSelectConfig.theme = 'bootstrap';    

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
            controller: 'cursoCtrl',
            resolve: {
                    parm: function (cursoService, tipoCursoService) {
                        return {
                            titulo: function () {
                                return "Curso";
                            },
                            filter: function () {
                                return { id: null, nome: null, tipoCursoId: null };
                            },
                            service: function () {
                                return cursoService;

                            },
                            model: function (callBack) {
                                return cursoService.model(function (data) {
                                    return callBack(data);
                                });
                            },
                            tipoCursoService: function () {
                                return tipoCursoService;
                            },
                        }
                    }
                }
            })
        .when("/tipoCurso", {
            templateUrl: configConst.baseUrlView + "tipoCurso.html",
            controller: 'tipoCursoCtrl',
            resolve: {
                parm: function (tipoCursoService) {
                    return {
                        titulo: function () {
                            return "Tipo Curso";
                        },
                        filter: function () {
                            return { id: null, descricao: null };

                        },
                        service: function () {
                            return tipoCursoService;

                        },
                        model: function (callBack) {
                            return tipoCursoService.model(function (data) {
                                return callBack(data);
                            });
                        }
                    }
                }
            }
        })
        .when("/docente", {
            templateUrl: configConst.baseUrlView + "docente.html",
            controller: 'docenteCtrl',
            resolve: {
                parm: function (docenteService) {
                    return {
                        titulo: function () {
                            return "Docente";
                        },
                        filter: function () {
                            return { id: null, nome: null, formacao: null };
                        },
                        service: function () {
                            return docenteService;
                        },
                        model: function (callBack) {
                            return docenteService.model(function (data) {
                                return callBack(data);
                            });
                        }
                    }
                }                
            }
        })
        .when("/conteudoProgramatico", {
            templateUrl: configConst.baseUrlView + "conteudoProgramatico.html",
            controller: 'conteudoProgramaticoCtrl',
            resolve: {
                parm: function (conteudoProgramaticoService) {
                    return {
                        titulo: function () {
                            return "Conteúdo Programático";
                        },
                        filter: function () {
                            return { id: null, identificacao: null };
                        },
                        service: function () {
                            return conteudoProgramaticoService;
                        },
                        model: function (callBack) {
                            return conteudoProgramaticoService.model(function (data) {
                                return callBack(data);
                            });
                        }
                    }
                }
            }
        })
        .otherwise('/home');

    }]);

app.factory("consumerService", ['$http', 'configConst', function ($http, configConst) {

    var param = { headers: { 'Content-Type': 'text/json', 'Accept': 'text/json' } };

    var headers = { 'Content-Type': 'text/json', 'Accept': 'text/json'};

    var _get = function (url, callback) {
        var url =configConst.baseUrlApi + url;       
        return $http.get(url, param).then(function (data) {            
            callback(data.data);
        });
    }

    var _delete= function (url, callback) {
        var url = configConst.baseUrlApi + url;
        return $http.delete(url, param).then(function (data) {
            callback(data.data);
        });
    }

    var _post= function (url, data, callback) {
        var urlNew = configConst.baseUrlApi + url;
        var parm = {
            method: 'POST',
            headers: headers           
        };

        return $http.post(urlNew, data, parm).then(
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

        return $http.put(urlNew, data, parm).then(
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
    
    return {       
        getSucesso: _getSucesso,
        getInfo: _getInfo,
        getAdvertencia: _getAdvertencia,
        getError: _getError
    };

}]);

app.factory('httpInterceptor', ['$q', '$rootScope', 'alertService','$location',
    function ($q, $rootScope, alertService, $location) {
        function loadingClose(retorno) {
            setTimeout(function () {
                var msg = retorno.status + ": " + retorno.statusText;
                $rootScope.Loading(false, msg);
            },600);
        }

        return {
            request: function (config) {              
                return config || $q.when(config);
            },
            requestError: function (rejection) {                
                loadingClose(rejection);

                return $q.reject(rejection);
            },
            response: function (response) { 
                loadingClose(response);
                return response || $q.when(response);
            },
            responseError: function (rejection) { 
                
                loadingClose(rejection);

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
