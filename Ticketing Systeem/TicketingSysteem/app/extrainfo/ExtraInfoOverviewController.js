(function () {
    'use strict';

    angular
        .module('app')
        .controller('ExtraInfoOverviewController', extraInfoOverviewController);

    extraInfoOverviewController.$inject = ['toaster', 'ExtraInfoService'];

    function extraInfoOverviewController(toaster, extraInfoService) {
        var ctrl = this;

        ctrl.table = {
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

        ctrl.getData = function () {
            ctrl.loading = true;
            extraInfoService.getAll().then(function (response) {
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