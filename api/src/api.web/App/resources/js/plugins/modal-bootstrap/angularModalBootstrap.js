var mymodal = angular.module('angular.modal.bootstrap', []);
mymodal.directive('modalContainer', function () {
    return {
        template: '<div class="modal fade" tabindex="-1" role="dialog">' +
            '<div class="modal-dialog" role="document" style="z-index: 999999;">' +
            '<div class="modal-content">' +
            '<div class="modal-header">' +
            '<button type="button" ng-show="buttonclose" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>' +
            '<h4 class="modal-title">{{ title }}</h4>' +
            '</div>' +
            '<div class="modal-body" style="text-align: center;" ng-transclude></div>' +
            '</div>' +
            '</div>' +
            '</div>',
        restrict: 'E',
        transclude: true,
        replace: true,
        scope: true,
        link: function postLink(scope, element, attrs) {
            
            scope.buttonclose = JSON.parse(attrs.buttonclose);

            //scope.title = element[0].title; 
           
            scope.$watch(attrs.visible, function (value) {              
                    if (value == true) {
                        $(element).modal({
                            keyboard: JSON.parse(element.attr('keyboard')),
                            backdrop: element.attr('backdrop')
                        });
                    }
                    if (value == false) {
                        $(element).modal('hide');
                    }                   
            });
            $(element).on('shown.bs.modal', function () {
                scope.$apply(function () {
                    scope.title = element[0].title; 
                    scope.$parent[attrs.visible] = true;
                    
                });
            });

            $(element).on('hidden.bs.modal', function () {
                scope.$apply(function () {
                    scope.$parent[attrs.visible] = false;
                });
            });
        }
    };
});
mymodal.service('modalService', ['$rootScope', function ($rootScope) {
    this.open= function () {
        $rootScope.$broadcast('modal-open');
    };
    this.close = function () {
        $rootScope.$broadcast('modal-close');
    };
}]);