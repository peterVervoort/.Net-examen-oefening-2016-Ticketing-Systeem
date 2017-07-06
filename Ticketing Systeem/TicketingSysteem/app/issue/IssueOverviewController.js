(function () {
    'use strict';

    angular
        .module('app')
        .controller('IssueOverviewController', issueOverviewController);

    issueOverviewController.$inject = ['toaster', 'IssueService', 'UserService', '$rootScope', 'Roles', '$scope'];

    function issueOverviewController(toaster, issueService, userService, $rootScope, roles, $scope) {
        var ctrl = this;

        ctrl.table = {
            detail: {
                name: 'issue.detail',
                param: 'issueId'
            },
            filter: {},
            headers: [
                {
                    name: "IOCStatus",
                    sortable: true,
                    selectable: true
                },
                {
                    name: "IOCTitel",
                    sortable: true,
                    selectable: true
                },
                {
                    name: "IOCIssueNiveau",
                    sortable: true,
                    selectable: true
                },
                {
                    name: "IOCGebruiker",
                    sortable: true,
                    selectable: true
                },
                {
                    name: "IOCIssueDate",
                    sortable: true,
                    selectable: true
                }
            ]
        }

        ctrl.getData = function () {
            ctrl.loading = true;
            if (ctrl.table.filter.minion) {
                issueService.search({ gebruikerId: ctrl.table.filter.minion.id }).then(function (response) {
                    ctrl.table.data = response;
                }, function (error) {
                }).finally(function () {
                    ctrl.loading = false;
                });
            } else {
                issueService.getAll().then(function (response) {
                    ctrl.table.data = response;
                }, function (error) {
                }).finally(function () {
                    ctrl.loading = false;
                });
            }
        }

        function getMinions() {
            if ($rootScope.userRole == roles.manager) {
                userService.search({ minionOfId: $rootScope.userId }).then(function (response) {
                    ctrl.minions = response;
                });
            }
        }

        $scope.$watch('ctrl.table.filter.minion', function () {
            ctrl.getData();
        });

        init();

        function init() {
            ctrl.getData();
            getMinions();
        }
    }
})();