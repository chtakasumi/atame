//Usar um gerador para esse arquivo,pode ser feita em PHP
"use strict";
 app.service('serviceModel', [function () {
      return{
         veiculo: function () {
            return angular.copy({
               id: null,
               cliente_id:null,
               marca_modelo: null,
               placa: null,
               ano_fabricacao: null,
               ano_modelo: null,
               chassi: null,
               renavam: null,
               ativo:''
            });
         }                
      };
   }]);
