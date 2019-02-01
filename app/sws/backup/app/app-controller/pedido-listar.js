var pedidoListarCtrl = function($scope, helper,$routeParams) {
	$scope.view = {
		filtroUnico : null,
		placeholder : 'Pesquisar por: Código - Descricão - Nome Fantasia - Categoria - Situação - Data Criação',
		dados : [],				
	};
		
	if($routeParams.id !== undefined){	    
		$scope.view.filtroUnico = {
					pedido_id: $routeParams.id
				};
	}
		
	$scope.promessa(function(){	
		if(!$scope.UserNivel().In(1)){
			listar();
		}
	});		
		
	$scope.onKeyPressListar = function(events) {
		if (events.which === 13 || events.keyCode === 13) {
			listar();
		}
	};
	
	$scope.onClickListar = function() {
		listar();
	};
	
	function listar() {
		helper.post("Pedido/Listar", $scope.view.filtroUnico, function(dados) {
			$scope.view.dados.clearGrid();
			$scope.view.dados = $scope.toList(dados);			
		});
	};
	
	$scope.onClickEnviar=function(id,$index){
		var dados={
				id:id,
				situacao_id:3,				
		};				
		helper.post("Pedido/AlterarSituacao",dados, function(dados){
			helper.mensagem(dados.Mensagem);			
			//atualizar apena a linha corrente.	
			$scope.view.dados[$index]=dados.Entidade;	
		});	
	};
	
	$scope.onClickCancelar=function(id, $index){
		var dados={
				id:id,
				situacao_id:4
		};		
		helper.post("Pedido/AlterarSituacao",dados, function(dados){						
			helper.mensagem(dados.Mensagem);			
			//atualizar apena a linha corrente.	
			$scope.view.dados[$index]=dados.Entidade;						
		});	
	};
	
};
