var homeCtrl = function($scope, helper, $timeout) {

	$scope.view = {
		lista : []
	};

	function Init() {
		if ($scope.pagina.titulo === 'Home') {
			helper.get("Home/Listar", null, function(retorno) {
				$scope.view.lista = retorno;

			});
			$timeout(Init, 5000);
		}
	};

	Init();

};
