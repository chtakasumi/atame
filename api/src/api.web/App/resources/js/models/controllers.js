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
app.controller('cursoCtrl', function ($rootScope, $scope, cursoService, alertService, tipoCursoService) {

    this.$$Model = null;
          
    init();

    function init() {
        cursoService.model(function (modelo) {  
            this.$$Model = modelo;
            $scope.curso = angular.copy(this.$$Model);          
            modoEdicao(false);
        });       
    }

    //****parametros do select 2

    //pesquisa tipo cadastro
    $scope.buscarTipoCurso = function (fitro)
    {      
        tipoCursoService.listar(null, fitro, function (data) {
            $scope.listaTipoCurso = data;
        });
    }

    $scope.selecionar = function (item) {     
        $scope.curso.tipoCursoId = item.id;
    }

    //pesquisa tipo curso
    $scope.buscarTipoCursoPesquisa = function (fitro) {        
        tipoCursoService.listar(null, fitro, function (data) {
            $scope.listaTipoCursoPesquisa = data;
        });
    }

    $scope.selecionarPesquisa = function (item) { 
        $scope.tipocurso_pesquisar = item.id;
    }
     //****fim select 2

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

    $scope.limparFiltros = function () {       
        $scope.id_pesquisar = null;
        $scope.nome_pesquisar = null;
        $scope.tipocurso_pesquisar = null; 
        $scope.listaTipoCursoPesquisa = null;
    }

    function modoEdicao(bool) {
        $scope.painelPesquisar = !bool;
        $scope.painelEditar = bool;
        if (bool) {
            limparFormulario();
        } else {
            $scope.id_pesquisar = null;
            $scope.nome_pesquisar = null;
            $scope.tipocurso_pesquisar = null;
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
        cursoService.listar($scope.id_pesquisar, $scope.nome_pesquisar, $scope.tipoCurso_pesquisar, function (data) {
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
            alertService.getSucesso("Dados salvo com sucesso");
        });
    }
});

//comportamento da pagina tipo curso
app.controller('tipoCursoCtrl', function ($rootScope, $scope, tipoCursoService, alertService) {

    this.$$Model = null;

    init();

    function init() {
        tipoCursoService.model(function (modelo) {
            this.$$Model = modelo;
            $scope.tipoCurso = angular.copy(this.$$Model);           
            modoEdicao(false);
        });
    }

    $scope.novo = function () {
        modoEdicao(true);
        $scope.titulo = "Cadastrar Novo Tipo de Curso";
    }

    $scope.editar = function (entity) {
        modoEdicao(true);
        $scope.titulo = "Editar Tipo de Curso";
        carregarFormulario(entity)
    }

    $scope.voltar = function () {
        modoEdicao(false);
    }

    $scope.limparFiltros = function (){
        $scope.id_pesquisar = null;
        $scope.descricao_pesquisar = null;
    }

    function modoEdicao(bool) {
        $scope.painelPesquisar = !bool;
        $scope.painelEditar = bool;
        if (bool) {
            limparFormulario();
        } else {
            $scope.id_pesquisar = null;
            $scope.descricao_pesquisar = null;
            pesquisar();
        }
    }

    function limparFormulario() {
        $scope.tipoCurso = angular.copy(this.$$Model);
    }

    function carregarFormulario(entity) {
        $scope.tipoCurso = entity;
    }

    //CRUD

    function pesquisar() {
        tipoCursoService.listar($scope.id_pesquisar, $scope.descricao_pesquisar, function (data) {
            $scope.grade_tipoCursos = data;
        });
    }

    $scope.pesquisar = function () {
        pesquisar();
    }

    $scope.excluir = function (id) {
        tipoCursoService.excluir(id, function () {
            modoEdicao(false);
            alertService.getSucesso("Registro excluido com sucesso");
        });
    }

    $scope.salvar = function () {
        //validacoes       
        tipoCursoService.salvar($scope.tipoCurso, function (dados) {
            limparFormulario()
            modoEdicao(false);           
            alertService.getSucesso("Dados salvo com sucesso");
        });
    }
});
