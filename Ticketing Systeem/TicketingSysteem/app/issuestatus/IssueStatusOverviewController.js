(function () {
    'use strict';

    angular
        .module('app')
        .controller('IssueStatusOverviewController', issueStatusOverviewController);

    issueStatusOverviewController.$inject = ['toaster', 'IssueStatusService'];

    function issueStatusOverviewController(toaster, issueStatusService) {
        var ctrl = this;

        ctrl.table = {
            headers: [
                {
                    name: "ISOCTitel",
                    sortable: true,
                    selectable: true
                },
                {
                    name: "ISOCIssueStatusNiveau",
                    sortable: true,
                    selectable: true
                },
                {
                    name: "ISOCIssueStatusDate",
                    sortable: true,
                    selectable: true
                },
                {
                    name: "ISOCCreationDate",
                    sortable: true,
                    selectable: true
                },
                {
                    name: "ISOCGebruiker",
                    sortable: true,
                    selectable: true
                },
                {
                    name: "ISOCSolver",
                    sortable: true,
                    selectable: true
                },
                {
                    name: "ISOCOplossing",
                    sortable: true,
                    selectable: true
                }
            ]
        }

        ctrl.getData = function () {
            ctrl.loading = true;
            issueStatusService.getAll().then(function (response) {
                ctrl.table.data = response;
            }, function (error) {
            }).finally(function () {
                ctrl.loading = false;
            });
        }

        init();

        function init() {
            ctrl.getData();
        }
    }
})();