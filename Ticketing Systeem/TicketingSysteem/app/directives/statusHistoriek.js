(function () {
    'use strict';

    angular
        .module('app')
        .directive('statusHistoriek', statusHistoriek);

    statusHistoriek.$inject = ['$window', 'IssueStatusService', '$rootScope'];

    function statusHistoriek($window, issueStatusService, $rootScope) {
        var directive = {
            link: link,
            restrict: 'E',
            templateUrl: 'app_content/directives/statusHistoriek.html',
            scope: {
                issueId: '='
            },
        };
        return directive;

        function link(scope, element, attrs) {

            scope.table = {
                headers: [
                    {
                        name: "SHDCreation",
                        sortable: true,
                        selectable: true
                    },
                    {
                        name: "SHDBeschrijving",
                        sortable: true,
                        selectable: true
                    },
                    {
                        name: "SHDSolver",
                        sortable: true,
                        selectable: true
                    }
                ]
            }

            scope.$watch('issueId', function () {
                getStatusHistoriek();
            });

            $rootScope.$on('refresh', function () {
                getStatusHistoriek();
            });

            function getStatusHistoriek() {
                if (angular.isUndefined(scope.issueId)) return;
                if (scope.issueId == null) return;
                if (scope.issueId === 0) return;

                scope.loading = true;
                issueStatusService.search({ issueId: scope.issueId }).then(function (response) {
                    scope.table.data = response;
                }).finally(function () {
                    scope.loading = false;
                });
            }
        }
    }

})();