//modulo principal
var app = angular.module("main", ["ngRoute", "ngSanitize", "ngAnimate", "toaster", "datatables",
    "ui.select", "ui.utils.masks", "angular.modal.bootstrap", "form.validation", "ngMessages", "html.date"]);

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
    DTDefaultOptions.setOption("columnDefs", [
        {
            defaultContent: "",
            targets: "_all" //para parar o erro de não encontrar colunas
        },
        {
            targets: 'edicao',           
            sortable: false
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
            .when("/parametro", {
                templateUrl: configConst.baseUrlView + "parametro.html",
                controller: 'parametroCtrl',
                resolve: {
                    parm: function (parametroService) {
                        return {
                            titulo: function () {
                                return "Parametro";
                            },
                            filter: function () {
                                return { id: null, chave: null };
                            },
                            service: function () {
                                return parametroService;
                            },
                            model: function (callBack) {
                                return parametroService.model(function (data) {
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
                                return { id: null, nome: null, sigla: null };
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
                                return { id: null, nome: null, codigo: null, uf: null };
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
            .when("/cliente", {
                templateUrl: configConst.baseUrlView + "cliente.html",
                controller: 'clienteCtrl',
                resolve: {
                    parm: function (clienteService, ufService, municipioService) {
                        return {
                            titulo: function () {
                                return "Cliente";
                            },
                            filter: function () {
                                return { id: null, nome: null, cpfCnpj: null };
                            },
                            service: function () {
                                return clienteService;
                            },
                            model: function (callBack) {
                                return clienteService.model(function (data) {
                                    return callBack(data);
                                });
                            },
                            ufService: function () {
                                return ufService;
                            },
                            municipioService: function () {
                                return municipioService;
                            }
                        }
                    }
                }
            })
            .when("/venda", {
                templateUrl: configConst.baseUrlView + "venda.html",
                controller: 'vendaCtrl',
                resolve: {
                    parm: function (vendaService, turmaService, vendedorService, clienteService, vendaClienteService, utils) {
                        return {
                            titulo: function () {
                                return "Venda";
                            },
                            filter: function () {
                                return { id: null, inicio: null, fim: null, turmaId: null, vendedorId: null, clienteFinanceiroId: null };
                            },
                            service: function () {
                                return vendaService;
                            },
                            model: function (callBack) {
                                return vendaService.model(function (data) {
                                    return callBack(data);
                                });
                            },
                            turmaService: function () {
                                return turmaService;
                            },
                            vendedorService: function () {
                                return vendedorService;
                            },
                            clienteService: function () {
                                return clienteService;
                            },
                            vendaClienteService: function () {
                                return vendaClienteService;
                            },
                            utils: function () {
                                return utils;
                            }
                        }
                    }
                }
            })
            .when("/prospeccao", {
                templateUrl: configConst.baseUrlView + "prospeccao.html",
                controller: 'prospeccaoCtrl',
                resolve: {
                    parm: function (prospeccaoService, vendedorService, clienteService, cursoService, prospeccaoInteresseService) {
                        return {
                            titulo: function () {
                                return "Prospecção";
                            },
                            filter: function () {
                                return { id: null, inicio: null, fim: null, vendedorId: null, clienteId: null };
                            },
                            service: function () {
                                return prospeccaoService;
                            },
                            model: function (callBack) {
                                return prospeccaoService.model(function (data) {
                                    return callBack(data);
                                });
                            },
                            vendedorService: function () {
                                return vendedorService;
                            },
                            clienteService: function () {
                                return clienteService;
                            },
                            vendaClienteService: function () {
                                return vendaClienteService;
                            },
                            prospeccaoInteresseService: function () {
                                return prospeccaoInteresseService;
                            },
                            cursoService: function () {
                                return cursoService;
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

app.factory("modelService", [function () {

    //funções em commun para telas de cadastros
    var _extendsAbstractController = function ($scope, alertService, parm, func) {

        var $$Model = null;
        var $$TituloModelo = null;
        var $$Servico = null;
        var $$Filtros = null;
        var $$Pesquisa;

        //não muda
        parm.model(function (dados) {
            $$Model = dados;
            $$TituloModelo = parm.titulo();
            $$Servico = parm.service();
            $$Filtros = parm.filter();
            $$Pesquisa = true;
            init();

        });

        //não muda
        function init() {
            $scope.model = angular.copy($$Model);
            $scope.tituloModelo = angular.copy($$TituloModelo);
            $scope.titulo = '';
            $scope.filtros = angular.copy($$Filtros);
            modoEdicao(false, function () {
                setTimeout(function () {
                    func = (func) ? func($$Model) : func;
                    $scope.facaDisgestao();
                })
            });
        }

        //não muda
        $scope.limparFiltros = function () {
            $scope.filtros = angular.copy($$Filtros);
        }

        //não muda
        $scope.novo = function () {
            modoEdicao(true);
            $scope.titulo = "Cadastrar Novo " + $$TituloModelo;
        }

        //não muda
        $scope.editar = function (entity) {
            modoEdicao(true);
            $scope.titulo = "Editar " + $$TituloModelo;
            carregarFormulario(entity)
        }

        //não muda
        $scope.voltar = function (form) {
            resetaForm(form);
            $Pesquisa = false;
            modoEdicao(false);
        }

        //não muda
        function modoEdicao(bool, func) {
            $scope.modoEdicao = bool

            if (bool) {
                limparFormulario();
            } else {
                $scope.limparFiltros();
                if ($$Pesquisa) {
                    // o botão voltar não pesquisa novamente.                
                    pesquisar(func);
                }
                $$Pesquisa = true;
            }
        }

        //não muda
        function limparFormulario() {
            $scope.model = angular.copy($$Model);
        }

        //não muda
        function carregarFormulario(entity) {
            $scope.model = entity;
        }

        //não muda
        $scope.pesquisar = function () {
            pesquisar();
        }

        //CRUD
        //não muda
        function pesquisar(func) {
            $$Servico.listar($scope.filtros, function (data) {
                $scope.grade_model = data;
                if (func) {
                    func();
                }
            });
        }

        //não muda
        $scope.excluir = function (id) {
            $$Servico.excluir(id, function () {
                modoEdicao(false);
                alertService.getSucesso("Registro excluido com sucesso");
            });
        }

        //não muda
        $scope.salvar = function (form) {

            angular.forEach(form.$$controls, function (field) {
                field.$setDirty();
            });

            if (form.$valid) {
                //validacoes       
                $$Servico.salvar($scope.model, function (dados) {
                    resetaForm(form);
                    limparFormulario()
                    modoEdicao(false);
                    alertService.getSucesso("Dados salvo com sucesso");
                });
            }
        }

        $scope.GetValidFormGrupoClass = function (componente) {

            if (componente.$touched || componente.$dirty /*|| form.$submitted*/) {
                return (componente.$valid) ? 'has-success' : 'has-error';
            }
        }

        $scope.GetValidInputClass = function (componente) {
            if (componente.$touched || componente.$dirty /*form.$submitted*/) {
                return (componente.$valid) ? 'glyphicon-ok' : 'glyphicon-remove';
            }
        }

        $scope.GetValidMessages = function (componente) {
            return componente.$invalid && (componente.$touched || componente.$dirty)
        }

        function resetaForm(form) {
            form.$setPristine();
            form.$setUntouched();
            form.$submitted = false;
        }
    }

    //funções em commun para telas de vinculos
    var _vincular = function (param) {

        //parametros esperados para vincular o modelo principal ao modelo de ligação
        var scopePartial = param.scopePartial,
            servicoVinculo = param.servicoVinculo,
            servicoLov = param.servicoLov,
            nomeAtributoFK01 = param.nomeAtributoFK01,
            nomeAtributoFK02 = param.nomeAtributoFK02,
            nomeTabelaLov = param.nomeTabelaLov,
            model = param.model,
            alertService = param.alertService,
            funcGrid = param.funcGrid

        //carregaModelos
        servicoVinculo.model(function (model) {
            scopePartial.model = angular.copy(model);
        });


        ////pesquisa  tipoCurso (filtro)
        scopePartial.listar = function (fitro) {
            servicoLov.lov(fitro, function (data) {
                scopePartial.data = data;
            });
        }

        scopePartial.selected = function (item) {
            scopePartial.model[nomeAtributoFK01] = model.id;
            scopePartial.model[nomeAtributoFK02] = item["id"];
            scopePartial.model[nomeTabelaLov] = item;
        }

        /////aba vincular
        scopePartial.vincular = function () {

            if (!scopePartial.model.item) {
                alertService.getAdvertencia("Selecione um item para vinculo.");
                return;
            }

            if (funcGrid().filter(item => { return item[nomeAtributoFK02] == scopePartial.model[nomeTabelaLov]["id"]; }).length > 0) {
                alertService.getAdvertencia("Este elemento ja foi adicionado");
                return;
            }

            delete scopePartial.model['item'];

            funcGrid().push(angular.copy(scopePartial.model));

        }

        ///aba vincular docente
        scopePartial.desvincular = function (element) {
            if (element.id == 0) {
                funcGrid().splice(funcGrid().indexOf(element), 1);//limpar da tela
                // funcAtualizaGrid(gridVinculoModel);
                return;
            }
            servicoVinculo.desvincular(element.id, function () {
                funcGrid().splice(funcGrid().indexOf(element), 1);//limpar da tela           
                alertService.getSucesso("Registro excluido com sucesso");
            });
        }
    }

    var _carregarLov = function (param) {
        param.scope.pesquisar = function (fitro) {
            param.servico.lov(fitro, function (data) {
                param.scope.dados = data;
            });
        }
        param.scope.selecionar = function (item) {
            param.aoSelecionar(item);
        }
    }

    var _existeDadosNaoGravados = function (scope, grid, msg, callback) {
        var listaItemconteudo = grid.filter(item => item.id == 0);
        if (listaItemconteudo.length > 0) {
            scope.modalConfirmar("Atencao", "Existem item a serem salvo na aba {VINCULO DE  " + msg + "}. Deseja sair memo assim? Os dados vinculados serao perdidos.", null, function () {
                callback();
            });
        } else {
            callback();
        }
    }
    
    return {
        extendsAbstractController: _extendsAbstractController,
        vincular: _vincular,
        carregarLov: _carregarLov,
        existeDadosNaoGravados: _existeDadosNaoGravados
    };

}]);

app.factory("globalService", [function () {

    var _extendsAbstractServices = function (baseUrl, consumerService, $rootScope) {

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

        var _desvincular = function (id, callBack) {
            var url = baseUrl + "?id=" + id;
            $rootScope.Loading(true, "DELETE: " + url);
            consumerService.delete(url, function () {
                callBack();
            });
        }

        return {
            model: _model,
            lov: _lov,
            listar: _listar,
            salvar: _salvar,
            excluir: _excluir,
            desvincular: _desvincular
        };
    }

    return {
        extendsAbstractServices: _extendsAbstractServices
    }

}]);

app.factory("utils", ['consumerService', function (consumerService) {
    var _getData = function (callback) {
        consumerService.get('utils/datahora', function (data) {
            //todo: pegar data do servidor
            callback(JSON.parse(data));
        })
    }
    var _incrementaDias = function (data, dias) {
        var time = new Date(data);
        var outraData = new Date(data);
        outraData.setDate(time.getDate() + dias); // Adiciona 3 dias
        return outraData.toISOString();
    }
    return {
        getData: _getData,
        incrementaDias: _incrementaDias
    }

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
