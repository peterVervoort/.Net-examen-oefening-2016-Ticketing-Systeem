(function () {
    'use strict';

    angular
        .module('app')
        .controller('IssueCreateController', issueCreateController);

    issueCreateController.$inject = ['toaster', 'IssueService', '$state', 'NiveauService', '$rootScope'];

    function issueCreateController(toaster, issueService, state, niveauService, $rootScope) {
        var ctrl = this;

        ctrl.dateOptions = {
            formatYear: 'yy',
            //maxDate: new Date(2020, 5, 22),
            //minDate: new Date(),
            startingDay: 1
        };

        ctrl.save = function (isValid) {
            if (isValid) {
                ctrl.loading = true;
                ctrl.issue.gebruikerId = $rootScope.userId;
                issueService.insert(ctrl.issue).then(function () {
                    ctrl.cancel(true)
                }, function (error) {
                }).finally(function () {
                    ctrl.loading = false;
                });
            } else {

            }
        }

        ctrl.cancel = function () {
            state.go('issue.list');
        }

        function getNiveaus() {
            ctrl.loadingRoles = true;
            niveauService.getAll().then(function (response) {
                ctrl.niveaus = response;
            }, function (error) {
            }).finally(function () {
                ctrl.loadingNiveaus = false;
            });
        }

        init();

        function init() {
            getNiveaus();
            ctrl.issue = {
                issueDate: new Date()
            };
        }
    }
})();

