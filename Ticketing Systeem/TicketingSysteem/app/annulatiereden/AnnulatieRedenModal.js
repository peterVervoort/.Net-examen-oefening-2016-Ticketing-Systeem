(function () {
    'use strict';

    angular
        .module('app')
        .component('annulatieRedenModal', {
            templateUrl: 'app_content/annulatiereden/anulatieRedenModal.html',
            bindings: {
                resolve: '<',
                close: '&',
                dismiss: '&'
            },
            controller: function () {
                var $ctrl = this;

                $ctrl.$onInit = function () {
                    console.log('open annulatieReden');
                }

                $ctrl.ok = function () {
                    $ctrl.close({ $value: $ctrl.annulatieReden });
                };

                $ctrl.cancel = function () {
                    $ctrl.dismiss();
                };
            }
        });
})();