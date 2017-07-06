(function () {
    'use strict';

    angular
        .module('app')
        .component('oplossingModal', {
            templateUrl: 'app_content/oplossing/oplossing.html',
            bindings: {
                resolve: '<',
                close: '&',
                dismiss: '&'
            },
            controller: function () {
                var $ctrl = this;

                $ctrl.$onInit = function () {
                    console.log('open oplossing');
                }

                $ctrl.ok = function () {
                    $ctrl.close({ $value: $ctrl.oplossing });
                };

                $ctrl.cancel = function () {
                    $ctrl.dismiss();
                };
            }
        });
})();