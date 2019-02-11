var valid = angular.module('form.validation', []);
valid.directive('validatePhone', function () {
    var PHONE_REGEXP = /^[789]\d{9}$/;
    return {
        link: function (scope, elm,ctrl) {
            debugger
            elm.on("keyup", function () {               
                var isMatchRegex = PHONE_REGEXP.test(elm.val());
                if (isMatchRegex && elm.hasClass('warning') || elm.val() == '') {
                    elm.removeClass('warning');
                } else if (isMatchRegex == false && !elm.hasClass('warning')) {
                    elm.addClass('warning');
                    elm.after('<span class="glyphicon glyphicon - ok form - control - feedback" aria-hidden="true"></span>');
                    elm.after('<span id="inputSuccess2Status" class="sr-only">(success)</span>');
                }
            });
        }
    }
});

valid.directive('customizarAlgumDiretetivaAqui', function () {
   
    return {
        require: 'ngModel',
        link: function (scope, element, attr, mCtrl) {
           
            var max = parseInt(attr.validateMax);

            function myValidation(value) {
                mCtrl.$modelValue
                
               if (!value) return value;

                var isValido = value.length <= max;

                if (isValido) {
                    mCtrl.$setValidity('validate-max', true);                   
                } else {
                    mCtrl.$setValidity('validate-max', false);                    
                }
                return value;
            }
           // mCtrl.$parsers.push(myValidation); //uUSANDO ISSO PEGO O MODELO O VALOR DO COMPONENTE COM MASKARAS CASO TIVER
            mCtrl.$validators.customValidationFunction = myValidation; //uUSANDO ISSO PEGO O MODELO
        }
    };
});

//
//
