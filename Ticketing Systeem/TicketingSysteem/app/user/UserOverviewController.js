(function () {
    'use strict';

    angular
        .module('app')
        .controller('UserOverviewController', userOverviewController);

    userOverviewController.$inject = ['toaster', 'UserService'];

    function userOverviewController(toaster, userService) {
        var ctrl = this;

        ctrl.table = {
            headers: [
                {
                    name: "UOCFirstName",
                    sortable: true,
                    selectable: true
                },
                {
                    name: "UOCName",
                    sortable: true,
                    selectable: true
                },
                {
                    name: "UOCEmail",
                    sortable: true,
                    selectable: true
                },
                {
                    name: "UOCPhone",
                    sortable: true,
                    selectable: true
                },
                {
                    name: "UOCMobile",
                    sortable: true,
                    selectable: true
                },
                {
                    name: "UOCChief",
                    sortable: true,
                    selectable: true
                },
                {
                    name: "UOCRole",
                    sortable: true,
                    selectable: true
                }
            ]
        }

        ctrl.getData = function() {
            ctrl.loading = true;
            userService.getAll().then(function (response) {
                ctrl.table.data = response;
            }, function (error) {
            }).finally(function() {
                ctrl.loading = false;
            });
        }

        init();

        function init() {
            ctrl.getData();
        }
    }
})();
