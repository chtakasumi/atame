//modulo principal
var app = angular.module("main", ["ngRoute", "ngSanitize", "ngAnimate", "toaster", "datatables",
    "ui.select", "ui.utils.masks", "angular.modal.bootstrap", "form.validation", "ngMessages","html.date"]);

//constantes
app.constant('configConst', {
    baseUrl: "", //url da minha aplicação
    baseUrlView: "./view/", //paginas html
    baseUrlApi: "/api/", //onde meu servico ira consumir dados no banco de dados
});

//permissão e autorização
app.run(function ($rootScope, autenticacaoService, $location, configConst, DTDefaultOptions, DTColumnDefBuilder) {

    //configuracao da Tables
    DTDefaultOptions.setLanguageSource(configConst.baseUrl + '/resources/js/plugins/data-table/Portuguese-Brasil.Json');
    DTDefaultOptions.setLoadingTemplate('<img src="' + configConst.baseUrl + '/resources/img/ajax-loader.gif">');
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
    $rootScope.modalExcluir = function (func, id, descricao) {
        var msg = "Deseja exluir o registor:" + descricao;
        $rootScope.modalConfirmar("Excluir", msg, id, func);
    }

    $rootScope.modalConfirmar = function (titulo, descricao, param, func) {
        $rootScope.showModalConfirmar = true;  //abre modal
        $rootScope.tituloModal = titulo;
        $rootScope.msg = descricao;

        $rootScope.Confirmar = function () {
            $rootScope.showModalConfirmar = false; //fecha modal
            func(param); //executa funcao de exclusão           
        }

        $rootScope.FecharModal = function () {
            $rootScope.showModalConfirmar = false;
        }
    }

    //loading
    $rootScope.Loading = function (status, msg) {
        // setTimeout(function () {
        $rootScope.showModalLoading = status;
        $rootScope.msg = msg;
        //});
        // $rootScope.facaDisgestao();     
    }

    $rootScope.facaDisgestao = function () {
        if ($rootScope.$$phase != '$apply' && $rootScope.$$phase != '$digest') {
            $rootScope.$apply();
        }
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
                    parm: function (cursoService, tipoCursoService, docenteService, conteudoProgramaticoService, cursoDocenteService, cursoConteudoProgramaticoService) {
                        return {
                            titulo: function () {
                                return "Curso";
                            },
                            filter: function () {
                                return {
                                    id: null, nome: null, tipoCursoId: null
                                };
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
                            docenteService: function () {
                                return docenteService;
                            },
                            conteudoProgramaticoService: function () {
                                return conteudoProgramaticoService;
                            },
                            cursoDocenteService: function () {
                                return cursoDocenteService;
                            },
                            cursoConteudoProgramaticoService: function () {
                                return cursoConteudoProgramaticoService;
                            },
                        }
                    }
                }
            })
            .when("/turma", {
                templateUrl: configConst.baseUrlView + "turma.html",
                controller: 'turmaCtrl',
                resolve: {
                    parm: function (turmaService, cursoService, docenteService, conteudoProgramaticoService, turmaDocenteService, turmaConteudoProgramaticoService) {
                        return {
                            titulo: function () {
                                return "Turma";
                            },
                            filter: function () {
                                return {
                                    id: null, identificacao: null, cursoId: null, inicio: null, fim: null
                                };
                            },
                            service: function () {
                                return turmaService;
                            },
                            model: function (callBack) {
                                return turmaService.model(function (data) {
                                    return callBack(data);
                                });
                            },
                            cursoService: function () {
                                return cursoService;
                            },
                            docenteService: function () {
                                return docenteService;
                            },
                            conteudoProgramaticoService: function () {
                                return conteudoProgramaticoService;
                            },
                            turmaDocenteService: function () {
                                return turmaDocenteService;
                            },
                            turmaConteudoProgramaticoService: function () {
                                return turmaConteudoProgramaticoService;
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
            .when("/vendedor", {
                templateUrl: configConst.baseUrlView + "vendedor.html",
                controller: 'vendedorCtrl',
                resolve: {
                    parm: function (vendedorService) {
                        return {
                            titulo: function () {
                                return "Vendedor";
                            },
                            filter: function () {
                                return { id: null, nome: null };
                            },
                            service: function () {
                                return vendedorService;
                            },
                            model: function (callBack) {
                                return vendedorService.model(function (data) {
                                    return callBack(data);
                                });
                            }
                        }
                    }
                }
            })
            .when("/uf", {
                templateUrl: configConst.baseUrlView + "uf.html",
                controller: 'ufCtrl',
                resolve: {
                    parm: function (ufService) {
                        return {
                            titulo: function () {
                                return "UF";
                            },
                            filter: function () {
                                return { id: null, nome: null , sigla:null};
                            },
                            service: function () {
                                return ufService;
                            },
                            model: function (callBack) {
                                return ufService.model(function (data) {
                                    return callBack(data);
                                });
                            }
                        }
                    }
                }
            })
            .when("/municipio", {
                templateUrl: configConst.baseUrlView + "municipio.html",
                controller: 'municipioCtrl',
                resolve: {
                    parm: function (municipioService, ufService) {
                        return {
                            titulo: function () {
                                return "Minicípio";
                            },
                            filter: function () {
                                return { id: null, nome: null, codigo: null, uf:null };
                            },
                            service: function () {
                                return municipioService;
                            },
                            model: function (callBack) {
                                return municipioService.model(function (data) {
                                    return callBack(data);
                                });
                            },
                            ufService: function (callBack) {
                                return ufService;
                            }
                        }
                    }
                }
            })
            .otherwise('/home');
    }]);

app.factory("consumerService", ['$http', 'configConst', function ($http, configConst) {

    var param = { headers: { 'Content-Type': 'text/json', 'Accept': 'text/json' } };

    var headers = { 'Content-Type': 'text/json', 'Accept': 'text/json' };

    var _get = function (url, callback) {
        var url = configConst.baseUrlApi + url;
        return $http.get(url, param).then(function (data) {
            callback(data.data);
        });
    }

    var _delete = function (url, callback) {
        var url = configConst.baseUrlApi + url;
        return $http.delete(url, param).then(function (data) {
            callback(data.data);
        });
    }

    var _post = function (url, data, callback) {
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

app.factory('httpInterceptor', ['$q', '$rootScope', 'alertService', '$location',
    function ($q, $rootScope, alertService, $location) {

        function CloseLoading() {
            setTimeout(function () {
                $rootScope.showModalLoading = false;
                $rootScope.msg = "";
                $rootScope.facaDisgestao();
            }, 1000);

        }

        return {
            request: function (config) {
                return config || $q.when(config);
            },
            requestError: function (rejection) {
                return $q.reject(rejection);
            },
            response: function (response) {

                CloseLoading();

                return response || $q.when(response);

            },
            responseError: function (rejection) {

                CloseLoading();

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
