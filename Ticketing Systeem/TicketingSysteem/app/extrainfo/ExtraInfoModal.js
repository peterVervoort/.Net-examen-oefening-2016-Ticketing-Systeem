(function () {
    'use strict';

    angular
        .module('app')
        .component('extraInfoModal', {
            templateUrl: 'app_content/extrainfo/extraInfoModal.html',
            bindings: {
                resolve: '<',
                close: '&',
                dismiss: '&'
            },
            controller: ['ExtraInfoService', function (extraInfoService) {
                var $ctrl = this;



                $ctrl.$onInit = function () {
                    if ($ctrl.resolve.update) {
                        $ctrl.update = true;
                        extraInfoService.search({issueStatusId: $ctrl.resolve.statusId }).then(function(response) {
                            $ctrl.extraInfo = response[0];
                        });
                    } else {
                        $ctrl.extraInfo = {
                            issueStatusId: $ctrl.resolve.statusId
                        };
                    }                   
                };

                $ctrl.ok = function () {
                    //$ctrl.close({ $value: $ctrl.extraInfo });
                    $ctrl.close({$value:$ctrl.extraInfo});
                };

                $ctrl.cancel = function () {
                    $ctrl.dismiss();
                };
            }]
        });
})();