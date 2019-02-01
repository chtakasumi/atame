var homeCtrl = function($scope,helper,$timeout) {
	
	$scope.view = {
			lista:[]
	};
	
	function Init(){		
		helper.get("Home/Listar", null, function(retorno) {			
			$scope.view.lista = retorno;
				
		});
	    $timeout(Init,60000);
	};
	
	Init();	
	
};
