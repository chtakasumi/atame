var licencaCtrl = function ($scope, helper) {  
  helper.get("User/Chave", null, function (chave) {
    $scope.ChaveGerada = chave;
  });
};