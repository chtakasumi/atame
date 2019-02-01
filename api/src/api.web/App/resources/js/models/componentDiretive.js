var app = angular.module('main');

app.directive('Menu', [function () {
    return {
        link: link,
        restrict: 'E',
        template: '<h1>MenuVemaqui<h1>',
    }
    function link(scope, element, attrs) {

    }
}]);

