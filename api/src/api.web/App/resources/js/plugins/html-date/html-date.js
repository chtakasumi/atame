var mymodal = angular.module('html.date', []);
mymodal.directive('htmlDate', function () {
    return {
        restrict: 'A', 
        require: 'ngModel',       
        link: function (scope, element, attr, ctrl) {
            
            ctrl.$formatters.push(function (value) {     
                ctrl.$setPristine();
                ctrl.$setUntouched(); 
                var retorno = value;
                if (value) {
                    return new Date(value);
                }
                return retorno;
            });
        }
    };    
});

