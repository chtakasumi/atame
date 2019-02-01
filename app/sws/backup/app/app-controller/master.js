var masterCtrl = function($scope, helper, servico, $routeParams,userService) {
	$scope.view={
			editar : false,			
	};
	
	$scope.dados = {
		id : null,
		nome:null,
		email:null,
		senha:null,
	};
	$scope.view={senha2:null};
		
	$scope.grade={
			lista:[],
	};
	
	helper.get("Master/Listar", $scope.dados.id, function(dados) {
		$scope.grade.lista= dados;
	});
		
	$scope.onClickSalvar = function() {		
		if ($scope.dados.id> 0){
			editar(function(dados){
				$scope.grade.lista = dados.Entidade;
				helper.mensagem(dados.Mensagem);					
			});
		}else{			
			cadastrar(function(dados){
				$scope.grade.lista = dados.Entidade;
				helper.mensagem(dados.Mensagem);
				$scope.dados={};
			});
		}		
	};
			
	//private	
	function cadastrar(sucess) {
		helper.post("Master/Cadastrar", $scope.dados, sucess);
	};
	
	function editar(sucess) {
		helper.post("Master/Editar", $scope.dados,sucess);
	};
}
