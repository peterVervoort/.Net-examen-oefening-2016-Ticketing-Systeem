(function () {
    'use strict';

    angular
        .module('app')
        .controller('IssueStatusDetailController', IssueStatusDetailController);

    IssueStatusDetailController.$inject = ['toaster', 'IssueStatusService', '$stateParams', '$state'];

    function IssueStatusDetailController(toaster, IssueStatusService, $stateParams, $state) {
        var ctrl = this;

        function getIssueStatus() {
            ctrl.loading = true;
            IssueStatusService.getById($stateParams.issueStatusId).then(function (response) {
                ctrl.issueStatus = response;
            }, function (error) {
            }).finally(function () {
                ctrl.loading = false;
            });
        }

        ctrl.delete = function () {
            issueStatusService.remove($stateParams.issueStatusId).then(function () {
                $state.go("issueStatus.list");
            })
        }

        init();

        function init() {
            getIssueStatus();
        }
    }
})();