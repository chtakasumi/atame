var clienteCtrl = function($scope, helper, servico, $routeParams,userService,upload) {
	$scope.view = {
		aba : 1,
		title:$scope.pagina.titulo,
		legenda:$scope.pagina.legenda,
		page:"Cliente",
        fotoDefault:helper.loadImage("cliente.png")
	};

	$scope.dados = {
		id : null
	};

	$scope.dados.id = ($routeParams.id > 0) ? $routeParams.id : 0;

	// carrega os dados default
	helper.get("Cliente/Buscar", $scope.dados.id, function(dados) {		
		$scope.dados = dados.Entidade;
		$scope.dadosLov= dados.Lov;
		$scope.onChangeCarregaCidades($scope.dados.uf_id);				
	});

	// functios
	$scope.onChangeCarregaCidades = function(id) {		
		if (id > 0) {
			servico.getMunicipios(id, function(dados) {
				$scope.dadosLov.cidade = dados;				
				$scope.dadosLov.cidade_id = $scope.dados.cidade_id;
			});
		}
	}
	
	$scope.onClickSalvar = function() {
		if ($scope.dados.id > 0) {
			editar();

		} else {
			cadastrar(function(dados) {

				$scope.dados.id = dados.Entidade.id;				
				helper.mensagem(dados.Mensagem);

				var param = {					
					tabela_id:$scope.dados.id,
					tabela_nome:'cliente',
					usuario_id : 0,
					nivel:2,
					funcao: atualizarUsuario
				};				
				userService.abrirModal(param);

			});
		}
	}
	//****Usuario******
	$scope.onClickEditarUsuario = function(usuario) {
		var param = {
			tabela_id:$scope.dados.id,
			tabela_nome:'cliente',
			usuario_id : usuario.id,
			nome:usuario.nome,
			nivel:2,
			email:usuario.email,
			funcao: atualizarUsuario
		};	
		
		userService.abrirModal(param);
	};	
	
	$scope.onClickCadastrarUsuario = function() {
		var param = {								
				tabela_id:$scope.dados.id,
				tabela_nome:'cliente',				
				usuario_id : 0,
				nivel:2,
				funcao: atualizarUsuario
			};			
			userService.abrirModal(param);
	};
    
    $scope.onClickCarregarFoto=function(id){      
       var parametro = { 
				server:"User/AtualizarFoto",
				limiteArquivos:1,			
				formData: id,
                autoClose:true
		};		
		upload.open(parametro,function(){
			atualizarUsuario();           
		});	
    };	
	
	function atualizarUsuario(){			
		var dados={
				id :$scope.dados.id,
				tabela:"cliente"	
		};
		
		helper.post("User/BuscarUsuarioDaTabela",dados , function(dados) {
			$scope.dadosLov.usuario = dados.Entidade;			
		});
	}	
	//private	
	function cadastrar(sucess) {
		helper.post("Cliente/Cadastrar", $scope.dados, sucess);
	}
    
	function editar() {
		helper.post("Cliente/Editar", $scope.dados, function(msg) {
			helper.mensagem(msg);
		});
	}
};