﻿var mymodal = angular.module('angular.modal.bootstrap', []);
mymodal.directive('modal', function () {
    return {
        template: '<div class="modal fade" tabindex="-1" role="dialog">' +
            '<div class="modal-dialog" role="document" style="z-index: 999999;">' +
            '<div class="modal-content">' +
            '<div class="modal-header">' +
            '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>' +
            '<h4 class="modal-title">{{ title }}</h4>' +
            '</div>' +
            '<div class="modal-body" style="text-align: center;" ng-transclude></div>' +
            '</div>' +
            '</div>' +
            '</div>',
        restrict: 'E',
        transclude: true,
        replace: true,
        scope:true,
        link: function postLink(scope, element, attrs) {
                      
             scope.title = attrs.title;

            scope.$watch(attrs.visible, function (value) {
              
                if (value == true) {
                    $(element).modal({
                        keyboard: false,
                        backdrop: 'static'
                    });

                }
            });
                                   
            $(element).on('shown.bs.modal', function () {
                scope.$apply(function () {
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