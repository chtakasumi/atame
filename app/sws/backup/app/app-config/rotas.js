"use strict";
var rotas = angular.module('rotas', [ 'ngRoute' ])

.constant("rotasNaoAutenticadas", [ '/', '/recuperar-senha' ])

.config([ '$routeProvider', function($routeProvider) {

	// ********Autenticação - home****************

	$routeProvider.when('/', {
		templateUrl : 'view-partial/autenticacao.html',
		controller : 'autenticacaoCtrl',

	}).when('/recuperar-senha', {
		templateUrl : 'view-partial/recuperar-senha.html',
		controller : 'recuperarSenhaCtrl',

	}).when('/home', {
		templateUrl : 'view-partial/home.html',
		controller : 'homeCtrl',

	// ********Estabelecimento*********************

	}).when('/estabelecimento/:id', {
		templateUrl : 'view-partial/estabelecimento.html',
		controller : 'estabelecimentoCtrl',
		pagina : {
			titulo : "Editar Estabelecimento",
			legenda : "Dados do estabelecimento"
		}

	}).when('/estabelecimento/new', {
		templateUrl : 'view-partial/estabelecimento.html',
		controller : 'estabelecimentoCtrl',
		pagina : {
			titulo : "Cadastrar Estabelecimento",
			legenda : "Dados do estabelecimento"
		}

	}).when('/estabelecimento', {
		templateUrl : 'view-partial/estabelecimento-listar.html',
		controller : 'estabelecimentoListarCtrl',
		pagina : {
			titulo : "Listar Estabelecimento",
			legenda : "Dados do estabelecimento"
		}

	}).when('/estabelecimento/visualizar/:id', {
		templateUrl : 'view-partial/estabelecimento-visualizar.html',
		controller : 'estabelecimentoCtrl',
		pagina : {
			titulo : "Visualizar Estabelecimento",
			legenda : "Dados do estabelecimento"
		}

	// ********Usuario******************************

	}).when('/usuario', {
		templateUrl : 'view-partial/usuario.html',
		controller : 'usuarioCtrl',		
		
    // ********Master******************************

	}).when('/master', {
		templateUrl : 'view-partial/master.html',
		controller : 'masterCtrl',

	// ********Cliente*********************

	}).when('/cliente/:id', {
		templateUrl : 'view-partial/cliente.html',
		controller : 'clienteCtrl',
		pagina : {
			titulo : "Editar Clientes",
			legenda : "Dados do cliente"
		}

	}).when('/cliente/new', {
		templateUrl : 'view-partial/cliente.html',
		controller : 'clienteCtrl',
		pagina : {
			titulo : "Cadastrar Clientes",
			legenda : "Dados do cliente"
		}

	}).when('/cliente/visualizar/:id', {
		templateUrl : 'view-partial/cliente-visualizar.html',
		controller : 'clienteCtrl',
		pagina : {
			titulo : "Visualizar Clientes",
			legenda : "Dados do cliente"
		}

	}).when('/cliente', {
		templateUrl : 'view-partial/cliente-listar.html',
		controller : 'clienteListarCtrl',
		pagina : {
			titulo : "Listar Clientes",
			legenda : "Dados do cliente"
		}
	
	// ********Categoria*********************

	}).when('/categoria', {
		templateUrl : 'view-partial/categoria.html',
		controller : 'categoriaCtrl',
		pagina : {
			titulo : 'Cadastrar Categoria',
			legenda : 'Dados da Categoria'
		}
	
	// ********Pedido*********************
	}).when('/pedido', {
		templateUrl : 'view-partial/pedido-listar.html',
		controller : 'pedidoListarCtrl',
		pagina : {
			titulo : 'Listar Solicitação de Orçamento',
			legenda : 'Dados de Solicitação de Orçamento'
		}
	}).when('/pedido/listar/:id', {
		templateUrl : 'view-partial/pedido-listar.html',
		controller : 'pedidoListarCtrl',
		pagina : {
			titulo : 'Listar Solicitação de Orçamento',
			legenda : 'Dados de Solicitação de Orçamento'
		}

	}).when('/pedido/:id', {
		templateUrl : 'view-partial/pedido.html',
		controller : 'pedidoCtrl',
		pagina : {
			titulo : 'Editar Solitação de Orçamento',
			legenda : 'Dados de Solicitação de Orçamento'
		}
	
	}).when('/pedido/visualizar/:id', {
		templateUrl : 'view-partial/pedido-visualizar.html',
		controller : 'pedidoCtrl',
		pagina : {
			titulo : 'Visualizar Solitação de Orçamento',
			legenda : 'Dados de Solicitação de Orçamento'
		}

	}).when('/pedido/new', {
		templateUrl : 'view-partial/pedido.html',
		controller : 'pedidoCtrl',
		pagina : {
			titulo : 'Cadastrar Solicitação de Orçamento',
			legenda : 'Dados de Solicitação de Orçamento'
		}

	// ********Orcamento*********************

	}).when('/orcamento/pedido_id=:pedido_id&cliente_id=:cliente_id', {
		templateUrl : 'view-partial/orcamento-listar.html',
		controller : 'orcamentoListarCtrl',
		pagina : {
			titulo : 'Listar Orçamentos',
			legenda : 'Dados do Orçamento'
		}

	}).when('/orcamento/editar/pedido_id=:pedido_id&id=:id&cliente_id=:cliente_id', {
		templateUrl : 'view-partial/orcamento.html',
		controller : 'orcamentoCtrl',
		pagina : {
			titulo : 'Editar Orçamento',
			legenda : 'Dados do Orçamento'
		}
	
	}).when('/orcamento/visualizar/pedido_id=:pedido_id&id=:id&cliente_id=:cliente_id', {
		templateUrl : 'view-partial/orcamento-visualizar.html',
		controller : 'orcamentoCtrl',
		pagina : {
			titulo : "Visualizar Orçamento",
			legenda : "Dados do Orçamento"
		}
	
	// ********Caso não econtrar a rota*********************
	}).otherwise({
		redirectTo : '/'
	});
}])

.service('anonymous', [ 'rotasNaoAutenticadas',
		function(rotasNaoAutenticadas) {
			return {
				get : function(rota) {
					var retorno = false;
					angular.forEach(rotasNaoAutenticadas, function(item) {
						if (item === rota) {
							retorno = true;
						}
					});
					return retorno;
				}
			}
		} ]);