var app = angular.module('main');

//comportamento da pagina login
app.controller('loginCtrl', function ($scope, autenticacaoService, $location) {
    $scope.autenticar = function () {        
        autenticacaoService.autenticar($scope.login, $scope.senha, function (chave) {
            if (chave) {
                $location.path("/home");
            }
        });       
    }
});

app.controller('cursoCtrl', ['$scope', 'alertService', 'parm', function ($scope, alertService, parm) {

    this.$$TipoCursoService = null

    extendsAbstractController($scope, alertService, parm, function () {
        $$TipoCursoService = parm.tipoCursoService();
    });
   
    //pesquisa tipoCurso (cadastro)
    $scope.buscarTipoCurso = function (filtro) {
        $$TipoCursoService.lov(filtro, function (data) {
            $scope.listaTipoCurso = data;
        });
    }

    $scope.selecionar = function (item) {
        $scope.model.tipoCursoId = item.id;
    }

    ////pesquisa  tipoCurso (Pesquisa)
    $scope.buscarTipoCursoPesquisa = function (fitro) {
        $$TipoCursoService.lov(fitro, function (data) {
            $scope.listaTipoCursoPesquisa = data;
        });
    }

    $scope.selecionarPesquisa = function (item) {
        $scope.filtros.tipoCursoId = item.id;
    }

    
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
        
        func = (func) ? func() : func;
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

    function resetaForm(form){        
        form.$setPristine();
        form.$setUntouched();
        form.$submitted = false;        
    }
}


