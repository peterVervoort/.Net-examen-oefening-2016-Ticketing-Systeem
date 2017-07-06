(function () {
    'use strict';

    angular
        .module('app')
        .controller('ExtraInfoCreateController', extraInfoCreateController);

    extraInfoCreateController.$inject = ['toaster', 'ExtraInfoService', '$state'];

    function extraInfoCreateController(toaster, extraInfoService, state) {
        var ctrl = this;

        ctrl.save = function (isValid) {
            if (isValid) {
                ctrl.loading = true;
                ctrl.extraInfo.gebruikerId = $rootScope.userId;
                extraInfoService.insert(ctrl.extraInfo).then(function () {
                    ctrl.cancel(true)
                }, function (error) {
                }).finally(function () {
                    ctrl.loading = false;
                });
            } else {

            }
        }

        ctrl.cancel = function () {
            state.go('extrainfo.list');
        }


        init();

        function init() {
        }
    }
})();

