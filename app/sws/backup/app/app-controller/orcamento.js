var orcamentoCtrl = function($scope, helper, servico, $routeParams,userService,$timeout) {

	$scope.view = {
		aba : 1,	
		editarItem:false,	
		salvar:'',	
		editar:false,
		isAdm :'',	
		cliente_id:'',
	};
	
	$scope.dadosBackup={
			id:null,
			situacao_id:null,
			pedido_id:null,		
			estabelecimento_id:null,
			valor_total:null,
			sub_total:null,
			prazo_entrega:null,
			desconto:null,
	};

	$scope.dados={
		id:null,
		situacao_id:null,
		pedido_id:null,		
		estabelecimento_id:null,
		valor_total:null,
		sub_total:null,
		prazo_entrega:null,
		desconto:null,
	};

	$scope.dadosLov={
		estabelecimento:[]
	};	

	// carrega os dados default		
	$scope.item={
			item_id:null,
			orcamento_id:null,
			quantidade:0,
			descricao:null,
			preco_unitario:0		
	};
	
	if($routeParams.id > 0){
		$scope.dados.id = $routeParams.id 
		$scope.view.salvar=true; 
	}else{
		$scope.dados.id = 0;
		$scope.view.salvar=false; 		
	}
	
	$scope.dados.pedido_id = ($routeParams.pedido_id > 0) ? $routeParams.pedido_id : 0;	
	$scope.view.cliente_id  = $routeParams.cliente_id;

	helper.post("Orcamento/Buscar", $scope.dados, function(dados) {		
		preencherDados(dados);	
		criarBackup();
		$scope.view.isAdm=(parseInt($scope.user.Nivel)===1);
	});
	
	function criarBackup(){
		var backup={
				Entidade:$scope.dados,
				Lov:$scope.dadosLov,
		}
		
		$scope.dadosBackup =  angular.copy(backup);
	}
	
	function preencherDados(dados){
		$scope.dados = dados.Entidade;
		$scope.dadosLov= dados.Lov;
		$scope.dadosLov.item=$scope.toList(dados.Lov.item);
		$scope.dados.desconto=($scope.dados.desconto==='')?0:$scope.dados.desconto;
		
		$scope.dados.situacao_id=($scope.dados.situacao_id > 0)? $scope.dados.situacao_id:'3';
		
	}	

	$scope.onClickSalvarEhEnviar = function() {
		if(!validaDadosItem()) return false;		

		var dados ={
				dados:$scope.dados,
				item:$scope.dadosLov.item,
		}
		
		criarBackup();
		
		if($scope.dados.id>0){		
			helper.post("Orcamento/Editar",dados , function(data){	
				$scope.view.salvar=true;
				$scope.view.editar=false;
				helper.mensagem(data.Mensagem);
			});			
		}else{			
			helper.post("Orcamento/Cadastrar",dados , function(data){
				$scope.view.salvar=true;
				$scope.view.editar=false;
				$scope.dados.id = data.Entidade.id;	
				$scope.dados.estabelecimento_id = data.Entidade.estabelecimento_id;	
				helper.mensagem(data.Mensagem);			
			});
		}
	}
	
	function validaDadosItem(){		
	  	//verificar se todos dos precos unitários foram informados, se estão maior que 0;
		
		 var item = $scope.dadosLov.item.find('preco_unitario',null); 
		 
		 if(item.length>0){
			 helper.msgAdvertencia("Informe o preço do item \"" + item[0].descricao + "\"");
			 return false;
		 }
		 
		 var item = $scope.dadosLov.item.where('preco_unitario','<==' , 0);
			 
		 if(item.length>0){
			 helper.msgAdvertencia("Informe um preço superior a 0 no item \"" + item[0].descricao + "\"");
			 return false;
		 }
		 
		 if($scope.dados.desconto >= $scope.dados.valor_total){
			 helper.msgAdvertencia("Desconto não pode ser maior que valor total.");			
			 return false;
		 }
		 
		 if($scope.dados.prazo_entrega ===''){
			 helper.msgAdvertencia("Selecione uma prazo de entrega.");			
			 return false;
		 }
		 
		 return true;
	}
	
	$scope.onClickDesfazer=function(){
		$scope.view.salvar=true;
		$scope.view.editar=false;
		preencherDados(angular.copy($scope.dadosBackup));
	};
	
	$scope.onClickEnviar=function(){
		$scope.dados.situacao_id="6";			
		helper.post("Orcamento/AlterarSituacao",$scope.dados , function(dados){	
			preencherDados(dados);			
			helper.mensagem(dados.Mensagem);	
		});	
	}	
	
	$scope.onClickCancelar=function(){
		//enquanto o pedido ainda não foi aprovado. poderá cancelar e editar.
		$scope.dados.situacao_id="4";			
		helper.post("Orcamento/Cancelar",$scope.dados , function(dados){
			preencherDados(dados);
			helper.mensagem(dados.Mensagem);	
		});	
	};
};
