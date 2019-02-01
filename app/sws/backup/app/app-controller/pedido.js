var pedidoCtrl = function($scope, helper, servico, $routeParams,userService,$timeout) {

	$scope.view = {
		aba : 1,	
		editarItem:false,	
		salvar:true,
		editar:true,
		enviar:true,
		cancelar:true,
		isAdm :'',
		dataHora:null,
	};

	$scope.dados={
		id:null,
		categoria_id:null,
		cliente_id:null,
		data_criacao:'',
		observacao:'',
		cliente_id:null,
		data_criacao:''			
	};
	$scope.dadosLov={
		cliente:[]
	}
	$scope.backup={};
		
	// carrega os dados default	
	$scope.itemBackup=={id:'',descricao:'',quantidade:''};	
	$scope.item={
			id:'',
			descricao:'',
			quantidade:'',			
	};
	
	var limparDados= function(){return angular.copy({id:'',descricao:'',quantidade:''})};	
	
	if($routeParams.id > 0){
		$scope.view.salvar=true;
		$scope.dados.id =  $routeParams.id; 
	}else{
		$scope.view.salvar=false;
		$scope.dados.id = 0;
	}	
		
	helper.get("Pedido/Buscar", $scope.dados.id, function(dados) {			
		carregarDados(dados);
		$scope.dados.data_criacao=time();		
		$scope.view.isAdm=(parseInt($scope.user.Nivel)===1);
		criarBackup();		
	});
		
	function time(){
		$scope.view.dataHora =new Date();		
	};
	
	// functios
	$scope.onClickItemAdicionar = function() {			
		if(validaDadosItem()){
			$scope.dadosLov.item.push(angular.copy($scope.item));		
			$scope.item=limparDados();				
		}
	};
	
	$scope.onClickItemRemover = function(index) {
		$scope.dadosLov.item.splice(index,1);
	};
	
	$scope.onClickEditarItem= function(index) {				
		$scope.view.editarItem=true;	
		$scope.itemBackup=  angular.copy($scope.dadosLov.item[index]);
		$scope.item = $scope.dadosLov.item[index];
	};
	
	$scope.onClickItemSalvar=function(){
		if(validaDadosItem()){
			$scope.view.editarItem=false;	
			$scope.itemBackup= {};
			$scope.item=limparDados();	
		}
	};
	
	$scope.onClickItemCancelar=function(){		
		$scope.view.editarItem=false;		
		var index = $scope.dadosLov.item.indexOf($scope.item);	
		$scope.dadosLov.item[index]  = angular.copy($scope.itemBackup);		
		$scope.itemBackup= {};
		$scope.item=limparDados();	
	};	
	
	//crud
	$scope.onClickSalvar = function() {	
		
		if($scope.view.editarItem===true){
			helper.msgAdvertencia('Salve ou Cancele as alterações dos item(s)');
			return;
		}	
				
		var dados ={
				dados:$scope.dados,
				item:$scope.dadosLov.item,
		}
		
		if($scope.dados.id>0){		
			helper.post("Pedido/Editar",dados , function(data){	
				$scope.view.salvar=true;						
				helper.mensagem(data.Mensagem);	
				criarBackup();
			});			
		}else{			
			helper.post("Pedido/Cadastrar",dados , function(data){									
				$scope.view.salvar=true;
				$scope.dados.id = data.Entidade.id;	
				$scope.dados.cliente_id = data.Entidade.cliente_id;	
				helper.mensagem(data.Mensagem);	
				criarBackup();
			});
		}		
	};
	
	function validaDadosItem(){
		if($scope.item.descricao===null || $scope.item.descricao===''){
			helper.msgAdvertencia("Preencha o campo descrição");
			return false;
		}
		if($scope.item.quantidade===null || $scope.item.quantidade===''){
			helper.msgAdvertencia("Preencha o campo Quantidade");
			return false;
		}	
		return true;
	};
	
	$scope.onClickEnviar=function(){
		$scope.dados.situacao_id="3";			
		helper.post("Pedido/MudarSituacaoPedido",$scope.dados , function(dados){
			$scope.view.salvar=true;
			$scope.view.editar=false;
			$scope.view.enviar=false;
			
			$scope.dados = dados.Entidade;
			$scope.dadosLov.situacao= dados.Lov.situacao;	
			
			helper.mensagem(dados.Mensagem);	
		});	
	};
	
	$scope.onClickCancelar=function(){
		$scope.dados.situacao_id="4";			
		helper.post("Pedido/Cancelar",$scope.dados , function(dados){			
			$scope.view.cancelar=false;			
			$scope.dados = dados.Entidade;
			$scope.dadosLov.situacao= dados.Lov.situacao;	
			
			helper.mensagem(dados.Mensagem);	
		});	
	};
	
	$scope.onClickDesfazer=function(){
	   	$scope.view.salvar=true;
	   	$scope.view.editar=true;
	   	$scope.dados = angular.copy($scope.backup.dados);
	   	$scope.dadosLov = angular.copy($scope.backup.dadosLov);
	};
		
	function criarBackup(){
		$scope.backup.dados = angular.copy($scope.dados);
		$scope.backup.dadosLov = angular.copy($scope.dadosLov);	
	};	
	
    function carregarDados(dados){
		$scope.dados = dados.Entidade;	
		$scope.dadosLov= dados.Lov;			
		$scope.dadosLov.item=$scope.toList(dados.Lov.item);
		$scope.dados.situacao_id=($scope.dados.situacao_id>0)?$scope.dados.situacao_id:"10";
	};
	
};