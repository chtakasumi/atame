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
app.controller('cursoCtrl_OLd', function ($rootScope, $scope, cursoService, alertService, tipoCursoService) {

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

    $scope.limparFiltros = function () {       
        $scope.id_pesquisar = null;
        $scope.nome_pesquisar = null;
        $scope.tipocurso_pesquisar = null;       
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
        cursoService.listar($scope.id_pesquisar, $scope.nome_pesquisar, $scope.tipocurso_pesquisar, function (data) {
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

    //****parametros do select 2 Lista Tipo Curso****///
   
    //pesquisa tipoCurso (cadastro)
    $scope.buscarTipoCurso = function (fitro) {
        tipoCursoService.listar(null, fitro, function (data) {
            $scope.listaTipoCurso = data;
        });
    }

    $scope.selecionar = function (item) {
        $scope.curso.tipoCursoId = item.id;
    }

    //pesquisa  tipoCurso (Pesquisa)
    $scope.buscarTipoCursoPesquisa = function (fitro) {
        tipoCursoService.listar(null, fitro, function (data) { 
            $scope.listaTipoCursoPesquisa = data;
        });
    }

    $scope.selecionarPesquisa = function (item) {
        $scope.tipocurso_pesquisar = item.id;
    }
     //****fim select 2


});

app.controller('cursoCtrl', ['$scope', 'alertService', 'parm', function ($scope, alertService, parm) {
    this.$$Model = null;
    this.$$TituloModelo = null;
    this.$$Servico = null;
    this.$$Filtros = null;
    this.$$TipoCursoService=null

    //não muda
    parm.model(function (dados) {      
        $$Model = dados;
        $$TituloModelo = parm.titulo();
        $$Servico = parm.service();
        $$TipoCursoService = parm.tipoCursoService();
        $$Filtros = parm.filter();

        init();
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
    $scope.voltar = function () {
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
            pesquisar();
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
    $scope.salvar = function () {
        //validacoes       
        $$Servico.salvar($scope.model, function (dados) {
            limparFormulario()
            modoEdicao(false);
            alertService.getSucesso("Dados salvo com sucesso");
        });
    }


    //****parametros do select 2 Lista Tipo Curso****///

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
    this.$$Model = null;
    this.$$TituloModelo = null;
    this.$$Servico = null;
    this.$$Filtros = null;

    //não muda
    parm.model(function (dados) {
        $$Model = dados;
        $$TituloModelo = parm.titulo();
        $$Servico = parm.service();
        $$Filtros = parm.filter();

        init();
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
    $scope.voltar = function () {
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
            pesquisar();
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
    $scope.salvar = function () {
        //validacoes       
        $$Servico.salvar($scope.model, function (dados) {
            limparFormulario()
            modoEdicao(false);
            alertService.getSucesso("Dados salvo com sucesso");
        });
    }
}]);

//comportamento da pagina docente
app.controller('docenteCtrl', ['$scope', 'alertService', 'parm', function ($scope, alertService, parm) {
    this.$$Model = null;
    this.$$TituloModelo = null;
    this.$$Servico = null;
    this.$$Filtros = null;

    //não muda
    parm.model(function (dados) {       
        $$Model = dados;
        $$TituloModelo = parm.titulo();
        $$Servico = parm.service();
        $$Filtros = parm.filter();

        init();
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
    $scope.voltar = function () {
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
            pesquisar();
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
    $scope.salvar = function () {
        //validacoes       
        $$Servico.salvar($scope.model, function (dados) {
            limparFormulario()
            modoEdicao(false);
            alertService.getSucesso("Dados salvo com sucesso");
        });
    }
}]);

app.controller('conteudoProgramaticoCtrl', ['$scope', 'alertService', 'parm', function ($scope, alertService, parm) {
    this.$$Model = null;
    this.$$TituloModelo = null;
    this.$$Servico = null;
    this.$$Filtros = null;

    //não muda
    parm.model(function (dados) {
        $$Model = dados;
        $$TituloModelo = parm.titulo();
        $$Servico = parm.service();
        $$Filtros = parm.filter();

        init();
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
    $scope.voltar = function () {
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
            pesquisar();
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
    $scope.salvar = function () {
        //validacoes       
        $$Servico.salvar($scope.model, function (dados) {
            limparFormulario()
            modoEdicao(false);
            alertService.getSucesso("Dados salvo com sucesso");
        });
    }
}]);


