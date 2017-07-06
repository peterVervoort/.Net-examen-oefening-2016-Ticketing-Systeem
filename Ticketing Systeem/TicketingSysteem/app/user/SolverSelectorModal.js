(function () {
    'use strict';

    angular
        .module('app')
        .component('solverSelectorModal', {
            templateUrl: 'app_content/user/solverSelectorModal.html',
            bindings: {
                resolve: '<',
                close: '&',
                dismiss: '&'
            },
            controller: ['UserService', 'Roles', function (userService, roles) {
                var $ctrl = this;

                $ctrl.$onInit = function () {
                    userService.search({ rol: roles.solver }).then(function (solvers) {
                        userService.search({ rol: roles.dispatcher }).then(function (dispatchers) {
                            $ctrl.solvers = solvers;
                            $ctrl.solvers.push.apply($ctrl.solvers, dispatchers)
                            $ctrl.solverId = solvers[0];
                        });
                    });
                };

                $ctrl.ok = function () {
                    $ctrl.close({$value:$ctrl.solverId.id});
                };

                $ctrl.cancel = function () {
                    $ctrl.dismiss();
                };
            }]
        });
})();