var app = angular.module('main');
app.factory("autenticacaoService", ['consumerService', function (consumerService) {

    var $cookieNoServidor = true;

    var _autenticate = function () {
        //regras do servidor
        // return $http.get(config.baseUrl + "/checa-permissao");    
        
        return $cookieNoServidor;
    };

    var _deslogar = function () {
        //regras do servidor        
        $cookieNoServidor = false;

    }

    var _autenticar = function (login, senha) {

        consumerService.post("usuario/Autenticar", { "login": login, "senha": senha }, function (data) {
            //regras do servidor
            if (login === 'master' && senha === '1') {
                $cookieNoServidor = true;
            } else {
                $cookieNoServidor = false;
            }

            return $cookieNoServidor;
        })       
    };

    return {
        getPermission: _autenticate,
        deslogar: _deslogar,
        autenticar: _autenticar
    };

}]);

app.factory("pacienteService", ['consumerService', function (consumerService) {

    var _new = function () {
        return angular.copy({ id: null, cpf: null, nome: null, email: null, fone: null });
    };

    var _listar = function (cpf, nome, callBack) {
        var param = {cpf: cpf, nome: nome };
        consumerService.post("paciente/listar", param, function (data) {
            callBack(data);
        })
    };

    var _salvar = function (entidade, callBack) {
        if(entidade.id > 0){
            consumerService.post("paciente/editar", entidade, function (data) {
                callBack(data);
            });
        }else{
            consumerService.post("paciente/cadastrar", entidade, function (data) {
                callBack(data);
            });
        }
    };

    var _excluir = function (id, callBack) {
        var param ={id:id};
        consumerService.post("paciente/excluir", param, function (data) {
            callBack(data);
        });  
    };

    return {
        new: _new,
        listar: _listar,
        salvar: _salvar,
        excluir: _excluir
    };

}]); 

app.factory("agendamentoService", ['consumerService', function (consumerService) {

    var _new = function () {
        return angular.copy({ id: null, cpf: null, nome: null, data: null, hora: null, status:null });
    };

    var _listar = function (cpf, nome, data, callBack) {
        var param = {cpf: cpf, nome: nome, data:data };
        consumerService.post("agendamento/listar", param, function (data) {
            callBack(data);
        })
    };

    var _salvar = function (entidade, callBack) {
        if(entidade.id > 0){
            consumerService.post("agendamento/editar", entidade, function (data) {
                callBack(data);
            });
        }else{
            consumerService.post("agendamento/cadastrar", entidade, function (data) {
                callBack(data);
            });     
        }        

        return callBack("salvo com sucesso!");
    };

    var _excluir = function (id, callBack) {
        var param ={id:id};
        consumerService.post("agendamento/excluir", param, function (data) {
            callBack(data);
        });  
    };

    var _confirmar = function (id, callBack) {
        var param ={id:id};
        consumerService.post("agendamento/confirmar", param, function (data) {
            callBack(data);
        });  
    };

    return {
        new: _new,
        listar: _listar,
        salvar: _salvar,
        excluir: _excluir,
        confirmar: _confirmar
    };

}]); 

app.factory("atendimentoService", ['consumerService', function (consumerService) {

    var _new = function () {
        return angular.copy({ id: null, cpf: null, nome: null, data: null, hora: null, status:null, prescricao:null, agendamento:null });
    };

    var _listar = function (cpf, nome, data, callBack) {
        var param = {cpf: cpf, nome: nome, data:data };
        consumerService.post("atendimento/listar", param, function (data) {
            callBack(data);
        })
    };

    var _salvar = function (entidade, callBack) {    
        consumerService.post("atendimento/cadastrar", entidade, function (data) {
            callBack(data);
        });
    };


    return {
        new: _new,
        listar: _listar,
        salvar: _salvar       
    };

}]); 