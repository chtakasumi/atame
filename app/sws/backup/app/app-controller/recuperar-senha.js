var recuperarSenhaCtrl = function($scope,helper) {

	$scope.dados = {
		email : ''
	};	
	
	$scope.RecuperarSenha = function() {			
			helper.post("User/RecuperarSenha",$scope.dados, function(msg) {				
				 helper.mensagem(msg);
				 helper.redirecionar("/",4);
			});		
	};

};
