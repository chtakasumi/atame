var app = angular.module('main');

//comportamento da pagina login
app.controller('loginCtrl', ['$scope', 'autenticacaoService', '$location', function ($scope, autenticacaoService, $location) {
    $scope.autenticar = function () {
        autenticacaoService.autenticar($scope.login, $scope.senha, function (chave) {
            if (chave) {
                $location.path("/home");
            }
        });
    }
}]);

app.controller('cursoCtrl', ['$scope', 'alertService', 'parm', function ($scope, alertService, parm) {

    extendsAbstractController($scope, alertService, parm, function () {

        $scope.antesDeVoltar = function (form) {           
            var listaItemconteudo = $scope.model.conteudosProgramaticos.filter(item => item.id == 0);
            var listaItemdocente = $scope.model.docentes.filter(item => item.id == 0);
            
            if (listaItemdocente.length > 0) {
                $scope.modalConfirmar("Atencao", "Existem item a serem salvo na aba {VINCULO DE  DOCENTE}. Deseja sair memo assim? Os dados vinculados serao perdidos.", form, function (form) {
                    if (listaItemconteudo.length > 0) {
                        $scope.modalConfirmar("Atencao", "Existem item a serem salvo na aba  {VINCULO DE CONTEUDOS PROGRAMATICOS}. Deseja sair memo assim? Os dados vinculados serao perdidos.", form, function (form) {
                            $scope.voltar(form)
                        });
                    } else {
                        $scope.voltar(form)
                    }    
                });
            } else {
                $scope.voltar(form)
            }                   
        }

        //************TIPO CURSO*******************//
        $$TipoCursoService = parm.tipoCursoService();

        //CADASTRO
        $scope.buscarTipoCurso = function (filtro) {
            $$TipoCursoService.lov(filtro, function (data) {
                $scope.listaTipoCurso = data;
            });
        }
        $scope.selecionarTipoCurso = function (item) {
            $scope.model.tipoCursoId = item.id;
        }

        //FILTRO
        $scope.buscarTipoCursoPesquisa = function (fitro) {
            $$TipoCursoService.lov(fitro, function (data) {
                $scope.listaTipoCursoPesquisa = data;
            });
        }
        $scope.selecionarPesquisa = function (item) {
            $scope.filtros.tipoCursoId = item.id;
        }
        //*****************FIM TIPO CURSO*************//
        
        //***VINCULAR CONTEUDO PROGRAMATICO***//       
        var param1 = {
            scopePartial: $scope.cursoConteudoProgramatico = {},
            servicoVinculo: parm.cursoConteudoProgramaticoService(),
            servicoLov: parm.conteudoProgramaticoService(),
            nomeAtributoFK01: "cursoId",
            nomeAtributoFK02: "conteudoProgramaticoId",
            nomeTabelaLov: 'conteudoProgramatico',
            model: $scope.model,
            alertService: alertService,
            funcGrid: function () {
                return $scope.model.conteudosProgramaticos;
            }
        };
        vincular(param1);
        //***FIM CONTEUDO PROGRAMATICO***//
        
        //***VINCULAR DOCENTE***//
        $scope.cursoDocente = {};
        var param2 = {
            scopePartial: $scope.cursoDocente,
            servicoVinculo: parm.cursoDocenteService(),
            servicoLov: parm.docenteService(),
            nomeAtributoFK01: "cursoId",
            nomeAtributoFK02: "docenteId",
            nomeTabelaLov: 'docente',
            model: $scope.model,
            alertService: alertService,
            funcGrid: function () {
                return $scope.model.docentes;
            }
        };
        vincular(param2);
        //***FIM VINCULAR DOCENTE***//

    });

}]);

app.controller('tipoCursoCtrl', ['$scope', 'alertService', 'parm', function ($scope, alertService, parm) {
    extendsAbstractController($scope, alertService, parm);
}]);

//comportamento da pagina docente
app.controller('docenteCtrl', ['$scope', 'alertService', 'parm', function ($scope, alertService, parm) {
    extendsAbstractController($scope, alertService, parm);
}]);

app.controller('conteudoProgramaticoCtrl', ['$scope', 'alertService', 'parm', function ($scope, alertService, parm) {
    extendsAbstractController($scope, alertService, parm);
}]);

//comportamento da pagina vendedor
app.controller('vendedorCtrl', ['$scope', 'alertService', 'parm', function ($scope, alertService, parm) {
    extendsAbstractController($scope, alertService, parm);
}]);

//funções em commun para telas de cadastros
function extendsAbstractController($scope, alertService, parm, func) {

    this.$$Model = null;
    this.$$TituloModelo = null;
    this.$$Servico = null;
    this.$$Filtros = null;
    this.$$Pesquisa;

    //não muda
    parm.model(function (dados) {
        $$Model = dados;
        $$TituloModelo = parm.titulo();
        $$Servico = parm.service();
        $$Filtros = parm.filter();
        $$Pesquisa = true;
        init();

        func = (func) ? func($$Model) : func;
    });

    //não muda
    function init() {
        $scope.model = angular.copy($$Model);
        $scope.tituloModelo = angular.copy($$TituloModelo);
        $scope.filtros = angular.copy($$Filtros);

        modoEdicao(false);
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
    function modoEdicao(bool) {
        $scope.painelPesquisar = !bool;
        $scope.painelEditar = bool;
        if (bool) {
            limparFormulario();
        } else {
            $scope.limparFiltros();
            if ($$Pesquisa) {
                // o botão voltar não pesquisa novamente.                
                pesquisar();
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
    function pesquisar() {
        $$Servico.listar($scope.filtros, function (data) {
            $scope.grade_model = data;
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
function vincular (param) {

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

        if (funcGrid().filter(function (item) {

            return (item[nomeAtributoFK02] == scopePartial.model[nomeTabelaLov]["id"])
        }).length > 0) {
            alertService.getAdvertencia("Este elemente ja adicionado");
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


