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

//comportamento da pagina curso
app.controller('cursoCtrl', function ($rootScope, $scope, cursoService, alertService) {

    this.$$Model = null;

    init();

    function init() {
        cursoService.model(function (modelo) {  
            this.$$Model = modelo;
            $scope.curso = angular.copy(this.$$Model);
            modoEdicao(false);
        });       
    }

    $scope.novo = function () {
        modoEdicao(true);
        $scope.titulo = "Cadastrar novo Curso";
    }

    $scope.editar = function (entity) {
        modoEdicao(true);
        $scope.titulo = "Editar Curso";
        carregarFormulario(entity)
    }

    $scope.voltar = function () {
        modoEdicao(false);
    }

    function modoEdicao(bool) {
        $scope.painelPesquisar = !bool;
        $scope.painelEditar = bool;
        if (bool) {
            limparFormulario();
        } else {
            $scope.id_pesquisar = null;
            $scope.nome_pesquisar = null;
            pesquisar();
        }
    }

    function limparFormulario() {       
        $scope.curso = angular.copy(this.$$Model);
    }

    function carregarFormulario(entity) {
        $scope.curso = entity;
    }

    //CRUD

    function pesquisar() {
        cursoService.listar($scope.id_pesquisar, $scope.nome_pesquisar, function (data) {
            $scope.grade_cursos = data;
        });
    }

    $scope.pesquisar = function () {
        pesquisar();
    }

    $scope.excluir = function (id) {
        cursoService.excluir(id, function () {
            modoEdicao(false);      
            alertService.getSucesso("Registro excluido com sucesso");
        });
    }

    $scope.salvar = function () {
        //validacoes       
        cursoService.salvar($scope.curso, function (dados) {          
            limparFormulario()
            modoEdicao(false);
            console.log(dados);
            alertService.getSucesso("Dados salvo com sucesso");
        });
    }
});
