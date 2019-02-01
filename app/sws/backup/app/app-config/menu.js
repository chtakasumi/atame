//obs-> Todas as permissões e visualização do menu devem ser configuradas aqui.
app.constant('menuItems', [
    {
        "Nome": "Solicitação",    //nome do menu
        "Rota": "#/pedido",       //Rota
        "Nivel": [1,2,3],         //Nivel de Acesso 1-adm; 2-cliente; 3-Estabelecimento
        "Ativo": false ,          //Deixe como falso
        "Visualizar":true		  // se o menu irá aparecer sim ou não. todos as todas 
    },
    {
        "Nome": "Cliente",
        "Rota": "#/cliente",
        "Nivel": [1,2],
        "Ativo": false,
        "Visualizar":true
    },
    {
        "Nome": "Estabelecimento",
        "Rota": "#/estabelecimento",
        "Nivel":[1,3],
        "Ativo": false,
        "Visualizar":true
    } ,
    {
        "Nome": "Orçamento",
        "Rota": "#/orcamento",
        "Nivel":[1,2,3],
        "Ativo": false, 
        "Visualizar":false
    },
    {
        "Nome": "Categoria",
        "Rota": "#/categoria",
        "Nivel":[1],
        "Ativo": false, 
        "Visualizar":true
    } ,
    {
        "Nome": "Configuração",
        "Rota": "#/master",
        "Nivel":[0],
        "Ativo": false, 
        "Visualizar":true
    } 
    
]);

//Não modificar o comando abaixo
app.controller("MenuCtrl", ["$rootScope", "menuItems","userService",function ($rootScope, menuItems,userService) {
	
   $rootScope.GetMenus = function () {
	   
	    var nivel = parseInt($rootScope.user.Nivel);    
        
	    if (!nivel > 0) return null;       
        
        for (var i = 0; i < menuItems.length; i++) {        	  
        	  menuItems[i].Ativo = ((menuItems[i].Nivel.indexOf(nivel)>-1) && menuItems[i].Visualizar===true);        	  
        }        
        return menuItems;
    };
}]);
app.service('permissao', ['$rootScope','menuItems',function($rootScope,menuItems) {

	return {
		isPermition : function(rota) {	
			
			var nivel = parseInt($rootScope.user.Nivel);
			
			if (!nivel > 0) return true; 	
				
			for(var i=0; i<menuItems.length; i++ ){						
				var minhaRota = menuItems[i].Rota.replace('#','');
				var rotaVinda=  rota.substr(0,minhaRota.length);		
								
				if(minhaRota===rotaVinda){
				    return (menuItems[i].Nivel.indexOf(nivel)>-1);						
				}
			}
		}	
	};
}]);