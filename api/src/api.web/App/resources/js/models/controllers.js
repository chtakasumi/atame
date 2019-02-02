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

app.controller('agendamentoCtrl', function ($rootScope, $scope, agendamentoService, pacienteService, alertService, $filter) {

    init();

    function init() {       
        $scope.agendamento = agendamentoService.new();
        modoEdicao(false);
    }

    $scope.novo = function () {
        modoEdicao(true);
        $scope.titulo = "Cadastrar novo agendamento";
        $scope.agendamento.status = "Agendado";
    }

    $scope.editar = function (entity) {
        modoEdicao(true);
        $scope.titulo = "Editar agendamento";
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

            pacienteService.listar(null, null, function (data) {
                $scope.selecionaPaciente = data;
            });

        } else {
            //quando for modo lista
            $scope.cpf_pesquisar = null;
            $scope.nome_pesquisar = null;
            $scope.data_pesquisar = null;
            pesquisar();
        }



    }

    function limparFormulario() {
        $scope.agendamento = agendamentoService.new();
    }

    function carregarFormulario(entity) {

        var separado = entity.data.split("/");
        entity.data = new Date(separado[2], separado[1], separado[0]);

        var separado2 = entity.hora.split(":");
        entity.hora = new Date(separado[2], separado[1], separado[0], separado2[0], separado2[1], 0)

        $scope.agendamento = entity;
    }

    //CRUD

    function pesquisar() {     
        agendamentoService.listar($scope.cpf_pesquisar, $scope.nome_pesquisar, $filter('date')($scope.data_pesquisar, 'yyyy-MM-dd'), function (data) {
            $scope.grade_agendamentos = data;
        });
    }

    $scope.pesquisar = function () {
        pesquisar();
    }

    $scope.excluir = function (id) {
        agendamentoService.excluir(id, function (data) {
            modoEdicao(false);
            alertService.getSucesso(data)
        });
    }

    $scope.salvar = function () {

        //validacoes
        if (!$scope.agendamento.paciente) {
            alertService.getAdvertencia("Selecione um paciente");
            return
        }

        if (!$scope.agendamento.data) {
            alertService.getAdvertencia("preencha o campo data");
            return
        }

        if (!$scope.agendamento.hora) {
            alertService.getAdvertencia("preencha uma hora");
            return
        }


        $scope.agendamento.data = $filter('date')($scope.agendamento.data, 'yyyy-MM-dd');
        $scope.agendamento.hora = $filter('date')($scope.agendamento.hora, 'HH:mm');

        // 1970-01-01T07:53:00.000Z

        agendamentoService.salvar($scope.agendamento, function (data) {
            limparFormulario()
            modoEdicao(false);
            alertService.getSucesso(data);
        });
    }

    $scope.confirmar = function (id) {
        agendamentoService.confirmar(id, function (data) {
            pesquisar();
        });
    }
});

app.controller('atendimentoCtrl', function ($rootScope, $scope, atendimentoService, alertService,  $filter) {

    init();

    function init() {     
        $scope.atendimento = atendimentoService.new();
        modoEdicao(false);
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
            $scope.cpf_pesquisar = null;
            $scope.nome_pesquisar = null;
            $scope.data_pesquisar = null;
            pesquisar();
        }
    }

    function limparFormulario() {
        $scope.atendimento = atendimentoService.new();
    }
   
    //CRUD
    function pesquisar() {
        atendimentoService.listar($scope.cpf_pesquisar,
            $scope.nome_pesquisar,
            $filter('date')($scope.data_pesquisar, 'yyyy-MM-dd'),
            function (data) {
            $scope.grade_atendimentos = data;
        });
    }

    $scope.pesquisar = function () {
        pesquisar();
    }

    $scope.atender = function(entity){
        modoEdicao(true);
        carregarFormulario(entity);       
        $scope.titulo = "Iniciar Atendimento";
        $scope.atendimento.status = "Concluido";
    }

    function carregarFormulario(entity) {
        $scope.atendimento = entity;
    }

    $scope.salvar = function () {

        //validacoes
        if (!$scope.atendimento.prescricao) {
            alertService.getAdvertencia("Preencha a prescrição");
            return
        }
          console.log($scope.atendimento);

       
          atendimentoService.salvar($scope.atendimento, function (data) {
            limparFormulario()
            modoEdicao(false);
            alertService.getSucesso(data);
        });
    }
});