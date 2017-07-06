(function () {
    'use strict';

    angular
        .module('app')
        .controller('IssueStatusCreateController', issueStatusCreateController);

    issueStatusCreateController.$inject = ['toaster', 'IssueStatusService', 'UserService', '$state', 'StatusService'];

    function issueStatusCreateController(toaster, issueStatusService, userService, state, statusService) {
        var ctrl = this;

        ctrl.save = function (isValid) {
            if (isValid) {
                ctrl.loading = true;
                issueStatusService.insert(ctrl.issueStatus).then(function () {
                    ctrl.cancel(true)
                }, function (error) {
                }).finally(function () {
                    ctrl.loading = false;
                });
            } 
        }

        ctrl.cancel = function () {
            state.go('issueStatus.list');
        }

        function getSolvers() {
            ctrl.loadingSolvers = true;
            userService.search({ rol: 'Solver' }).then(function (response) {
                ctrl.solvers = response;
            }, function (error) {
            }).finally(function () {
                ctrl.loadingSolvers = false;
            });
        }

        function getStatussen() {
            ctrl.loadingStatussen = true;
            statusService.getAll().then(function (response) {
                ctrl.statussen = response;
            }, function (error) {
            }).finally(function () {
                ctrl.loadingStatussen = false;
            });
        }

        init();

        function init() {
            getSolvers();
            getStatussen();
        }
    }
})();

