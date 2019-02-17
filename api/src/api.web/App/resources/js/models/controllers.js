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

        //CarregarLovPesquisa
        carregarLov({
            scope: $scope.lovPesquisa = {},
            servico: parm.tipoCursoService(),
            aoSelecionar: function (item) {
                $scope.filtros.tipoCursoId = item.id;
            }
        });

        //CarregarLovCadastro     
        carregarLov({
            scope: $scope.lovCadastro = {},
            servico: parm.tipoCursoService(),
            aoSelecionar: function (item) {
                $scope.model.tipoCursoId = item.id;
            }
        });
              
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
       
        //***VINCULAR DOCENTE***//        
        var param2 = {
            scopePartial: $scope.cursoDocente = {},
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

         //***ANTES DE VOLTAR VALIDA REGISTROS NÃO SALVOS***//
        $scope.antesDeVoltar = function (form) {
            $scope.antesDeVoltar = function (form) {
                existeDadosNaoGravados($scope, $scope.model.docentes, 'DOCENTE', function () {
                    existeDadosNaoGravados($scope, $scope.model.conteudosProgramaticos, 'CONTEUDOS PROGRAMATICOS', function () {
                        $scope.voltar(form);
                    });
                });
            }
        }

    });

}]);

app.controller('turmaCtrl', ['$scope', 'alertService', 'parm', function ($scope, alertService, parm) {

    extendsAbstractController($scope, alertService, parm, function () {

        //CarregarLovPesquisa
        carregarLov({
            scope: $scope.lovPesquisa = {},
            servico: parm.cursoService(),
            aoSelecionar: function (item) {
                $scope.filtros.cursoId = item.id;
            }
        });
        
        //CarregarLovCadastro     
        carregarLov({
            scope: $scope.lovCadastro = {},
            servico: parm.cursoService(),
            aoSelecionar: function (item) {
                $scope.model.cursoId = item.id;
            }
        });
        

        ////***VINCULAR CONTEUDO PROGRAMATICO***//       
        var param1 = {
            scopePartial: $scope.turmaConteudoProgramatico = {},
            servicoVinculo: parm.turmaConteudoProgramaticoService(),
            servicoLov: parm.conteudoProgramaticoService(),
            nomeAtributoFK01: "turmaId",
            nomeAtributoFK02: "conteudoProgramaticoId",
            nomeTabelaLov: 'conteudoProgramatico',
            model: $scope.model,
            alertService: alertService,
            funcGrid: function () {
                return $scope.model.conteudosProgramaticos;
            }
        };      
        vincular(param1);
        

        ////***VINCULAR DOCENTE***//    
        var param2 = {
            scopePartial: $scope.turmaDocente = {},
            servicoVinculo: parm.turmaDocenteService(),
            servicoLov: parm.docenteService(),
            nomeAtributoFK01: "turmaId",
            nomeAtributoFK02: "docenteId",
            nomeTabelaLov: 'docente',
            model: $scope.model,
            alertService: alertService,
            funcGrid: function () {
                return $scope.model.docentes;
            }
        };
        vincular(param2);
     

        //***ANTES DE VOLTAR VALIDA REGISTROS NÃO SALVOS***//
        $scope.antesDeVoltar = function (form) {   
            existeDadosNaoGravados($scope, $scope.model.docentes, 'DOCENTE', function () {
                existeDadosNaoGravados($scope, $scope.model.conteudosProgramaticos, 'CONTEUDOS PROGRAMATICOS', function () {
                    $scope.voltar(form);
                });
            });
        }

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

//comportamento da pagina Uf
app.controller('ufCtrl', ['$scope', 'alertService', 'parm', function ($scope, alertService, parm) {
    extendsAbstractController($scope, alertService, parm);
}]);

//comportamento da pagina Uf
app.controller('municipioCtrl', ['$scope', 'alertService', 'parm', function ($scope, alertService, parm) {
    extendsAbstractController($scope, alertService, parm, function () {


        //CarregarLovPesquisa
        carregarLov({
            scope: $scope.lovPesquisa = {},
            servico: parm.ufService(),
            aoSelecionar: function (item) {
                $scope.filtros.ufId = item.id;
            }
        });

        //CarregarLovCadastro     
        carregarLov({
            scope: $scope.lovCadastro = {},
            servico: parm.ufService(),
            aoSelecionar: function (item) {
                $scope.model.ufId = item.id;
            }
        });

    });
}]);

//funções em commun para telas de cadastros
function extendsAbstractController($scope, alertService, parm, func) {

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
        $scope.painelPesquisar = !bool;
        $scope.painelEditar = bool;
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
function vincular(param) {

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

        if (funcGrid().filter(item => {return item[nomeAtributoFK02] == scopePartial.model[nomeTabelaLov]["id"]; }).length > 0) {
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

function carregarLov(param) {
    param.scope.pesquisar = function (fitro) {
        param.servico.lov(fitro, function (data) {
            param.scope.dados = data;
        });
    }
    param.scope.selecionar = function (item) {
        param.aoSelecionar(item);
    }
}

function existeDadosNaoGravados(scope, grid, msg, callback) {    
    var listaItemconteudo = grid.filter(item => item.id == 0);  
    if (listaItemconteudo.length > 0) {
        scope.modalConfirmar("Atencao", "Existem item a serem salvo na aba {VINCULO DE  " + msg+"}. Deseja sair memo assim? Os dados vinculados serao perdidos.", null, function () {
            callback();
        });
    } else {
        callback();
    }    
}





