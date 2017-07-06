(function () {
    'use strict';

    angular
        .module('app')
        .controller('ExtraInfoDetailController', ExtraInfoDetailController);

    ExtraInfoDetailController.$inject = ['toaster', 'ExtraInfoService', '$stateParams', '$state', '$translate'];

    function ExtraInfoDetailController(toaster, ExtraInfoService, $stateParams, $state, $translate) {
        var ctrl = this;

        function getExtraInfo() {
            ctrl.loading = true;
            ExtraInfoService.getById($stateParams.extraInfoId).then(function (response) {
                ctrl.extraInfo = response;
            }, function (error) {
            }).finally(function () {
                ctrl.loading = false;
            });
        }

        ctrl.delete = function () {
            extraInfoService.remove($stateParams.extraInfoId).then(function () {
                $translate('ToasterExtraInfoRemoved').then(function (translate) {
                    toaster.success(translate);
                });
                $state.go("extrainfo.list");
            })
        }

        init();

        function init() {
            getExtraInfo();
        }
    }
})();