var app = angular.module('app', [ 'toaster', 'rotas', 'httpInterceptor','api','ui.utils.masks','customFilter','bootstrapLightbox']);

app.run(['$rootScope','helper','userService','anonymous','$modal','$timeout','$injector',
         function($rootScope,helper,userService,anonymous,$modal,$timeout,$injector) {
	$(document).ready(function(){
		$(".jquery-waiting-base-container").fadeOut("slow");
	});
	
	permissao= $injector.get('permissao');
	$rootScope.user={Nivel:''};
	$rootScope.consultarSessao=0;
	var temposessao =(60*6);
	
	$rootScope.UserNivel=function(){
		return parseInt(userService.get().Nivel);
	};
	
	$rootScope.UserAtivo=function(){	
	    return parseInt(userService.get().Ativo);			
	};
	
	$rootScope.UserCodigo=function(){	
	    return parseInt(userService.get().Id);
	};
	
	$rootScope.promessa=function(sucesso){
		$timeout(function(){
			sucesso();
		});
	};
	
	$rootScope.ImprimirNoBrower=function(divId){		
	   var conteudo = document.getElementById(divId).innerHTML;
	   tela_impressao = window.open('about:blank');
	   tela_impressao.document.write(conteudo);
	   tela_impressao.window.print();
	   tela_impressao.window.close();
		
	};
	
	$rootScope.funcao = function(){
		console.log("senha alterada");
	}
	
	$rootScope.toList=function(objeto){			
		return helper.toList(objeto);		
	};
	
	$rootScope.sistema = {
		titulo :  'SISCO - Sistema de Controle de Orçamento',
		saudacao: 'Bem vindo',
		rodape1 : '&nbsp;&nbsp;&regSISCO - Sistema de Controle de Orçamento',
		rodape2 : '&copy;Todos os direitos reservados',
		version:  'Versão 1.4.0.0',
		autor:    'suporte@ewati.com.br',
		product: 'Produzido pela: <a style="color:#ffff;" href="http://simwebsite.com.br" target="_blank">ewati.com.br</a>',
		menu:false,		
		tela:{titulo:'',legenda:''}
	};
	
	helper.post("User/BuscarUsuarioLogado",$rootScope.sistema,function(v){});
	
	$rootScope.EditarSenha=function(){		        
		var param = {
				tabela_id:1,
				tabela_nome:(userService.get().Nivel ==='2')?'cliente':'estabelecimento',
				usuario_id :userService.get().Id,
				nome:userService.get().Nome,
				nivel:userService.get().Nivel,
				email:userService.get().Login,
				editarEmailEhSenha:false
			};			
		userService.abrirModal(param);
	};
			
	timer();
	
	function timer(){		
		$rootScope.dataHora = new Date();				
		$rootScope.consultarSessao= parseInt($rootScope.consultarSessao)+1;
		
		//A cada 6 minutos checa sessão.		
		if(parseInt($rootScope.consultarSessao)===temposessao){
			userService.isLogado(function(logado){
				if(!logado){
					 helper.redirecionar("/");
				}
				$rootScope.consultarSessao=0;
			});				
		}		
		$timeout(timer,1000);		
	};
	
	$rootScope.pagina={
			titulo:'',
			legenda:''
	};
	
	$rootScope.abrirModal= function(template,controller,size,param){		
		$modal.open({
			templateUrl : template,
			controller : controller,
			size : size,
			keyboard : false,
			backdrop : 'static',
			resolve : {
				param : function() {
					return param;
				}
			}
		});
	}
	
	$rootScope.confirm=function(titulo,funcao){
		
		var param= {titulo:titulo,funcao:funcao};
		
		$modal.open({
			templateUrl : helper.loadTemplat("confirm.html"),
			controller : function($scope,helper,$modalInstance,param){
				
				$scope.view={titulo:param.titulo};
				
				$scope.Ok = function(){
					param.funcao();
					$modalInstance.dismiss('cancel');
				}
				
				$scope.No = function(){
					 $modalInstance.dismiss('cancel');
				}
				$scope.onClickFecharModal=function(){
					 $modalInstance.dismiss('cancel');
				}
			},
			size :null,
			keyboard : false,
			backdrop : 'static',
			resolve : {
				param : function() {
					return param;
				}
			}
		});
	}

	$rootScope.onClickDeslogar=function(){
		helper.get("User/Deslogar",null,function(){
			$rootScope.sistema.menu=false;
			 userService.destroy();			 
			 helper.redirecionar("/");			 
		});	
	}
	//filter
	$rootScope.$on("$routeChangeStart",function(event,next,current){
		var pagina=event.currentScope.pagina;
		pagina.titulo='',
		pagina.legenda=''		
		var dadosPagina = next.pagina;
		
		if(dadosPagina!==undefined){
			pagina.titulo=dadosPagina.titulo;
			pagina.legenda=dadosPagina.legenda;	
		}
		
		var rota = (next.$$route!==undefined)?next.$$route.originalPath:'';
		
		if(rota!=='' && rota!==undefined){					
			   userService.isLogado(function(logado){	
				   if(anonymous.get(rota)){	
						if(rota==="/" && logado===true){
							event.preventDefault();						
							helper.redirecionar('home');							
						}
				    }else{						
						if(logado){							
							$rootScope.user=userService.get();
							$rootScope.sistema.menu=true;
							 if(!permissao.isPermition(rota)){
								 helper.redirecionar('home');
							 }						 
						}else{							
							$rootScope.sistema.menu=false;							
							helper.redirecionar('/');
						}			
					}						
				});
			 }
	 });
	
}]);

app.service("userService",['$rootScope','helper', function ($rootScope,helper) {

    var _user = { Id: '', Nome: '', Login:'', Nivel: '',Ativo:''};
    var _retorno = undefined;
    
    var userService = {};

    userService.criarUser = function(user) {
        _user.Id = user.id;
        _user.Nome = user.nome;
        _user.Login = user.email;
        _user.Nivel = user.nivel;
        _user.Ativo = user.ativo; //1 ativo; 2 - indimplente       
    };
    
    userService.destroy = function () {
        _user = { Id: '', Nome: '', Login:'', Nivel: '' , Ativo:''};
        $rootScope.MeuMenu=null;
    };

    userService.get = function() {
        return _user;
    };         
    
    userService.existeUsuario = function() {
        return (_user.Id > 0);
    };
    
    userService.isLogado=function(callback){ 
    	
	    	helper.get("User/BuscarUsuarioLogado",null,function(user){
	    		  if(user=='notLicence'){
	    			  helper.redirecionar('/xxxx');
	    			  return;
	    		  }	    		
		  		  if(user.id > 0 && user.id !==null  && user.id !==undefined && user.id !==''){
		  			userService.criarUser(user);	  			
		  			callback(true);  			
		  		  }	else{
		  			 userService.destroy();	  			
		  			 callback(false);
		  		  }
		     });	    	
    };
    
    userService.abrirModal=function(param){
    	$rootScope.abrirModal("view-partial/usuario.html",'usuarioCtrl',null,param);
    }
    
    return userService;
    
}]);

//servicoes gerais de Lovs
app.service("servico",['$rootScope','helper',function($rootScope,helper){
	return {
		getMunicipios : function(id,sucess) {
			helper.get("Endereco/Cidade", id,sucess);
		}
	}
	
}]);