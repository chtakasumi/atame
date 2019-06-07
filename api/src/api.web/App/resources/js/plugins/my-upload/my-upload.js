var module = angular.module('my.upload', []);

module.directive('myUpload', [function () {
    return {
        replace: false,
        restrict: 'A',
        require: 'ngModel',
        scope: {
            ngModel: "="           
        },
        link: function (scope, elm, attrs, ctrl) {
            elm.on("click", function () {
                var inputFile = document.getElementById(attrs.myUpload);               
                inputFile.addEventListener('change', function () {
                    var files = this.files;
                    if (files.length > 0) {
                        getBase64(files[0]);
                    }                   
                });
                inputFile.click();
            });

            ctrl.$formatters.push(function (value) {
                ctrl.$setPristine();
                ctrl.$setUntouched();  
                if (value) {
                    elm.attr('src', value);
                    scope.ngModel = value;
                } else {
                    elm.attr('src', "");
                    scope.ngModel = "";
                }
                return value;
            });
            
            function getBase64(file) {
                var reader = new FileReader();
                reader.readAsDataURL(file);
                reader.onload = function () {   
                    scope.$apply(function () { 
                        elm.attr('src', angular.copy(reader.result));
                        scope.ngModel = angular.copy(reader.result);
                    });
                };
                reader.onerror = function (error) {
                    console.log('Error: ', error);
                };
            }            
        }
    }
}]);