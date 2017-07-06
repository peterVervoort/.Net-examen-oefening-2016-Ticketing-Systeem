(function() {
    'use strict';

    angular
        .module('app')
        .directive('detailItem', detailItem);

    detailItem.$inject = ['$window'];
    
    function detailItem ($window) {
        // Usage:
        //     <detailItem></detailItem>
        // Creates:
        // 
        var directive = {
            link: link,
            restrict: 'E',
            templateUrl: 'app_content/directives/detailitem.html',
            scope: {
                label: '@',
                value: '@'
            },
        };
        return directive;

        function link(scope, element, attrs) {
            console.log(scope.label);
        }
    }

})();