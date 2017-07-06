(function () {
    'use strict';

    angular
        .module('app')
        .directive('extraInfo', extraInfo);

    extraInfo.$inject = ['$window', 'ExtraInfoService'];

    function extraInfo($window, extraInfoService) {
        var directive = {
            link: link,
            restrict: 'E',
            templateUrl: 'app_content/directives/extraInfoTable.html',
            scope: {
                issueStatusId: '='
            },
        };
        return directive;

        function link(scope, element, attrs) {

            scope.table = {
                headers: [
                     {
                         name: "EIOCVraag",
                         sortable: true,
                         selectable: true
                     },
                    {
                        name: "EIOCAntwoord",
                        sortable: true,
                        selectable: true
                    }
                ]
            }

            scope.$watch('issueStatusId', function () {
                if (angular.isUndefined(scope.issueStatusId)) return;
                if (scope.issueStatusId == null) return;
                scope.loading = true;
                extraInfoService.search({ issueStatusId: scope.issueStatusId }).then(function (response) {
                    scope.table.data = response;
                }).finally(function () {
                    scope.loading = false;
                });
            });
        }
    }

})();